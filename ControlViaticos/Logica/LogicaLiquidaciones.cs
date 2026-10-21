using System;
using System.Collections.Generic;
using ControlViaticos.Objetos;

namespace ControlViaticos.Logica
{
    internal class LogicaLiquidaciones
    {
        //----------- Metodos CRUD de trabajo con liquidaciones -----------\\

        public void AgregarLiquidacion(Liquidacion miLiquidacion)
        {
            Estructuras.listaLiquidaciones.Add(miLiquidacion);
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

        public void modificarLiquidacion(Liquidacion miLiquidacion)
        {
            Liquidacion encontrado = buscarLiquidacion(miLiquidacion.id);

            if (encontrado != null)
            {
                encontrado.idEmpleado = miLiquidacion.idEmpleado;
                encontrado.inicio = miLiquidacion.inicio;
                encontrado.fin = miLiquidacion.fin;
                encontrado.justificacion = miLiquidacion.justificacion;
            }
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

        public void AgregarDetalle(DetalleLiquidacion miDetalle)
        {
            Estructuras.listaDetalles.Add(miDetalle);
        }

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

        public void eliminarDetalle(int idLiquidacion, int idViatico)
        {
            foreach (DetalleLiquidacion miDetalle in Estructuras.listaDetalles)
            {
                if (miDetalle.idLiquidacion == idLiquidacion && miDetalle.idViatico == idViatico)
                {
                    Estructuras.listaDetalles.Remove(miDetalle);
                    return;
                }
            }
        }

        public int calcularTotal(int idLiquidacion)
        {
            int total = 0;
            foreach (DetalleLiquidacion miDetalle in ObtenerDetalles(idLiquidacion))
            {
                total = total + miDetalle.monto;
            }
            return total;
        }

        //----------- Reglas de horario  -----------\\

        public bool cumpleHorario(string viatico, DateTime salida, DateTime llegada)
        {
            TimeSpan horaSalida = salida.TimeOfDay;
            DateTime dia = salida.Date;

            if (viatico.Equals("Desayuno"))
            {
                return horaSalida <= new TimeSpan(7, 0, 0) && llegada >= dia.AddHours(10);
            }
            else if (viatico.Equals("Almuerzo"))
            {
                return horaSalida <= new TimeSpan(10, 0, 0) && llegada >= dia.AddHours(14);
            }
            else if (viatico.Equals("Cena"))
            {
                return horaSalida >= new TimeSpan(16, 0, 0) && llegada >= dia.AddHours(20);
            }
            return true;
        }




    }
}
