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
    public partial class frmReporteConsumosFechas : Form
    {
        private LogicaReportesConsumo logicaReportes = new LogicaReportesConsumo();

        // Inicializa los controles del formulario.
        public frmReporteConsumosFechas()
        {
            InitializeComponent();
            lblMontoTotal.AutoSize = true;
            ConfigurarTabla();
            this.Load += new EventHandler(frmReporteConsumosFechas_Load);
        }

        // Consulta y muestra el reporte y su total.
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (!ValidarSesion())
            {
                return;
            }

            // No conservar resultados anteriores si los nuevos filtros no son válidos.
            grdConsumos.DataSource = null;
            lblMontoTotal.Text = "₡ 0";

            if (dtpDesde.Value.Date > dtpHasta.Value.Date)
            {
                MessageBox.Show("La fecha inicial debe ser anterior o igual a la fecha final.");
                dtpDesde.Focus();
                return;
            }

            List<ConsumoReporte> resultado = logicaReportes.ConsultarConsumosPorFechas( dtpDesde.Value.Date, dtpHasta.Value.Date);
            grdConsumos.DataSource = resultado;
            grdConsumos.ClearSelection();

            long total = logicaReportes.CalcularTotal(resultado);
            lblMontoTotal.Text = "₡ " + total.ToString("N0", CultureInfo.GetCultureInfo("es-CR"));

            if (resultado.Count == 0)
            {
                MessageBox.Show("No hay consumos en el rango de fechas seleccionado.");
            }
        }

        // Prepara los datos y permisos al abrir el formulario.
        private void frmReporteConsumosFechas_Load(object sender, EventArgs e)
        {
            ValidarSesion();
        }

        // Comprueba que haya una sesión activa.
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

        // Configura las columnas de la tabla.
        private void ConfigurarTabla()
        {
            grdConsumos.AutoGenerateColumns = false;
            grdConsumos.Columns.Clear();
            grdConsumos.ReadOnly = true;
            grdConsumos.AllowUserToAddRows = false;
            grdConsumos.AllowUserToDeleteRows = false;
            grdConsumos.MultiSelect = false;
            grdConsumos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            string[] propiedades = { "nombreEmpleado", "fechaConsumo", "nombreViatico", "monto" };
            string[] titulos = { "Empleado", "Fecha", "Tipo de viático", "Monto (₡)" };
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
