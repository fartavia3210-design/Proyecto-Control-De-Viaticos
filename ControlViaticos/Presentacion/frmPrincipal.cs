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
        // Inicializa los controles del formulario.
        public frmPrincipal()
        {
            InitializeComponent();
            btnUsuarios.Enabled = SesionActual.EsAdministrador();
            btnCargaInicial.Enabled = SesionActual.EsAdministrador();
            this.Load += new EventHandler(frmPrincipal_Load);
            this.FormClosed += new FormClosedEventHandler(frmPrincipal_FormClosed);
            this.FormClosing += frmPrincipal_FormClosing;
        }

        // Prepara los datos y permisos al abrir el formulario.
        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            ValidarSesion();
        }

        // Comprueba que haya una sesión activa.
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

        // Comprueba la sesión y el permiso de administrador.
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

        // Cierra la sesión al cerrar el menú.
        private void frmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            SesionActual.CerrarSesion();
        }

        // Close recorre el mismo FormClosing para navegar y para salir.
        // Si el formulario cancela, se conserva la pantalla y su preparación.
        private bool CerrarFormularioActual()
        {
            Form actual = pnlContenedor.Tag as Form;
            if (actual == null)
            {
                return true;
            }
            actual.Close();
            if (!actual.IsDisposed)
            {
                return false;
            }
            pnlContenedor.Tag = null;
            return true;
        }

        // Comprueba que se pueda cerrar el formulario abierto.
        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CerrarFormularioActual())
            {
                e.Cancel = true;
            }
        }

        // Abre el formulario seleccionado dentro del menú.
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

        // Comprueba el acceso y abre usuarios.
        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            if (!ValidarAdministrador())
            {
                return;
            }

            pnlSubReportes.Visible = false;
            abrirFormHija(new frmUsuarios());
        }

        // Comprueba el acceso y abre empleados.
        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            pnlSubReportes.Visible = false;
            abrirFormHija(new frmEmpleados());
        }

        // Comprueba el acceso y abre tipos de viático.
        private void btnTiposViatico_Click(object sender, EventArgs e)
        {
            pnlSubReportes.Visible = false;
            abrirFormHija(new frmTiposViatico());
        }

        // Comprueba el acceso y abre liquidaciones.
        private void btnLiquidaciones_Click(object sender, EventArgs e)
        {
            pnlSubReportes.Visible = false;
            abrirFormHija(new frmLiquidaciones());
        }

        // Comprueba el acceso y abre bitácora.
        private void btnBitacora_Click(object sender, EventArgs e)
        {
            pnlSubReportes.Visible = false;
            abrirFormHija(new frmBitacora());
        }

        // Muestra u oculta las opciones de reportes.
        private void btnReportes_Click(object sender, EventArgs e)
        {
            if (!ValidarSesion())
            {
                return;
            }

            pnlSubReportes.Visible = !pnlSubReportes.Visible;
        }

        // Comprueba el acceso y abre consumos por fechas.
        private void btnRepFechas_Click(object sender, EventArgs e)
        {
            pnlSubReportes.Visible = false;
            abrirFormHija(new frmReporteConsumosFechas());
        }

        // Comprueba el acceso y abre consumos por empleado.
        private void btnRepEmpleado_Click(object sender, EventArgs e)
        {
            pnlSubReportes.Visible = false;
            abrirFormHija(new frmReporteConsumosEmpleado());
        }

        // Comprueba el acceso y abre operaciones por usuario.
        private void btnRepOperaciones_Click(object sender, EventArgs e)
        {
            pnlSubReportes.Visible = false;
            abrirFormHija(new frmReporteOperaciones());
        }

        // Comprueba el acceso y abre carga inicial.
        private void btnCargaInicial_Click(object sender, EventArgs e)
        {
            if (!ValidarAdministrador())
            {
                return;
            }

            pnlSubReportes.Visible = false;
            abrirFormHija(new frmCargaInicial());
        }

        // Cierra el menú principal.
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
