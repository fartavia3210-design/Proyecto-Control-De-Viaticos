using System;

namespace ControlViaticos.Objetos
{
    // Solo presenta resultados de los reportes; no se guarda en Estructuras.
    internal class ConsumoReporte
    {
        public int idLiquidacion { get; set; }
        public int idEmpleado { get; set; }
        public string nombreEmpleado { get; set; }
        public DateTime fechaConsumo { get; set; }
        public string nombreViatico { get; set; }
        public int monto { get; set; }
    }
}
