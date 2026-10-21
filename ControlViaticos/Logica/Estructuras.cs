using System.Collections.Generic;
using ControlViaticos.Objetos;

namespace ControlViaticos.Logica
{
    internal class Estructuras
    {
        private static readonly List<Liquidacion> listaLiquidaciones = new List<Liquidacion>();
        private static readonly List<DetalleLiquidacion> listaDetalles = new List<DetalleLiquidacion>();

        internal static List<Liquidacion> ListaLiquidaciones
        {
            get { return listaLiquidaciones; }
        }

        internal static List<DetalleLiquidacion> ListaDetalles
        {
            get { return listaDetalles; }
        }
    }
}
