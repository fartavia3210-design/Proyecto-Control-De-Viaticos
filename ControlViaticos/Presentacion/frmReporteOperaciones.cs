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
    public partial class frmReporteOperaciones : Form
    {
        private LogicaBitacora logicaBitacora = new LogicaBitacora();

        // Inicializa los controles del formulario.
        public frmReporteOperaciones()
        {
            InitializeComponent();
            ConfigurarTabla();
            this.Load += new EventHandler(frmReporteOperaciones_Load);
        }

        // Consulta y muestra el reporte y su total.
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (!ValidarSesion())
            {
                return;
            }

            // No conservar resultados anteriores si los nuevos filtros no son válidos.
            grdOperaciones.DataSource = null;
            Usuario seleccionado = cmbUsuario.SelectedItem as Usuario;
            if (seleccionado == null)
            {
                MessageBox.Show("Debe seleccionar un usuario para consultar sus operaciones.");
                cmbUsuario.Focus();
                return;
            }

            if (dtpDesde.Value.Date > dtpHasta.Value.Date)
            {
                MessageBox.Show("La fecha inicial debe ser anterior o igual a la fecha final.");
                dtpDesde.Focus();
                return;
            }

            List<RegistroBitacora> resultado = logicaBitacora.ConsultarOperaciones( seleccionado.id, dtpDesde.Value.Date, dtpHasta.Value.Date);
            grdOperaciones.DataSource = resultado;
            grdOperaciones.ClearSelection();

            if (resultado.Count == 0)
            {
                MessageBox.Show("No hay operaciones de ese usuario en el rango de fechas seleccionado.");
            }
        }

        // Prepara los datos y permisos al abrir el formulario.
        private void frmReporteOperaciones_Load(object sender, EventArgs e)
        {
            if (!ValidarSesion())
            {
                return;
            }

            LogicaUsuarios logicaUsuarios = new LogicaUsuarios();
            cmbUsuario.DisplayMember = "nombreUsuario";
            cmbUsuario.ValueMember = "id";
            // También se incluyen cuentas inactivas para consultar su historial.
            cmbUsuario.DataSource = logicaUsuarios.ObtenerUsuarios();
            cmbUsuario.SelectedIndex = -1;
        }

        // Comprueba que haya una sesión activa.
        private bool ValidarSesion()
        {
            if (!SesionActual.HaySesionActiva())
            {
                MessageBox.Show("Debe iniciar sesión para consultar las operaciones.");
                Close();
                return false;
            }
            return true;
        }

        // Configura las columnas de la tabla.
        private void ConfigurarTabla()
        {
            grdOperaciones.AutoGenerateColumns = false;
            grdOperaciones.Columns.Clear();
            grdOperaciones.ReadOnly = true;
            grdOperaciones.AllowUserToAddRows = false;
            grdOperaciones.AllowUserToDeleteRows = false;
            grdOperaciones.MultiSelect = false;
            grdOperaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            string[] propiedades = { "nombreUsuario", "fechaHora", "seccion", "movimiento" };
            string[] titulos = { "Usuario", "Fecha y hora", "Sección", "Movimiento" };
            for (int i = 0; i < propiedades.Length; i++)
            {
                DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn();
                columna.Name = propiedades[i];
                columna.DataPropertyName = propiedades[i];
                columna.HeaderText = titulos[i];
                grdOperaciones.Columns.Add(columna);
            }

            grdOperaciones.Columns["fechaHora"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            grdOperaciones.Columns["movimiento"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdOperaciones.Columns["movimiento"].MinimumWidth = 220;
        }
    }
}
