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
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
            DisenoFormularios.Menu(this, pnlMenu, pnlSubReportes, btnUsuarios, btnEmpleados,
                btnTiposViatico, btnLiquidaciones, btnBitacora, btnReportes, pnlSubReportes,
                btnCargaInicial, btnSalir);
            btnUsuarios.Enabled = SesionActual.EsAdministrador();
            btnCargaInicial.Enabled = SesionActual.EsAdministrador();
            this.Load += new EventHandler(frmPrincipal_Load);
            this.FormClosed += new FormClosedEventHandler(frmPrincipal_FormClosed);
            this.FormClosing += frmPrincipal_FormClosing;
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            ValidarSesion();
        }

        private bool ValidarSesion()
        {
            if (!SesionActual.HaySesionActiva())
            {
                MessageBox.Show("Debe iniciar sesión para utilizar el menú.");
                Close();
                return false;
            }

            btnUsuarios.Enabled = SesionActual.EsAdministrador();
            btnCargaInicial.Enabled = SesionActual.EsAdministrador();
            return true;
        }

        private bool ValidarAdministrador()
        {
            if (!ValidarSesion())
            {
                return false;
            }

            if (!SesionActual.EsAdministrador())
            {
                MessageBox.Show("Esta opción solo está disponible para administradores.");
                return false;
            }

            return true;
        }

        private void frmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            SesionActual.CerrarSesion();
        }

        // Close recorre el mismo FormClosing para navegar y para salir.
        // Si el formulario cancela, se conserva la pantalla y su preparación.
        private bool CerrarFormularioActual()
        {
            Form actual = pnlContenedor.Tag as Form;
            if (actual == null) return true;
            actual.Close();
            if (!actual.IsDisposed) return false;
            pnlContenedor.Tag = null;
            return true;
        }

        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CerrarFormularioActual()) e.Cancel = true;
        }

        private void abrirFormHija(Form siguiente)
        {
            if (!ValidarSesion() || !CerrarFormularioActual())
            {
                siguiente.Dispose();
                return;
            }
            siguiente.TopLevel = false;
            siguiente.Dock = DockStyle.Fill;
            pnlContenedor.Controls.Add(siguiente);
            pnlContenedor.Tag = siguiente;
            siguiente.Show();
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            if (!ValidarAdministrador())
            {
                return;
            }

            pnlSubReportes.Visible = false;
            abrirFormHija(new frmUsuarios());
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            pnlSubReportes.Visible = false;
            abrirFormHija(new frmEmpleados());
        }

        private void btnTiposViatico_Click(object sender, EventArgs e)
        {
            pnlSubReportes.Visible = false;
            abrirFormHija(new frmTiposViatico());
        }

        private void btnLiquidaciones_Click(object sender, EventArgs e)
        {
            pnlSubReportes.Visible = false;
            abrirFormHija(new frmLiquidaciones());
        }

        private void btnBitacora_Click(object sender, EventArgs e)
        {
            pnlSubReportes.Visible = false;
            abrirFormHija(new frmBitacora());
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            if (!ValidarSesion())
            {
                return;
            }

            pnlSubReportes.Visible = !pnlSubReportes.Visible;
        }

        private void btnRepFechas_Click(object sender, EventArgs e)
        {
            pnlSubReportes.Visible = false;
            abrirFormHija(new frmReporteConsumosFechas());
        }

        private void btnRepEmpleado_Click(object sender, EventArgs e)
        {
            pnlSubReportes.Visible = false;
            abrirFormHija(new frmReporteConsumosEmpleado());
        }

        private void btnRepOperaciones_Click(object sender, EventArgs e)
        {
            pnlSubReportes.Visible = false;
            abrirFormHija(new frmReporteOperaciones());
        }

        private void btnCargaInicial_Click(object sender, EventArgs e)
        {
            if (!ValidarAdministrador())
            {
                return;
            }

            pnlSubReportes.Visible = false;
            abrirFormHija(new frmCargaInicial());
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
