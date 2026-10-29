using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlViaticos.Objetos
{
    internal class Empleado
    {
        public int id { get; set; }
        public string cedula { get; set; }
        public string nombre { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
        public bool activo { get; set; }

        public string nombreCompleto
        {
            get
            {
                return (nombre + " " + apellido1 + " " + apellido2).Trim();
            }
        }

        // Permite distinguir homónimos sin usar la posición del combo como ID.
        public string descripcion
        {
            get
            {
                string texto = id + " - " + nombreCompleto;
                if (!activo)
                {
                    texto = texto + " (Inactivo)";
                }
                return texto;
            }
        }
    }
}
