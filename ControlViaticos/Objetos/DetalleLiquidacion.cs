using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlViaticos.Objetos
{
    internal class DetalleLiquidacion
    {
        public int id { get; set; }
        public DateTime fechaConsumo { get; set; }
        public int idLiquidacion { get; set; }
        public int idViatico { get; set; }
        public int monto { get; set; }
    }
}
