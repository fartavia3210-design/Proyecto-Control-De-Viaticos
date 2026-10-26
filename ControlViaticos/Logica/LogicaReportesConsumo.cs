using System;
using System.Collections.Generic;
using ControlViaticos.Objetos;

namespace ControlViaticos.Logica
{
    internal class LogicaReportesConsumo
    {
        public List<ConsumoReporte> ConsultarConsumosPorFechas(DateTime desde, DateTime hasta)
        {
            List<ConsumoReporte> resultado = new List<ConsumoReporte>();
            if (desde.Date > hasta.Date) return resultado;
            foreach (DetalleLiquidacion detalle in Estructuras.listaDetalles)
            {
                if (detalle.fechaConsumo.Date < desde.Date || detalle.fechaConsumo.Date > hasta.Date) continue;
                ConsumoReporte consumo = ArmarConsumo(detalle);
                if (consumo != null) resultado.Add(consumo);
            }
            Ordenar(resultado);
            return resultado;
        }

        public List<ConsumoReporte> ConsultarConsumosPorEmpleado(int idEmpleado)
        {
            List<ConsumoReporte> resultado = new List<ConsumoReporte>();
            if (idEmpleado <= 0) return resultado;
            foreach (DetalleLiquidacion detalle in Estructuras.listaDetalles)
            {
                ConsumoReporte consumo = ArmarConsumo(detalle);
                if (consumo != null && consumo.idEmpleado == idEmpleado) resultado.Add(consumo);
            }
            Ordenar(resultado);
            return resultado;
        }

        private static void Ordenar(List<ConsumoReporte> consumos)
        {
            consumos.Sort((a, b) =>
            {
                int fecha = a.fechaConsumo.CompareTo(b.fechaConsumo);
                return fecha != 0 ? fecha : a.idLiquidacion.CompareTo(b.idLiquidacion);
            });
        }

        private static ConsumoReporte ArmarConsumo(DetalleLiquidacion detalle)
        {
            LogicaLiquidaciones liquidaciones = new LogicaLiquidaciones();
            Liquidacion cabecera = liquidaciones.buscarLiquidacion(detalle.idLiquidacion);
            if (cabecera == null) return null;
            return new ConsumoReporte
            {
                idLiquidacion = cabecera.id,
                idEmpleado = cabecera.idEmpleado,
                nombreEmpleado = liquidaciones.NombreEmpleado(cabecera.idEmpleado),
                fechaConsumo = detalle.fechaConsumo.Date,
                nombreViatico = liquidaciones.NombreViatico(detalle.idViatico),
                monto = detalle.monto
            };
        }
    }
}
