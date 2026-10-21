using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlViaticos.Objetos
{
    internal class TipoViatico
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int monto { get; set; }
        public bool activo { get; set; }
    }
}
