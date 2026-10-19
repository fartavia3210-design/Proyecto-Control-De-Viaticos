using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlViaticos.Presentacion
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        //--por ahora solo abre el menu, la validacion del acceso se hace despues
        private void Ok_Click(object sender, EventArgs e)
        {
            frmPrincipal prc = new frmPrincipal();
            prc.ShowDialog();
        }

        private void Salir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
