using System;
using System.Windows.Forms;
using ControlViaticos.Logica;

namespace ControlViaticos.Presentacion
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtClave.Text))
            {
                MessageBox.Show("Debe completar usuario y contraseña.");
                return;
            }

            if (!SesionActual.IniciarSesion(txtUsuario.Text, txtClave.Text))
            {
                MessageBox.Show("Usuario o contraseña incorrectos, o cuenta inactiva.");
                txtClave.Clear();
                txtClave.Focus();
                return;
            }

            Hide();
            frmPrincipal principal = new frmPrincipal();
            try
            {
                principal.ShowDialog();
            }
            finally
            {
                principal.Dispose();
                SesionActual.CerrarSesion();
                txtClave.Clear();
                Show();
                txtClave.Focus();
            }
        }

        private void Salir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
