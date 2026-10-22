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
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmUsuarios_Load);

            //--opciones de los combos
            cmbTipo.Items.Add("Normal");
            cmbTipo.Items.Add("Administrador");
            cmbTipo.SelectedIndex = 0;
            cmbEstado.Items.Add("Activo");
            cmbEstado.Items.Add("Inactivo");
            cmbEstado.SelectedIndex = 0;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!ValidarPermiso())
            {
                return;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarPermiso())
            {
                return;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!ValidarPermiso())
            {
                return;
            }
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            ValidarPermiso();
        }

        private bool ValidarPermiso()
        {
            if (!SesionActual.EsAdministrador())
            {
                MessageBox.Show("Solo un administrador puede gestionar usuarios.");
                Close();
                return false;
            }

            return true;
        }
    }
}
