using System;
using System.Collections.Generic;
using ControlViaticos.Objetos;

namespace ControlViaticos.Logica
{
    internal class LogicaReportesConsumo
    {
        // Incluye todos los consumos de ambos dias; un rango invertido devuelve una lista vacia.
        public List<ConsumoReporte> ConsultarConsumosPorFechas(DateTime desde, DateTime hasta)
        {
            List<ConsumoReporte> resultado = new List<ConsumoReporte>();
            if (desde.Date > hasta.Date)
            {
                return resultado;
            }

            foreach (DetalleLiquidacion miDetalle in Estructuras.listaDetalles)
            {
                if (miDetalle.fechaConsumo.Date >= desde.Date &&
                    miDetalle.fechaConsumo.Date <= hasta.Date)
                {
                    ConsumoReporte consumo = ArmarConsumo(miDetalle);
                    if (consumo != null)
                    {
                        resultado.Add(consumo);
                    }
                }
            }

            return resultado;
        }

        // Un empleado inexistente o sin consumos devuelve una lista vacia.
        public List<ConsumoReporte> ConsultarConsumosPorEmpleado(int idEmpleado)
        {
            List<ConsumoReporte> resultado = new List<ConsumoReporte>();
            foreach (DetalleLiquidacion miDetalle in Estructuras.listaDetalles)
            {
                ConsumoReporte consumo = ArmarConsumo(miDetalle);
                if (consumo != null && consumo.idEmpleado == idEmpleado)
                {
                    resultado.Add(consumo);
                }
            }

            return resultado;
        }

        // Une el detalle con su cabecera, empleado y viatico. El monto sale del detalle, no del catalogo.
        private ConsumoReporte ArmarConsumo(DetalleLiquidacion miDetalle)
        {
            LogicaLiquidaciones logicaLiquidaciones = new LogicaLiquidaciones();
            Liquidacion miLiquidacion = logicaLiquidaciones.buscarLiquidacion(miDetalle.idLiquidacion);
            if (miLiquidacion == null)
            {
                return null;
            }

            ConsumoReporte consumo = new ConsumoReporte();
            consumo.idLiquidacion = miLiquidacion.id;
            consumo.idEmpleado = miLiquidacion.idEmpleado;
            consumo.fechaConsumo = miDetalle.fechaConsumo;
            consumo.monto = miDetalle.monto;

            Empleado empleado = logicaLiquidaciones.buscarEmpleado(miLiquidacion.idEmpleado);
            if (empleado != null)
            {
                consumo.nombreEmpleado = empleado.nombre + " " + empleado.apellido1 + " " + empleado.apellido2;
            }

            TipoViatico tipo = logicaLiquidaciones.buscarTipoViatico(miDetalle.idViatico);
            if (tipo != null)
            {
                consumo.nombreViatico = tipo.nombre;
            }

            return consumo;
        }
    }
}
