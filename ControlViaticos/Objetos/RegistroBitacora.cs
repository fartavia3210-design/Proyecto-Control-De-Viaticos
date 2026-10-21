using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlViaticos.Objetos
{
    internal class RegistroBitacora
    {
        public int id { get; set; }
        public int idUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public DateTime fechaHora { get; set; }
        public string seccion { get; set; }
        public string movimiento { get; set; }
    }
}
