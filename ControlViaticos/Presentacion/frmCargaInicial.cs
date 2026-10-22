using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControlViaticos.Logica;

namespace ControlViaticos.Presentacion
{
    public partial class frmCargaInicial : Form
    {
        public frmCargaInicial()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmCargaInicial_Load);
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            if (!ValidarPermiso())
            {
                return;
            }

            LogicaCargaInicial logicaCargaInicial = new LogicaCargaInicial();
            int agregados = logicaCargaInicial.EjecutarCargaInicial(SesionActual.usuarioActual);

            if (agregados == -1)
            {
                MessageBox.Show("No tiene permiso para ejecutar la carga inicial.");
                return;
            }

            if (agregados == 0)
            {
                MessageBox.Show("Los datos iniciales ya existen. No se agregaron ni modificaron registros.");
                return;
            }

            MessageBox.Show("Carga inicial completada. Se agregaron " + agregados +
                " registros entre usuarios, empleados y tipos de viático. Los registros existentes se conservaron.");
        }

        private void frmCargaInicial_Load(object sender, EventArgs e)
        {
            ValidarPermiso();
        }

        private bool ValidarPermiso()
        {
            if (!SesionActual.EsAdministrador())
            {
                MessageBox.Show("Solo un administrador puede ejecutar la carga inicial.");
                Close();
                return false;
            }

            return true;
        }
    }
}
