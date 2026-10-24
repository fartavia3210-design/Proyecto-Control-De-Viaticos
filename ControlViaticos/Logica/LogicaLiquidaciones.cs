using System;
using System.Collections.Generic;
using ControlViaticos.Objetos;

namespace ControlViaticos.Logica
{
    internal class LogicaLiquidaciones
    {
        public string ultimoError = "";

        //----------- Metodos CRUD de trabajo con liquidaciones -----------\\

        // Guarda la cabecera junto con sus detalles; si algo no es valido no guarda nada.
        public bool AgregarLiquidacion(Liquidacion miLiquidacion, List<DetalleLiquidacion> detalles)
        {
            ultimoError = "";
            if (!SesionActual.HaySesionActiva())
            {
                ultimoError = "Debe iniciar sesión para guardar liquidaciones.";
                return false;
            }

            if (miLiquidacion != null && buscarLiquidacion(miLiquidacion.id) != null)
            {
                ultimoError = "Ya existe una liquidación con ese consecutivo.";
                return false;
            }

            if (!ValidarLiquidacion(miLiquidacion, detalles, true))
            {
                return false;
            }

            miLiquidacion.justificacion = miLiquidacion.justificacion.Trim();
            Estructuras.listaLiquidaciones.Add(miLiquidacion);
            GuardarDetalles(miLiquidacion.id, detalles);
            return true;
        }

        public Liquidacion buscarLiquidacion(int id)
        {
            foreach (Liquidacion miLiquidacion in Estructuras.listaLiquidaciones)
            {
                if (miLiquidacion.id == id)
                {
                    return miLiquidacion;
                }
            }
            return null;
        }

        // Valida todo antes de cambiar la cabecera y reemplazar sus detalles.
        public bool modificarLiquidacion(Liquidacion miLiquidacion, List<DetalleLiquidacion> detalles)
        {
            ultimoError = "";
            if (!SesionActual.HaySesionActiva())
            {
                ultimoError = "Debe iniciar sesión para modificar liquidaciones.";
                return false;
            }

            if (miLiquidacion == null)
            {
                ultimoError = "No existe una liquidación con ese consecutivo.";
                return false;
            }

            Liquidacion encontrado = buscarLiquidacion(miLiquidacion.id);
            if (encontrado == null)
            {
                ultimoError = "No existe una liquidación con ese consecutivo.";
                return false;
            }

            //--Solo se exige empleado activo si se cambia por otro
            bool empleadoNuevo = encontrado.idEmpleado != miLiquidacion.idEmpleado;
            if (!ValidarLiquidacion(miLiquidacion, detalles, empleadoNuevo))
            {
                return false;
            }

            foreach (DetalleLiquidacion miDetalle in ObtenerDetalles(encontrado.id))
            {
                Estructuras.listaDetalles.Remove(miDetalle);
            }

            encontrado.idEmpleado = miLiquidacion.idEmpleado;
            encontrado.inicio = miLiquidacion.inicio;
            encontrado.fin = miLiquidacion.fin;
            encontrado.justificacion = miLiquidacion.justificacion.Trim();
            GuardarDetalles(encontrado.id, detalles);
            return true;
        }

        public void eliminarLiquidacion(int id)
        {
            Liquidacion encontrado = buscarLiquidacion(id);
            if (encontrado != null)
            {
                Estructuras.listaLiquidaciones.Remove(encontrado);

                //--Tambien se borran sus detalles para que no queden sueltos
                foreach (DetalleLiquidacion miDetalle in ObtenerDetalles(id))
                {
                    Estructuras.listaDetalles.Remove(miDetalle);
                }
            }
        }

        public List<Liquidacion> ObtenerLiquidaciones()
        {
            return new List<Liquidacion>(Estructuras.listaLiquidaciones);
            //Copia de la lista real
        }

        //----------- Metodos de trabajo con detalles -----------\\

        public List<DetalleLiquidacion> ObtenerDetalles(int idLiquidacion) //-- Solo los de esa liquidacion --\\
        {
            List<DetalleLiquidacion> resultado = new List<DetalleLiquidacion>();
            foreach (DetalleLiquidacion miDetalle in Estructuras.listaDetalles)
            {
                if (miDetalle.idLiquidacion == idLiquidacion)
                {
                    resultado.Add(miDetalle);
                }
            }
            return resultado;
        }

        public int calcularTotal(int idLiquidacion)
        {
            return calcularTotal(ObtenerDetalles(idLiquidacion));
        }

        public int calcularTotal(List<DetalleLiquidacion> detalles) //-- Sobrecargado para los detalles en preparacion --\\
        {
            int total = 0;
            foreach (DetalleLiquidacion miDetalle in detalles)
            {
                total = total + miDetalle.monto;
            }
            return total;
        }

        // Los detalles nuevos (id 0) reciben su id y el monto actual del catalogo;
        // los ya guardados conservan el monto que tenian.
        private void GuardarDetalles(int idLiquidacion, List<DetalleLiquidacion> detalles)
        {
            int consecutivo = 1;
            foreach (DetalleLiquidacion miDetalle in Estructuras.listaDetalles)
            {
                if (miDetalle.id >= consecutivo)
                {
                    consecutivo = miDetalle.id + 1;
                }
            }

            foreach (DetalleLiquidacion miDetalle in detalles)
            {
                if (miDetalle.id == 0)
                {
                    miDetalle.id = consecutivo;
                    consecutivo++;
                    miDetalle.monto = buscarTipoViatico(miDetalle.idViatico).monto;
                }
                miDetalle.idLiquidacion = idLiquidacion;
                miDetalle.fechaConsumo = miDetalle.fechaConsumo.Date;
                Estructuras.listaDetalles.Add(miDetalle);
            }
        }

        private bool ValidarLiquidacion(Liquidacion miLiquidacion, List<DetalleLiquidacion> detalles, bool exigirEmpleadoActivo)
        {
            if (miLiquidacion == null || miLiquidacion.id <= 0)
            {
                ultimoError = "El consecutivo debe ser un número entero positivo.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(miLiquidacion.justificacion))
            {
                ultimoError = "Debe escribir la justificación.";
                return false;
            }

            if (miLiquidacion.inicio >= miLiquidacion.fin)
            {
                ultimoError = "La fecha de inicio debe ser anterior a la de fin.";
                return false;
            }

            Empleado empleado = buscarEmpleado(miLiquidacion.idEmpleado);
            if (empleado == null)
            {
                ultimoError = "El empleado seleccionado no existe.";
                return false;
            }

            if (exigirEmpleadoActivo && !empleado.activo)
            {
                ultimoError = "El empleado está inactivo y no se puede elegir.";
                return false;
            }

            if (detalles == null || detalles.Count == 0)
            {
                ultimoError = "La liquidación debe tener al menos un detalle.";
                return false;
            }

            foreach (DetalleLiquidacion miDetalle in detalles)
            {
                TipoViatico tipo = buscarTipoViatico(miDetalle.idViatico);
                if (tipo == null)
                {
                    ultimoError = "Un detalle tiene un tipo de viático que no existe.";
                    return false;
                }

                if (miDetalle.id == 0 && !tipo.activo)
                {
                    ultimoError = "El viático " + tipo.nombre + " está inactivo y no se puede elegir.";
                    return false;
                }

                //--Un detalle con id debe ser uno ya guardado de esta misma liquidacion
                if (miDetalle.id != 0 && miDetalle.idLiquidacion != miLiquidacion.id)
                {
                    ultimoError = "Un detalle no pertenece a esta liquidación.";
                    return false;
                }

                if (miDetalle.fechaConsumo.Date < miLiquidacion.inicio.Date ||
                    miDetalle.fechaConsumo.Date > miLiquidacion.fin.Date)
                {
                    ultimoError = "La fecha de " + tipo.nombre + " está fuera del viaje.";
                    return false;
                }

                if (!cumpleHorario(tipo.nombre, miLiquidacion.inicio, miLiquidacion.fin, miDetalle.fechaConsumo))
                {
                    ultimoError = "El horario del viaje no permite " + tipo.nombre + " en la fecha elegida.";
                    return false;
                }
            }

            return true;
        }

        //----------- Catalogos para la liquidacion -----------\\
        // shortcut: lee las listas compartidas directamente, cambiar a LogicaEmpleados y LogicaTiposViatico cuando Mango los entregue.

        public Empleado buscarEmpleado(int id)
        {
            foreach (Empleado miEmpleado in Estructuras.listaEmpleados)
            {
                if (miEmpleado.id == id)
                {
                    return miEmpleado;
                }
            }
            return null;
        }

        public TipoViatico buscarTipoViatico(int id)
        {
            foreach (TipoViatico miTipo in Estructuras.listaTiposViatico)
            {
                if (miTipo.id == id)
                {
                    return miTipo;
                }
            }
            return null;
        }

        public List<Empleado> ObtenerEmpleadosActivos()
        {
            List<Empleado> resultado = new List<Empleado>();
            foreach (Empleado miEmpleado in Estructuras.listaEmpleados)
            {
                if (miEmpleado.activo)
                {
                    resultado.Add(miEmpleado);
                }
            }
            return resultado;
        }

        public List<TipoViatico> ObtenerTiposActivos()
        {
            List<TipoViatico> resultado = new List<TipoViatico>();
            foreach (TipoViatico miTipo in Estructuras.listaTiposViatico)
            {
                if (miTipo.activo)
                {
                    resultado.Add(miTipo);
                }
            }
            return resultado;
        }

        //----------- Reglas de horario  -----------\\

        // El primer dia del viaje se revisa la hora de salida y el ultimo la de llegada;
        // en los dias intermedios se permiten todas las comidas. Otros conceptos no tienen regla.
        public bool cumpleHorario(string viatico, DateTime salida, DateTime llegada, DateTime fechaConsumo)
        {
            DateTime dia = fechaConsumo.Date;
            bool esPrimerDia = dia == salida.Date;
            bool esUltimoDia = dia == llegada.Date;
            TimeSpan horaSalida = salida.TimeOfDay;
            TimeSpan horaLlegada = llegada.TimeOfDay;

            if (viatico.Equals("Desayuno"))
            {
                return (!esPrimerDia || horaSalida <= new TimeSpan(7, 0, 0)) &&
                       (!esUltimoDia || horaLlegada >= new TimeSpan(10, 0, 0));
            }
            else if (viatico.Equals("Almuerzo"))
            {
                return (!esPrimerDia || horaSalida <= new TimeSpan(10, 0, 0)) &&
                       (!esUltimoDia || horaLlegada >= new TimeSpan(14, 0, 0));
            }
            else if (viatico.Equals("Cena"))
            {
                return (!esPrimerDia || horaSalida >= new TimeSpan(16, 0, 0)) &&
                       (!esUltimoDia || horaLlegada >= new TimeSpan(20, 0, 0));
            }
            return true;
        }
    }
}
