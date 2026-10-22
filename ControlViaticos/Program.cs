using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlViaticos
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Logica.Estructuras.listaUsuarios.Count == 0)
            {
                Presentacion.frmRegistroAdministrador registro = new Presentacion.frmRegistroAdministrador();
                DialogResult resultado = registro.ShowDialog();
                registro.Dispose();
                if (resultado != DialogResult.OK)
                {
                    return;
                }
            }
            Application.Run(new Presentacion.frmLogin());
        }
    }
}
