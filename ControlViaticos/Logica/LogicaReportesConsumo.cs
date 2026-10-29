using System;
using System.Collections.Generic;
using ControlViaticos.Objetos;

namespace ControlViaticos.Logica
{
    internal class LogicaReportesConsumo
    {
        // Obtiene los consumos del rango de fechas.
        public List<ConsumoReporte> ConsultarConsumosPorFechas(DateTime desde, DateTime hasta)
        {
            List<ConsumoReporte> resultado = new List<ConsumoReporte>();
            if (desde.Date > hasta.Date)
            {
                return resultado;
            }
            foreach (DetalleLiquidacion detalle in Estructuras.listaDetalles)
            {
                if (detalle.fechaConsumo.Date < desde.Date || detalle.fechaConsumo.Date > hasta.Date)
                {
                    continue;
                }
                ConsumoReporte consumo = ArmarConsumo(detalle);
                if (consumo != null)
                {
                    resultado.Add(consumo);
                }
            }
            Ordenar(resultado);
            return resultado;
        }

        // Obtiene los consumos del empleado indicado.
        public List<ConsumoReporte> ConsultarConsumosPorEmpleado(int idEmpleado)
        {
            List<ConsumoReporte> resultado = new List<ConsumoReporte>();
            if (idEmpleado <= 0)
            {
                return resultado;
            }
            foreach (DetalleLiquidacion detalle in Estructuras.listaDetalles)
            {
                ConsumoReporte consumo = ArmarConsumo(detalle);
                if (consumo != null && consumo.idEmpleado == idEmpleado)
                {
                    resultado.Add(consumo);
                }
            }
            Ordenar(resultado);
            return resultado;
        }

        // Suma los montos de los consumos.
        public long CalcularTotal(List<ConsumoReporte> consumos)
        {
            long total = 0;
            foreach (ConsumoReporte consumo in consumos)
            {
                total += consumo.monto;
            }
            return total;
        }

        // Ordena los consumos por fecha y consecutivo.
        private static void Ordenar(List<ConsumoReporte> consumos)
        {
            consumos.Sort(CompararConsumos);
        }

        // Compara dos consumos por fecha y consecutivo.
        private static int CompararConsumos(ConsumoReporte primero, ConsumoReporte segundo)
        {
            int comparacion = primero.fechaConsumo.CompareTo(segundo.fechaConsumo);
            if (comparacion != 0)
            {
                return comparacion;
            }
            return primero.idLiquidacion.CompareTo(segundo.idLiquidacion);
        }

        // Reúne los datos del consumo para el reporte.
        private static ConsumoReporte ArmarConsumo(DetalleLiquidacion detalle)
        {
            LogicaLiquidaciones liquidaciones = new LogicaLiquidaciones();
            Liquidacion cabecera = liquidaciones.buscarLiquidacion(detalle.idLiquidacion);
            if (cabecera == null)
            {
                return null;
            }

            ConsumoReporte copia = new ConsumoReporte();
            copia.idLiquidacion = cabecera.id;
            copia.idEmpleado = cabecera.idEmpleado;
            copia.nombreEmpleado = liquidaciones.NombreEmpleado(cabecera.idEmpleado);
            copia.fechaConsumo = detalle.fechaConsumo.Date;
            copia.nombreViatico = liquidaciones.NombreViatico(detalle.idViatico);
            copia.monto = detalle.monto;
            return copia;
        }
    }
}
