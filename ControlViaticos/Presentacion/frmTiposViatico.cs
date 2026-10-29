using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControlViaticos.Logica;
using ControlViaticos.Objetos;

namespace ControlViaticos.Presentacion
{
    public partial class frmTiposViatico : Form
    {
        private LogicaTiposViatico logicaTipos = new LogicaTiposViatico();
        private LogicaBitacora logicaBitacora = new LogicaBitacora();

        // Inicializa los controles del formulario.
        public frmTiposViatico()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmTiposViatico_Load);

            //--opciones de los combos
            cmbEstado.Items.Add("Activo");
            cmbEstado.Items.Add("Inactivo");
            cmbEstado.SelectedIndex = 0;
            ConfigurarTabla();
        }

        // Busca el registro y carga sus datos.
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!ValidarSesion())
            {
                return;
            }

            int identificador;
            if (!LeerIdentificador(out identificador))
            {
                return;
            }

            TipoViatico encontrado = logicaTipos.BuscarTipoViatico(identificador);
            if (encontrado == null)
            {
                MessageBox.Show("No existe un tipo de viático con ese identificador.");
                return;
            }

            txtNombre.Text = encontrado.nombre;
            txtMonto.Text = encontrado.monto.ToString();
            if (encontrado.activo)
            {
                cmbEstado.SelectedItem = "Activo";
            }
            else
            {
                cmbEstado.SelectedItem = "Inactivo";
            }
        }

        // Valida y guarda los datos del registro.
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarSesion())
            {
                return;
            }

            int identificador;
            if (!LeerIdentificador(out identificador))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtMonto.Text) || cmbEstado.SelectedIndex == -1)
            {
                MessageBox.Show("Debe completar nombre, monto y estado.");
                return;
            }

            int monto;
            if (!int.TryParse(txtMonto.Text, out monto) || monto <= 0)
            {
                MessageBox.Show("El monto debe ser un número entero mayor que cero.");
                txtMonto.Focus();
                return;
            }

            // Un objeto nuevo evita cambiar el registro antes de validar la operación.
            TipoViatico tipo = new TipoViatico();
            tipo.id = identificador;
            tipo.nombre = txtNombre.Text;
            tipo.monto = monto;
            tipo.activo = cmbEstado.SelectedItem.ToString() == "Activo";

            bool esNuevo = logicaTipos.BuscarTipoViatico(identificador) == null;
            bool guardado;
            if (esNuevo)
            {
                guardado = logicaTipos.AgregarTipoViatico(tipo);
            }
            else
            {
                guardado = logicaTipos.ModificarTipoViatico(tipo);
            }

            if (!guardado)
            {
                MessageBox.Show(logicaTipos.ultimoError);
                return;
            }

            if (esNuevo)
            {
                logicaBitacora.RegistrarOperacion(SesionActual.usuarioActual, "Tipos de viático", "Creó tipo de viático " + tipo.nombre + " (ID " + tipo.id + ")");
                MessageBox.Show("Tipo de viático creado correctamente.");
            }
            else
            {
                logicaBitacora.RegistrarOperacion(SesionActual.usuarioActual, "Tipos de viático", "Modificó tipo de viático " + tipo.nombre + " (ID " + tipo.id + ")");
                MessageBox.Show("Tipo de viático modificado correctamente.");
            }
            ActualizarTabla();
            LimpiarCampos();
        }

        // Confirma y elimina el registro.
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!ValidarSesion())
            {
                return;
            }

            int identificador;
            if (!LeerIdentificador(out identificador))
            {
                return;
            }

            TipoViatico encontrado = logicaTipos.BuscarTipoViatico(identificador);
            if (encontrado == null)
            {
                MessageBox.Show("No existe un tipo de viático con ese identificador.");
                return;
            }

            if (MessageBox.Show("¿Eliminar el tipo de viático " + encontrado.nombre + "?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            if (!logicaTipos.EliminarTipoViatico(identificador))
            {
                MessageBox.Show(logicaTipos.ultimoError);
                return;
            }

            logicaBitacora.RegistrarOperacion(SesionActual.usuarioActual, "Tipos de viático", "Eliminó tipo de viático " + encontrado.nombre + " (ID " + encontrado.id + ")");
            MessageBox.Show("Tipo de viático eliminado correctamente.");
            ActualizarTabla();
            LimpiarCampos();
        }

        // Prepara los datos y permisos al abrir el formulario.
        private void frmTiposViatico_Load(object sender, EventArgs e)
        {
            if (ValidarSesion())
            {
                ActualizarTabla();
            }
        }

        // Comprueba que haya una sesión activa.
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

        // Configura las columnas de la tabla.
        private void ConfigurarTabla()
        {
            grdTiposViatico.AutoGenerateColumns = false;
            grdTiposViatico.Columns.Clear();
            grdTiposViatico.ReadOnly = true;
            grdTiposViatico.AllowUserToAddRows = false;
            grdTiposViatico.AllowUserToDeleteRows = false;
            grdTiposViatico.MultiSelect = false;
            grdTiposViatico.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            string[] propiedades = { "id", "nombre", "monto", "activo" };
            string[] titulos = { "Identificador", "Tipo de viático", "Monto (₡)", "Estado" };
            for (int i = 0; i < propiedades.Length; i++)
            {
                DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn();
                columna.Name = propiedades[i];
                columna.DataPropertyName = propiedades[i];
                columna.HeaderText = titulos[i];
                grdTiposViatico.Columns.Add(columna);
            }
            grdTiposViatico.Columns["monto"].DefaultCellStyle.Format = "N0";
            grdTiposViatico.Columns["monto"].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("es-CR");

            grdTiposViatico.CellFormatting += new DataGridViewCellFormattingEventHandler(grdTiposViatico_CellFormatting);
        }

        // Muestra el estado del registro con un texto legible.
        private void grdTiposViatico_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && grdTiposViatico.Columns[e.ColumnIndex].Name == "activo" && e.Value is bool)
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

        // Actualiza los registros de la tabla.
        private void ActualizarTabla()
        {
            grdTiposViatico.DataSource = logicaTipos.ObtenerTiposViatico();
            grdTiposViatico.ClearSelection();
        }

        // Comprueba y obtiene un identificador positivo.
        private bool LeerIdentificador(out int identificador)
        {
            if (!int.TryParse(txtIdentificador.Text, out identificador) || identificador <= 0)
            {
                MessageBox.Show("El identificador debe ser un número entero positivo.");
                txtIdentificador.Focus();
                return false;
            }
            return true;
        }

        // Limpia los campos para ingresar otro registro.
        private void LimpiarCampos()
        {
            txtIdentificador.Clear();
            txtNombre.Clear();
            txtMonto.Clear();
            cmbEstado.SelectedIndex = 0;
            txtIdentificador.Focus();
        }
    }
}
