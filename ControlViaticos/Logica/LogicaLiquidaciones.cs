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
            // Solo se reemplaza el conjunto después de validar todos los datos.
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

            // Cada coincidencia histórica se consume una vez: un tipo inactivo no permite nuevas filas.
            List<DetalleLiquidacion> anteriores = anterior == null ? new List<DetalleLiquidacion>() : ObtenerDetalles(anterior.id);
            long total = 0;
            foreach (DetalleLiquidacion detalle in detalles)
            {
                if (detalle == null || detalle.idLiquidacion != liquidacion.id || detalle.monto <= 0)
                    return Rechazar("Los detalles deben pertenecer a la liquidación y tener un monto positivo.");
                TipoViatico tipo = BuscarTipoViatico(detalle.idViatico);
                if (tipo == null) return Rechazar("Uno de los tipos de viático ya no existe.");
                DetalleLiquidacion historico = anteriores.Find(d => d.idViatico == detalle.idViatico && d.monto == detalle.monto);
                if (historico != null) anteriores.Remove(historico);
                if (!tipo.activo && historico == null) return Rechazar("No se pueden asignar viáticos inactivos.");
                if (historico == null && detalle.monto != tipo.monto)
                    return Rechazar("El monto del catálogo cambió. Quite el detalle nuevo y agréguelo de nuevo.");
                if (!cumpleHorario(tipo.nombre, liquidacion.inicio, liquidacion.fin))
                    return Rechazar("El horario del viaje no permite " + tipo.nombre + ".");
                total += detalle.monto;
                if (total > int.MaxValue) return Rechazar("El total supera el monto máximo permitido.");
            }
            return true;
        }

        private void GuardarConjunto(Liquidacion liquidacion, List<DetalleLiquidacion> detalles)
        {
            Liquidacion copia = CopiarLiquidacion(liquidacion);
            List<DetalleLiquidacion> copias = CopiarDetalles(detalles);
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
                resultado.Add(new DetalleLiquidacion { idLiquidacion = d.idLiquidacion, idViatico = d.idViatico, monto = d.monto });
            return resultado;
        }

        public int calcularTotal(int idLiquidacion)
        {
            int total = 0;
            foreach (DetalleLiquidacion detalle in ObtenerDetalles(idLiquidacion)) total = checked(total + detalle.monto);
            return total;
        }

        // Adaptadores sobre los catálogos compartidos mientras se entregan sus clases de lógica CRUD.
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

        // Se conserva la regla existente; Cena y viajes de varios días siguen pendientes de definición.
        public bool cumpleHorario(string viatico, DateTime salida, DateTime llegada)
        {
            TimeSpan horaSalida = salida.TimeOfDay;
            DateTime dia = salida.Date;
            if (string.Equals(viatico, "Desayuno", StringComparison.OrdinalIgnoreCase))
                return horaSalida <= new TimeSpan(7, 0, 0) && llegada >= dia.AddHours(10);
            if (string.Equals(viatico, "Almuerzo", StringComparison.OrdinalIgnoreCase))
                return horaSalida <= new TimeSpan(10, 0, 0) && llegada >= dia.AddHours(14);
            if (string.Equals(viatico, "Cena", StringComparison.OrdinalIgnoreCase))
                return horaSalida >= new TimeSpan(16, 0, 0) && llegada >= dia.AddHours(20);
            return true;
        }
    }
}
