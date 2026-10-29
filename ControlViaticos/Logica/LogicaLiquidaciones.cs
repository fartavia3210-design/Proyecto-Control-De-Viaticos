using System;
using System.Collections.Generic;
using ControlViaticos.Objetos;

namespace ControlViaticos.Logica
{
    internal class LogicaLiquidaciones
    {
        public string ultimoError = "";

        // Valida y agrega la liquidación.
        public bool AgregarLiquidacion(Liquidacion liquidacion, List<DetalleLiquidacion> detalles)
        {
            if (!Validar(liquidacion, detalles, null))
            {
                return false;
            }

            if (buscarLiquidacion(liquidacion.id) != null)
            {
                return Rechazar("Ya existe una liquidación con ese consecutivo.");
            }
            return GuardarConjunto(liquidacion, detalles);
        }

        // Valida y modifica la liquidación.
        public bool modificarLiquidacion(Liquidacion liquidacion, List<DetalleLiquidacion> detalles)
        {
            Liquidacion anterior;

            if (liquidacion == null)
            {
                anterior = null;
            }
            else
            {
                anterior = buscarLiquidacion(liquidacion.id);
            }
            if (anterior == null)
            {
                return Rechazar("La liquidación no existe.");
            }
            if (!Validar(liquidacion, detalles, anterior))
            {
                return false;
            }
            return GuardarConjunto(liquidacion, detalles);
        }

        // Guarda el motivo del rechazo y devuelve falso.
        private bool Rechazar(string mensaje)
        {
            ultimoError = mensaje;
            return false;
        }

        // Comprueba la sesión y los datos de la liquidación.
        private bool Validar(Liquidacion liquidacion, List<DetalleLiquidacion> detalles, Liquidacion anterior)
        {
            ultimoError = "";

            if (!SesionActual.HaySesionActiva())
            {
                return Rechazar("Debe iniciar sesión.");
            }

            Empleado empleado;

            if (!ValidarCabecera(liquidacion, anterior, out empleado))
            {
                return false;
            }
            return ValidarDetalles(liquidacion, detalles, anterior, empleado);
        }

        // La cabecera valida el viaje y su beneficiario, sin modificar las listas.
        private bool ValidarCabecera(Liquidacion liquidacion, Liquidacion anterior, out Empleado empleado)
        {
            empleado = null;
            if (liquidacion == null || liquidacion.id <= 0)
            {
                return Rechazar("El consecutivo debe ser un entero positivo.");
            }
            empleado = BuscarEmpleado(liquidacion.idEmpleado);

            if (empleado == null || (!empleado.activo && (anterior == null || anterior.idEmpleado != empleado.id)))
            {
                return Rechazar("Seleccione un empleado existente y activo para una nueva asignación.");
            }
            if (liquidacion.fin <= liquidacion.inicio)
            {
                return Rechazar("El fin debe ser posterior al inicio.");
            }
            if (string.IsNullOrWhiteSpace(liquidacion.justificacion))
            {
                return Rechazar("Debe escribir la justificación.");
            }
            return true;
        }

        // La validación del conjunto mantiene identidad, vínculos y total coherentes.
        private bool ValidarDetalles(Liquidacion liquidacion, List<DetalleLiquidacion> detalles,Liquidacion anterior, Empleado empleado)
        {
            if (detalles == null || detalles.Count == 0)
            {
                return Rechazar("Agregue al menos un detalle antes de guardar.");
            }

            List<DetalleLiquidacion> anteriores = new List<DetalleLiquidacion>();
            if (anterior != null)
            {
                anteriores = ObtenerDetalles(anterior.id);
            }
            List<int> idsUsados = new List<int>();
            long total = 0;
            foreach (DetalleLiquidacion detalle in detalles)
            {
                if (detalle == null || detalle.idLiquidacion != liquidacion.id || detalle.monto <= 0)
                {
                    return Rechazar("Los detalles deben pertenecer a la liquidación y tener un monto positivo.");
                }
                if (detalle.fechaConsumo.Date < liquidacion.inicio.Date || detalle.fechaConsumo.Date > liquidacion.fin.Date)
                {
                    return Rechazar("La fecha de consumo debe estar dentro del viaje.");
                }
                TipoViatico tipo = BuscarTipoViatico(detalle.idViatico);
                if (tipo == null)
                {
                    return Rechazar("Uno de los tipos de viático ya no existe.");
                }

                if (detalle.id == 0)
                {
                    if (!ValidarDetalleNuevo(detalle, empleado, tipo))
                    {
                        return false;
                    }
                }
                else
                {
                    DetalleLiquidacion historico = BuscarDetalle(anteriores, detalle.id);
                    if (idsUsados.Contains(detalle.id))
                    {
                        return Rechazar("No repita el identificador de un detalle.");
                    }
                    idsUsados.Add(detalle.id);
                    if (!ValidarDetalleHistorico(detalle, historico, tipo, empleado, anterior != null && anterior.idEmpleado != liquidacion.idEmpleado))
                    {
                        return false;
                    }
                }

                if (!cumpleHorario(tipo.nombre, liquidacion.inicio, liquidacion.fin, detalle.fechaConsumo))
                {
                    return Rechazar("El horario del viaje no permite " + tipo.nombre + " en la fecha elegida.");
                }
                total += detalle.monto;
                if (total > int.MaxValue)
                {
                    return Rechazar("El total supera el monto máximo permitido.");
                }
            }
            return true;
        }

        // Valida el empleado, el tipo y el monto del nuevo consumo.
        private bool ValidarDetalleNuevo(DetalleLiquidacion detalle, Empleado empleado, TipoViatico tipo)
        {
            if (!empleado.activo)
            {
                return Rechazar("No se pueden asignar nuevos viáticos a un empleado inactivo.");
            }
            if (!tipo.activo)
            {
                return Rechazar("No se pueden asignar viáticos inactivos.");
            }
            if (detalle.monto != tipo.monto)
            {
                return Rechazar("El monto del catálogo cambió. Quite el detalle nuevo y agréguelo de nuevo.");
            }
            return true;
        }

        // Un registro inactivo se conserva como historial; no habilita otro pago,
        // otra fecha de consumo ni la transferencia de un tipo inactivo a otra persona.
        private bool ValidarDetalleHistorico(DetalleLiquidacion detalle, DetalleLiquidacion historico, TipoViatico tipo, Empleado empleado, bool cambioEmpleado)
        {
            if (detalle.id < 0 || historico == null || historico.idViatico != detalle.idViatico || historico.monto != detalle.monto)
            {
                return Rechazar("El detalle guardado no coincide con esta liquidación.");
            }
            bool cambioFecha = historico.fechaConsumo.Date != detalle.fechaConsumo.Date;
            if (!empleado.activo && cambioFecha)
            {
                return Rechazar("No se puede asignar otra fecha de consumo a un empleado inactivo.");
            }
            if (!tipo.activo && (cambioEmpleado || cambioFecha))
            {
                return Rechazar("Un viático inactivo solo puede conservarse con su empleado y fecha originales.");
            }
            return true;
        }

        // Solo se escriben cabecera y detalles después de validar y preparar sus copias e IDs.
        private bool GuardarConjunto(Liquidacion liquidacion, List<DetalleLiquidacion> detalles)
        {
            Liquidacion copia = CopiarLiquidacion(liquidacion);
            List<DetalleLiquidacion> copias = CopiarDetalles(detalles);
            List<DetalleLiquidacion> ocupados = new List<DetalleLiquidacion>(Estructuras.listaDetalles);
            foreach (DetalleLiquidacion detalle in copias)
            {
                if (detalle.id == 0)
                {
                    detalle.id = Identificadores.Siguiente(ocupados);
                    if (detalle.id == 0)
                    {
                        return Rechazar("No quedan identificadores de detalle disponibles.");
                    }
                    ocupados.Add(detalle);
                }
                detalle.fechaConsumo = detalle.fechaConsumo.Date;
            }
            QuitarCabecera(copia.id);
            QuitarDetalles(copia.id);
            Estructuras.listaLiquidaciones.Add(copia);
            Estructuras.listaDetalles.AddRange(copias);
            return true;
        }

        // Valida y elimina la liquidación.
        public bool eliminarLiquidacion(int id)
        {
            ultimoError = "";
            if (!SesionActual.HaySesionActiva())
            {
                return Rechazar("Debe iniciar sesión.");
            }
            if (buscarLiquidacion(id) == null)
            {
                return Rechazar("La liquidación no existe.");
            }
            QuitarDetalles(id);
            QuitarCabecera(id);
            return true;
        }

        // Busca la liquidación.
        public Liquidacion buscarLiquidacion(int id)
        {
            foreach (Liquidacion liquidacion in Estructuras.listaLiquidaciones)
            {
                if (liquidacion.id == id)
                {
                    return CopiarLiquidacion(liquidacion);
                }
            }
            return null;
        }

        // Obtiene las liquidaciones.
        public List<Liquidacion> ObtenerLiquidaciones()
        {
            List<Liquidacion> resultado = new List<Liquidacion>();
            foreach (Liquidacion liquidacion in Estructuras.listaLiquidaciones)
            {
                resultado.Add(CopiarLiquidacion(liquidacion));
            }
            return resultado;
        }

        // Obtiene los detalles de la liquidación.
        public List<DetalleLiquidacion> ObtenerDetalles(int idLiquidacion)
        {
            return CopiarDetalles(DetallesDeLiquidacion(idLiquidacion));
        }

        // Crea una copia de la liquidación.
        private static Liquidacion CopiarLiquidacion(Liquidacion l)
        {

            Liquidacion copia = new Liquidacion();
            copia.id = l.id;
            copia.idEmpleado = l.idEmpleado;
            copia.inicio = l.inicio;
            copia.fin = l.fin;
            copia.justificacion = l.justificacion;
            return copia;
        }

        // Crea copias de los detalles.
        private static List<DetalleLiquidacion> CopiarDetalles(List<DetalleLiquidacion> detalles)
        {
            List<DetalleLiquidacion> resultado = new List<DetalleLiquidacion>();
            foreach (DetalleLiquidacion d in detalles)
            {
                DetalleLiquidacion nuevoDetalle = new DetalleLiquidacion();
                nuevoDetalle.id = d.id;
                nuevoDetalle.idLiquidacion = d.idLiquidacion;
                nuevoDetalle.idViatico = d.idViatico;
                nuevoDetalle.monto = d.monto;
                nuevoDetalle.fechaConsumo = d.fechaConsumo;
                resultado.Add(nuevoDetalle);
            }
            return resultado;
        }

        // Suma los montos de la liquidación.
        public int calcularTotal(int idLiquidacion)
        {
            int total = 0;
            foreach (DetalleLiquidacion detalle in ObtenerDetalles(idLiquidacion))
            {
                total = checked(total + detalle.monto);
            }
            return total;
        }

        // Obtiene los empleados.
        public List<Empleado> ObtenerEmpleados()

        {

            return new List<Empleado>(Estructuras.listaEmpleados);

        }
        // Obtiene los tipos de viático.
        public List<TipoViatico> ObtenerTiposViatico()
        {
            return new List<TipoViatico>(Estructuras.listaTiposViatico);
        }
        // Busca el empleado.
        public Empleado BuscarEmpleado(int id)
        {
            return new LogicaEmpleados().BuscarEmpleado(id);
        }
        // Busca el tipo de viático.
        public TipoViatico BuscarTipoViatico(int id)
        {
            return new LogicaTiposViatico().BuscarTipoViatico(id);
        }
        // Obtiene el nombre del empleado para mostrarlo.
        public string NombreEmpleado(int id)
        {
            Empleado e = BuscarEmpleado(id);
            if (e == null)
            {
                return "Empleado no disponible (" + id + ")";
            }
            else
            {
                return (e.nombre + " " + e.apellido1 + " " + e.apellido2).Trim();
            }
        }
        // Obtiene el nombre del viático para mostrarlo.
        public string NombreViatico(int id)
        {
            TipoViatico t = BuscarTipoViatico(id);
            if (t == null)
            {
                return "Viático no disponible (" + id + ")";
            }
            else
            {
                return t.nombre;
            }
        }

        // Comprueba el horario permitido para el consumo.
        public bool cumpleHorario(string viatico, DateTime salida, DateTime llegada)
        {
            return cumpleHorario(viatico, salida, llegada, salida.Date);
        }

        // Comprueba el horario permitido para el consumo.
        public bool cumpleHorario(string viatico, DateTime salida, DateTime llegada, DateTime fechaConsumo)
        {
            return ReglasViaticos.Cumple(viatico, salida, llegada, fechaConsumo);
        }

        // Busca un detalle por su identificador.
        private DetalleLiquidacion BuscarDetalle(List<DetalleLiquidacion> detalles, int id)
        {
            foreach (DetalleLiquidacion detalle in detalles)
            {
                if (detalle.id == id)
                {
                    return detalle;
                }
            }
            return null;
        }

        // Obtiene los detalles de la liquidación indicada.
        private List<DetalleLiquidacion> DetallesDeLiquidacion(int id)
        {
            List<DetalleLiquidacion> resultado = new List<DetalleLiquidacion>();
            foreach (DetalleLiquidacion detalle in Estructuras.listaDetalles)
            {
                if (detalle.idLiquidacion == id)
                {
                    resultado.Add(detalle);
                }
            }
            return resultado;
        }

        // Retira los detalles de la liquidación indicada.
        private void QuitarDetalles(int id)
        {
            // Recorrer hacia atras permite quitar filas sin saltarse la siguiente.
            for (int i = Estructuras.listaDetalles.Count - 1; i >= 0; i--)
            {
                if (Estructuras.listaDetalles[i].idLiquidacion == id)
                {
                    Estructuras.listaDetalles.RemoveAt(i);
                }
            }
        }

        // Retira la cabecera de la liquidación indicada.
        private void QuitarCabecera(int id)
        {
            for (int i = Estructuras.listaLiquidaciones.Count - 1; i >= 0; i--)
            {
                if (Estructuras.listaLiquidaciones[i].id == id)
                {
                    Estructuras.listaLiquidaciones.RemoveAt(i);
                }
            }
        }
    }
}
