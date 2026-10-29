using System;
using System.Windows.Forms;
using ControlViaticos.Logica;
using ControlViaticos.Objetos;

namespace ControlViaticos.Presentacion
{
    public partial class frmRegistroAdministrador : Form
    {
        // Inicializa los controles del formulario.
        public frmRegistroAdministrador()
        {
            InitializeComponent();
        }

        // Valida y crea el primer administrador.
        private void btnCrear_Click(object sender, EventArgs e)
        {
            int identificacion;
            if (!int.TryParse(txtIdentificacion.Text, out identificacion) || identificacion <= 0)
            {
                MessageBox.Show("La identificación debe ser un número entero positivo.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombreCompleto.Text) || string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtClave.Text))
            {
                MessageBox.Show("Debe completar nombre, usuario y contraseña.");
                return;
            }

            Usuario administrador = new Usuario();
            administrador.id = identificacion;
            administrador.nombreCompleto = txtNombreCompleto.Text;
            administrador.nombreUsuario = txtUsuario.Text;
            administrador.clave = txtClave.Text;
            administrador.tipo = "Administrador";
            administrador.activo = true;

            LogicaAcceso acceso = new LogicaAcceso();

            if (!acceso.CrearPrimerAdministrador(administrador))
            {
                MessageBox.Show("No se pudo crear el primer administrador.");
                return;
            }

            MessageBox.Show("Administrador creado. Inicie sesión con su usuario y contraseña.");
            txtClave.Clear();
            DialogResult = DialogResult.OK;
            Close();
        }

        // Cierra el menú principal.
        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
