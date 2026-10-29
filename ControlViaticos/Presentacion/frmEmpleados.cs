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
    public partial class frmEmpleados : Form
    {
        private LogicaEmpleados logicaEmpleados = new LogicaEmpleados();
        private LogicaBitacora logicaBitacora = new LogicaBitacora();

        public frmEmpleados()
        {
            InitializeComponent();
            Control campoIdentificacion = DisenoFormularios.Campo(lblIdentificacion, txtIdentificacion, btnBuscar);
            Control campoCedula = DisenoFormularios.Campo(lblCedula, txtCedula);
            Control campoNombre = DisenoFormularios.Campo(lblNombre, txtNombre);
            Control campoApellido1 = DisenoFormularios.Campo(lblApellido1, txtApellido1);
            Control campoApellido2 = DisenoFormularios.Campo(lblApellido2, txtApellido2);
            Control campoTelefono = DisenoFormularios.Campo(lblTelefono, txtTelefono);
            Control campoDireccion = DisenoFormularios.Campo(lblDireccion, txtDireccion);
            Control campoEstado = DisenoFormularios.Campo(lblEstado, cmbEstado);
            Control filaCampos = DisenoFormularios.Fila(campoIdentificacion, campoCedula, campoNombre, campoApellido1, campoApellido2, campoTelefono, campoDireccion, campoEstado);
            Control filaAcciones = DisenoFormularios.Fila(btnGuardar, btnEliminar);
            DisenoFormularios.Pagina(this, lblTitulo, filaCampos, filaAcciones, grdEmpleados);
            this.Load += new EventHandler(frmEmpleados_Load);

            //--opciones de los combos
            cmbEstado.Items.Add("Activo");
            cmbEstado.Items.Add("Inactivo");
            cmbEstado.SelectedIndex = 0;
            ConfigurarTabla();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!ValidarSesion())
            {
                return;
            }

            int identificacion;
            if (!LeerIdentificacion(out identificacion))
            {
                return;
            }

            Empleado encontrado = logicaEmpleados.BuscarEmpleado(identificacion);
            if (encontrado == null)
            {
                MessageBox.Show("No existe un empleado con esa identificación.");
                return;
            }

            txtCedula.Text = encontrado.cedula;
            txtNombre.Text = encontrado.nombre;
            txtApellido1.Text = encontrado.apellido1;
            txtApellido2.Text = encontrado.apellido2;
            txtTelefono.Text = encontrado.telefono;
            txtDireccion.Text = encontrado.direccion;
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
            if (!ValidarSesion())
            {
                return;
            }

            int identificacion;
            if (!LeerIdentificacion(out identificacion))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCedula.Text) || string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido1.Text) || string.IsNullOrWhiteSpace(txtApellido2.Text) || string.IsNullOrWhiteSpace(txtTelefono.Text) || string.IsNullOrWhiteSpace(txtDireccion.Text) || cmbEstado.SelectedIndex == -1)
            {
                MessageBox.Show("Debe completar cédula, nombre, apellidos, teléfono, dirección y estado.");
                return;
            }

            // Un objeto nuevo evita cambiar el registro antes de validar la operación.
            Empleado empleado = new Empleado();
            empleado.id = identificacion;
            empleado.cedula = txtCedula.Text;
            empleado.nombre = txtNombre.Text;
            empleado.apellido1 = txtApellido1.Text;
            empleado.apellido2 = txtApellido2.Text;
            empleado.telefono = txtTelefono.Text;
            empleado.direccion = txtDireccion.Text;
            empleado.activo = cmbEstado.SelectedItem.ToString() == "Activo";

            bool esNuevo = logicaEmpleados.BuscarEmpleado(identificacion) == null;
            bool guardado;
            if (esNuevo)
            {
                guardado = logicaEmpleados.AgregarEmpleado(empleado);
            }
            else
            {
                guardado = logicaEmpleados.ModificarEmpleado(empleado);
            }

            if (!guardado)
            {
                MessageBox.Show(logicaEmpleados.ultimoError);
                return;
            }

            if (esNuevo)
            {
                logicaBitacora.RegistrarOperacion(SesionActual.usuarioActual, "Empleados", "Creó empleado " + empleado.nombre + " (ID " + empleado.id + ")");
                MessageBox.Show("Empleado creado correctamente.");
            }
            else
            {
                logicaBitacora.RegistrarOperacion(SesionActual.usuarioActual, "Empleados", "Modificó empleado " + empleado.nombre + " (ID " + empleado.id + ")");
                MessageBox.Show("Empleado modificado correctamente.");
            }
            ActualizarTabla();
            LimpiarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!ValidarSesion())
            {
                return;
            }

            int identificacion;
            if (!LeerIdentificacion(out identificacion))
            {
                return;
            }

            Empleado encontrado = logicaEmpleados.BuscarEmpleado(identificacion);
            if (encontrado == null)
            {
                MessageBox.Show("No existe un empleado con esa identificación.");
                return;
            }

            if (MessageBox.Show("¿Eliminar al empleado " + encontrado.nombre + "?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            if (!logicaEmpleados.EliminarEmpleado(identificacion))
            {
                MessageBox.Show(logicaEmpleados.ultimoError);
                return;
            }

            logicaBitacora.RegistrarOperacion(SesionActual.usuarioActual, "Empleados", "Eliminó empleado " + encontrado.nombre + " (ID " + encontrado.id + ")");
            MessageBox.Show("Empleado eliminado correctamente.");
            ActualizarTabla();
            LimpiarCampos();
        }

        private void frmEmpleados_Load(object sender, EventArgs e)
        {
            if (ValidarSesion())
            {
                ActualizarTabla();
            }
        }

        private bool ValidarSesion()
        {
            if (SesionActual.HaySesionActiva())
            {
                return true;
            }
            MessageBox.Show("Debe iniciar sesión para utilizar este catálogo.");
            Close();
            return false;
        }

        private void ConfigurarTabla()
        {
            grdEmpleados.AutoGenerateColumns = false;
            grdEmpleados.Columns.Clear();
            grdEmpleados.ReadOnly = true;
            grdEmpleados.AllowUserToAddRows = false;
            grdEmpleados.AllowUserToDeleteRows = false;
            grdEmpleados.MultiSelect = false;
            grdEmpleados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            string[] propiedades = { "id", "cedula", "nombre", "apellido1", "apellido2", "telefono", "direccion", "activo" };
            string[] titulos = { "Identificación", "Cédula", "Nombre", "Primer apellido", "Segundo apellido", "Teléfono", "Dirección", "Estado" };
            for (int i = 0; i < propiedades.Length; i++)
            {
                DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn();
                columna.Name = propiedades[i];
                columna.DataPropertyName = propiedades[i];
                columna.HeaderText = titulos[i];
                grdEmpleados.Columns.Add(columna);
            }

            grdEmpleados.Columns["id"].FillWeight = 150;
            grdEmpleados.Columns["direccion"].FillWeight = 130;

            grdEmpleados.CellFormatting += new DataGridViewCellFormattingEventHandler(grdEmpleados_CellFormatting);
        }

        private void grdEmpleados_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && grdEmpleados.Columns[e.ColumnIndex].Name == "activo" && e.Value is bool)
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
            grdEmpleados.DataSource = logicaEmpleados.ObtenerEmpleados();
            grdEmpleados.ClearSelection();
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
            txtCedula.Clear();
            txtNombre.Clear();
            txtApellido1.Clear();
            txtApellido2.Clear();
            txtTelefono.Clear();
            txtDireccion.Clear();
            cmbEstado.SelectedIndex = 0;
            txtIdentificacion.Focus();
        }
    }
}
