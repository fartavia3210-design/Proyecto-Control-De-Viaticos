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
    public partial class frmReporteConsumosEmpleado : Form
    {
        private LogicaReportesConsumo logicaReportes = new LogicaReportesConsumo();

        public frmReporteConsumosEmpleado()
        {
            InitializeComponent();
            lblMontoTotal.AutoSize = true;
            Control campoEmpleado = DisenoFormularios.Campo(lblEmpleado, cmbEmpleado);
            Control filaCampos = DisenoFormularios.Fila(campoEmpleado);
            Control filaAcciones = DisenoFormularios.Fila(btnConsultar);
            Control filaTotal = DisenoFormularios.Fila(lblTotal, lblMontoTotal);
            DisenoFormularios.Pagina(this, lblTitulo, filaCampos, filaAcciones, grdConsumos, filaTotal);
            ConfigurarTabla();
            this.Load += new EventHandler(frmReporteConsumosEmpleado_Load);
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (!ValidarSesion())
            {
                return;
            }

            // No conservar resultados anteriores si el filtro no es válido.
            grdConsumos.DataSource = null;
            lblMontoTotal.Text = "₡ 0";

            Empleado seleccionado = cmbEmpleado.SelectedItem as Empleado;
            if (seleccionado == null)
            {
                MessageBox.Show("Debe seleccionar un empleado para consultar sus consumos.");
                cmbEmpleado.Focus();
                return;
            }

            List<ConsumoReporte> resultado = logicaReportes.ConsultarConsumosPorEmpleado(seleccionado.id);
            grdConsumos.DataSource = resultado;
            grdConsumos.ClearSelection();

            long total = logicaReportes.CalcularTotal(resultado);
            lblMontoTotal.Text = "₡ " + total.ToString("N0", CultureInfo.GetCultureInfo("es-CR"));

            if (resultado.Count == 0)
            {
                MessageBox.Show("Ese empleado no tiene consumos registrados.");
            }
        }

        private void frmReporteConsumosEmpleado_Load(object sender, EventArgs e)
        {
            if (!ValidarSesion())
            {
                return;
            }

            LogicaEmpleados logicaEmpleados = new LogicaEmpleados();
            cmbEmpleado.DisplayMember = "descripcion";
            cmbEmpleado.ValueMember = "id";
            // También se incluyen empleados inactivos para consultar su historial.
            cmbEmpleado.DataSource = logicaEmpleados.ObtenerEmpleados();
            cmbEmpleado.SelectedIndex = -1;
        }

        private bool ValidarSesion()
        {
            if (!SesionActual.HaySesionActiva())
            {
                MessageBox.Show("Debe iniciar sesión para consultar los consumos.");
                Close();
                return false;
            }
            return true;
        }

        private void ConfigurarTabla()
        {
            grdConsumos.AutoGenerateColumns = false;
            grdConsumos.Columns.Clear();
            grdConsumos.ReadOnly = true;
            grdConsumos.AllowUserToAddRows = false;
            grdConsumos.AllowUserToDeleteRows = false;
            grdConsumos.MultiSelect = false;
            grdConsumos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            string[] propiedades = { "fechaConsumo", "nombreViatico", "monto" };
            string[] titulos = { "Fecha", "Tipo de viático", "Monto (₡)" };
            for (int i = 0; i < propiedades.Length; i++)
            {
                DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn();
                columna.Name = propiedades[i];
                columna.DataPropertyName = propiedades[i];
                columna.HeaderText = titulos[i];
                grdConsumos.Columns.Add(columna);
            }

            grdConsumos.Columns["fechaConsumo"].DefaultCellStyle.Format = "dd/MM/yyyy";
            grdConsumos.Columns["monto"].DefaultCellStyle.Format = "N0";
            grdConsumos.Columns["monto"].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("es-CR");
        }
    }
}
