using System;
using System.Collections.Generic;
using ControlViaticos.Objetos;

namespace ControlViaticos.Logica
{
    internal class LogicaLiquidaciones
    {
        public string ultimoError = "";

        public bool AgregarLiquidacion(Liquidacion liquidacion, List<DetalleLiquidacion> detalles)
        {
            if (!Validar(liquidacion, detalles, null)) return false;
            if (buscarLiquidacion(liquidacion.id) != null)
                return Rechazar("Ya existe una liquidación con ese consecutivo.");
            GuardarConjunto(liquidacion, detalles);
            return true;
        }

        public bool modificarLiquidacion(Liquidacion liquidacion, List<DetalleLiquidacion> detalles)
        {
            Liquidacion anterior = liquidacion == null ? null : buscarLiquidacion(liquidacion.id);
            if (anterior == null) return Rechazar("La liquidación no existe.");
            if (!Validar(liquidacion, detalles, anterior)) return false;
            GuardarConjunto(liquidacion, detalles);
            return true;
        }

        private bool Rechazar(string mensaje)
        {
            ultimoError = mensaje;
            return false;
        }

        private bool Validar(Liquidacion liquidacion, List<DetalleLiquidacion> detalles, Liquidacion anterior)
        {
            ultimoError = "";
            if (!SesionActual.HaySesionActiva()) return Rechazar("Debe iniciar sesión.");
            if (liquidacion == null || liquidacion.id <= 0)
                return Rechazar("El consecutivo debe ser un entero positivo.");
            Empleado empleado = BuscarEmpleado(liquidacion.idEmpleado);
            if (empleado == null || (!empleado.activo && (anterior == null || anterior.idEmpleado != empleado.id)))
                return Rechazar("Seleccione un empleado existente y activo para una nueva asignación.");
            if (liquidacion.fin <= liquidacion.inicio) return Rechazar("El fin debe ser posterior al inicio.");
            if (string.IsNullOrWhiteSpace(liquidacion.justificacion)) return Rechazar("Debe escribir la justificación.");
            if (detalles == null || detalles.Count == 0) return Rechazar("Agregue al menos un detalle antes de guardar.");

            List<DetalleLiquidacion> anteriores = anterior == null
                ? new List<DetalleLiquidacion>() : ObtenerDetalles(anterior.id);
            HashSet<int> idsUsados = new HashSet<int>();
            long total = 0;
            foreach (DetalleLiquidacion detalle in detalles)
            {
                if (detalle == null || detalle.idLiquidacion != liquidacion.id || detalle.monto <= 0)
                    return Rechazar("Los detalles deben pertenecer a la liquidación y tener un monto positivo.");
                if (detalle.fechaConsumo.Date < liquidacion.inicio.Date ||
                    detalle.fechaConsumo.Date > liquidacion.fin.Date)
                    return Rechazar("La fecha de consumo debe estar dentro del viaje.");
                TipoViatico tipo = BuscarTipoViatico(detalle.idViatico);
                if (tipo == null) return Rechazar("Uno de los tipos de viático ya no existe.");

                if (detalle.id == 0)
                {
                    if (!tipo.activo) return Rechazar("No se pueden asignar viáticos inactivos.");
                    if (detalle.monto != tipo.monto)
                        return Rechazar("El monto del catálogo cambió. Quite el detalle nuevo y agréguelo de nuevo.");
                }
                else
                {
                    DetalleLiquidacion historico = anteriores.Find(d => d.id == detalle.id);
                    if (detalle.id < 0 || historico == null || !idsUsados.Add(detalle.id) ||
                        historico.idViatico != detalle.idViatico || historico.monto != detalle.monto)
                        return Rechazar("El detalle guardado no coincide con esta liquidación.");
                }

                if (!cumpleHorario(tipo.nombre, liquidacion.inicio, liquidacion.fin, detalle.fechaConsumo))
                    return Rechazar("El horario del viaje no permite " + tipo.nombre + " en la fecha elegida.");
                total += detalle.monto;
                if (total > int.MaxValue) return Rechazar("El total supera el monto máximo permitido.");
            }
            return true;
        }

        private void GuardarConjunto(Liquidacion liquidacion, List<DetalleLiquidacion> detalles)
        {
            Liquidacion copia = CopiarLiquidacion(liquidacion);
            List<DetalleLiquidacion> copias = CopiarDetalles(detalles);
            int siguienteId = 1;
            foreach (DetalleLiquidacion existente in Estructuras.listaDetalles)
                if (existente.id >= siguienteId) siguienteId = existente.id + 1;
            foreach (DetalleLiquidacion detalle in copias)
            {
                if (detalle.id == 0) detalle.id = siguienteId++;
                detalle.fechaConsumo = detalle.fechaConsumo.Date;
            }
            Estructuras.listaLiquidaciones.RemoveAll(l => l.id == copia.id);
            Estructuras.listaDetalles.RemoveAll(d => d.idLiquidacion == copia.id);
            Estructuras.listaLiquidaciones.Add(copia);
            Estructuras.listaDetalles.AddRange(copias);
        }

        public bool eliminarLiquidacion(int id)
        {
            ultimoError = "";
            if (!SesionActual.HaySesionActiva()) return Rechazar("Debe iniciar sesión.");
            if (buscarLiquidacion(id) == null) return Rechazar("La liquidación no existe.");
            Estructuras.listaDetalles.RemoveAll(d => d.idLiquidacion == id);
            Estructuras.listaLiquidaciones.RemoveAll(l => l.id == id);
            return true;
        }

        public Liquidacion buscarLiquidacion(int id)
        {
            foreach (Liquidacion liquidacion in Estructuras.listaLiquidaciones)
                if (liquidacion.id == id) return CopiarLiquidacion(liquidacion);
            return null;
        }

        public List<Liquidacion> ObtenerLiquidaciones()
        {
            List<Liquidacion> resultado = new List<Liquidacion>();
            foreach (Liquidacion liquidacion in Estructuras.listaLiquidaciones) resultado.Add(CopiarLiquidacion(liquidacion));
            return resultado;
        }

        public List<DetalleLiquidacion> ObtenerDetalles(int idLiquidacion)
        {
            return CopiarDetalles(Estructuras.listaDetalles.FindAll(d => d.idLiquidacion == idLiquidacion));
        }

        private static Liquidacion CopiarLiquidacion(Liquidacion l)
        {
            return new Liquidacion { id = l.id, idEmpleado = l.idEmpleado, inicio = l.inicio, fin = l.fin, justificacion = l.justificacion };
        }

        private static List<DetalleLiquidacion> CopiarDetalles(List<DetalleLiquidacion> detalles)
        {
            List<DetalleLiquidacion> resultado = new List<DetalleLiquidacion>();
            foreach (DetalleLiquidacion d in detalles)
                resultado.Add(new DetalleLiquidacion { id = d.id, idLiquidacion = d.idLiquidacion,
                    idViatico = d.idViatico, monto = d.monto, fechaConsumo = d.fechaConsumo });
            return resultado;
        }

        public int calcularTotal(int idLiquidacion)
        {
            int total = 0;
            foreach (DetalleLiquidacion detalle in ObtenerDetalles(idLiquidacion)) total = checked(total + detalle.monto);
            return total;
        }

        public List<Empleado> ObtenerEmpleados() { return new List<Empleado>(Estructuras.listaEmpleados); }
        public List<TipoViatico> ObtenerTiposViatico() { return new List<TipoViatico>(Estructuras.listaTiposViatico); }
        public Empleado BuscarEmpleado(int id) { return Estructuras.listaEmpleados.Find(e => e.id == id); }
        public TipoViatico BuscarTipoViatico(int id) { return Estructuras.listaTiposViatico.Find(t => t.id == id); }
        public string NombreEmpleado(int id)
        {
            Empleado e = BuscarEmpleado(id);
            return e == null ? "Empleado no disponible (" + id + ")" : (e.nombre + " " + e.apellido1 + " " + e.apellido2).Trim();
        }
        public string NombreViatico(int id)
        {
            TipoViatico t = BuscarTipoViatico(id);
            return t == null ? "Viático no disponible (" + id + ")" : t.nombre;
        }

        public bool cumpleHorario(string viatico, DateTime salida, DateTime llegada)
        {
            return cumpleHorario(viatico, salida, llegada, salida.Date);
        }

        // En días intermedios se permiten las comidas. Los límites de Cena siguen el enunciado escrito.
        public bool cumpleHorario(string viatico, DateTime salida, DateTime llegada, DateTime fechaConsumo)
        {
            DateTime dia = fechaConsumo.Date;
            bool primero = dia == salida.Date;
            bool ultimo = dia == llegada.Date;
            TimeSpan horaSalida = salida.TimeOfDay;
            TimeSpan horaLlegada = llegada.TimeOfDay;
            if (string.Equals(viatico, "Desayuno", StringComparison.OrdinalIgnoreCase))
                return (!primero || horaSalida <= new TimeSpan(7, 0, 0)) &&
                    (!ultimo || horaLlegada >= new TimeSpan(10, 0, 0));
            if (string.Equals(viatico, "Almuerzo", StringComparison.OrdinalIgnoreCase))
                return (!primero || horaSalida <= new TimeSpan(10, 0, 0)) &&
                    (!ultimo || horaLlegada >= new TimeSpan(14, 0, 0));
            if (string.Equals(viatico, "Cena", StringComparison.OrdinalIgnoreCase))
                return (!primero || horaSalida <= new TimeSpan(16, 0, 0)) &&
                    (!ultimo || horaLlegada >= new TimeSpan(20, 0, 0));
            return true;
        }
    }
}
