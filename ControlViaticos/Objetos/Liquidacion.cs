using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlViaticos.Objetos
{
    internal class Liquidacion
    {
        public int id { get; set; }
        public int idEmpleado { get; set; }
        public DateTime inicio { get; set; }
        public DateTime fin { get; set; }
        public string justificacion { get; set; }

    }
}
