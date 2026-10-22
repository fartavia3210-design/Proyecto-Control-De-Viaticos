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
using ControlViaticos.Objetos;

namespace ControlViaticos.Presentacion
{
    public partial class frmUsuarios : Form
    {
        private LogicaUsuarios logicaUsuarios = new LogicaUsuarios();
        private LogicaBitacora logicaBitacora = new LogicaBitacora();

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
            ConfigurarTabla();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!ValidarPermiso())
            {
                return;
            }

            int identificacion;
            if (!LeerIdentificacion(out identificacion))
            {
                return;
            }

            Usuario encontrado = logicaUsuarios.BuscarUsuario(identificacion);
            if (encontrado == null)
            {
                MessageBox.Show("No existe un usuario con esa identificación.");
                return;
            }

            txtNombreCompleto.Text = encontrado.nombreCompleto;
            txtNombreUsuario.Text = encontrado.nombreUsuario;
            txtClave.Text = encontrado.clave;
            cmbTipo.SelectedItem = encontrado.tipo;
            if (encontrado.activo)
            {
                cmbEstado.SelectedItem = "Activo";
            }
            else
            {
                cmbEstado.SelectedItem = "Inactivo";
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarPermiso())
            {
                return;
            }

            int identificacion;
            if (!LeerIdentificacion(out identificacion))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombreCompleto.Text) ||
                string.IsNullOrWhiteSpace(txtNombreUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtClave.Text) ||
                cmbTipo.SelectedIndex == -1 || cmbEstado.SelectedIndex == -1)
            {
                MessageBox.Show("Debe completar nombre, usuario, clave, tipo y estado.");
                return;
            }

            // Un objeto nuevo evita cambiar el registro antes de validar la operación.
            Usuario usuario = new Usuario();
            usuario.id = identificacion;
            usuario.nombreCompleto = txtNombreCompleto.Text;
            usuario.nombreUsuario = txtNombreUsuario.Text;
            usuario.clave = txtClave.Text;
            usuario.tipo = cmbTipo.SelectedItem.ToString();
            usuario.activo = cmbEstado.SelectedItem.ToString() == "Activo";

            bool esNuevo = logicaUsuarios.BuscarUsuario(identificacion) == null;
            bool guardado;
            if (esNuevo)
            {
                guardado = logicaUsuarios.AgregarUsuario(usuario);
            }
            else
            {
                guardado = logicaUsuarios.ModificarUsuario(usuario);
            }

            if (!guardado)
            {
                MessageBox.Show(logicaUsuarios.ultimoError);
                return;
            }

            if (esNuevo)
            {
                logicaBitacora.RegistrarOperacion(SesionActual.usuarioActual, "Usuarios",
                    "Creó usuario " + usuario.nombreUsuario + " (ID " + usuario.id + ")");
                MessageBox.Show("Usuario creado correctamente.");
            }
            else
            {
                logicaBitacora.RegistrarOperacion(SesionActual.usuarioActual, "Usuarios",
                    "Modificó usuario " + usuario.nombreUsuario + " (ID " + usuario.id + ")");
                MessageBox.Show("Usuario modificado correctamente.");
            }
            ActualizarTabla();
            LimpiarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!ValidarPermiso())
            {
                return;
            }

            int identificacion;
            if (!LeerIdentificacion(out identificacion))
            {
                return;
            }

            Usuario encontrado = logicaUsuarios.BuscarUsuario(identificacion);
            if (encontrado == null)
            {
                MessageBox.Show("No existe un usuario con esa identificación.");
                return;
            }

            if (MessageBox.Show("¿Eliminar al usuario " + encontrado.nombreUsuario + "?",
                "Confirmar eliminación", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            if (!logicaUsuarios.EliminarUsuario(identificacion))
            {
                MessageBox.Show(logicaUsuarios.ultimoError);
                return;
            }

            logicaBitacora.RegistrarOperacion(SesionActual.usuarioActual, "Usuarios",
                "Eliminó usuario " + encontrado.nombreUsuario + " (ID " + encontrado.id + ")");
            MessageBox.Show("Usuario eliminado correctamente.");
            ActualizarTabla();
            LimpiarCampos();
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            if (ValidarPermiso())
            {
                ActualizarTabla();
            }
        }

        private void ConfigurarTabla()
        {
            grdUsuarios.AutoGenerateColumns = false;
            grdUsuarios.Columns.Clear();
            grdUsuarios.ReadOnly = true;
            grdUsuarios.AllowUserToAddRows = false;
            grdUsuarios.AllowUserToDeleteRows = false;
            grdUsuarios.MultiSelect = false;
            grdUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Se incluyen únicamente las columnas visibles; la clave no se agrega.
            string[] propiedades = { "id", "nombreCompleto", "nombreUsuario", "tipo", "activo" };
            string[] titulos = { "Identificación", "Nombre completo", "Usuario", "Tipo", "Estado" };
            for (int i = 0; i < propiedades.Length; i++)
            {
                DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn();
                columna.Name = propiedades[i];
                columna.DataPropertyName = propiedades[i];
                columna.HeaderText = titulos[i];
                grdUsuarios.Columns.Add(columna);
            }

            grdUsuarios.CellFormatting += new DataGridViewCellFormattingEventHandler(grdUsuarios_CellFormatting);
        }

        private void grdUsuarios_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && grdUsuarios.Columns[e.ColumnIndex].Name == "activo" &&
                e.Value is bool)
            {
                if ((bool)e.Value)
                {
                    e.Value = "Activo";
                }
                else
                {
                    e.Value = "Inactivo";
                }
                e.FormattingApplied = true;
            }
        }

        private void ActualizarTabla()
        {
            grdUsuarios.DataSource = logicaUsuarios.ObtenerUsuarios();
            grdUsuarios.ClearSelection();
        }

        private bool LeerIdentificacion(out int identificacion)
        {
            if (!int.TryParse(txtIdentificacion.Text, out identificacion) || identificacion <= 0)
            {
                MessageBox.Show("La identificación debe ser un número entero positivo.");
                txtIdentificacion.Focus();
                return false;
            }
            return true;
        }

        private void LimpiarCampos()
        {
            txtIdentificacion.Clear();
            txtNombreCompleto.Clear();
            txtNombreUsuario.Clear();
            txtClave.Clear();
            cmbTipo.SelectedIndex = 0;
            cmbEstado.SelectedIndex = 0;
            txtIdentificacion.Focus();
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
