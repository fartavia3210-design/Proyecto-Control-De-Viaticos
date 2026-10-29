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
    public partial class frmBitacora : Form
    {
        private LogicaBitacora logicaBitacora = new LogicaBitacora();

        // Inicializa los controles del formulario.
        public frmBitacora()
        {
            InitializeComponent();
            ConfigurarTabla();
            this.Load += new EventHandler(frmBitacora_Load);
        }

        // Prepara los datos y permisos al abrir el formulario.
        private void frmBitacora_Load(object sender, EventArgs e)
        {
            if (!SesionActual.HaySesionActiva())
            {
                MessageBox.Show("Debe iniciar sesión para consultar la bitácora.");
                Close();
                return;
            }

            grdBitacora.DataSource = logicaBitacora.ObtenerBitacora();
            grdBitacora.ClearSelection();
        }

        // Configura las columnas de la tabla.
        private void ConfigurarTabla()
        {
            grdBitacora.AutoGenerateColumns = false;
            grdBitacora.Columns.Clear();
            grdBitacora.ReadOnly = true;
            grdBitacora.AllowUserToAddRows = false;
            grdBitacora.AllowUserToDeleteRows = false;
            grdBitacora.MultiSelect = false;
            grdBitacora.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            string[] propiedades = { "nombreUsuario", "fechaHora", "seccion", "movimiento" };
            string[] titulos = { "Usuario", "Fecha y hora", "Sección", "Movimiento" };
            for (int i = 0; i < propiedades.Length; i++)
            {
                DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn();
                columna.Name = propiedades[i];
                columna.DataPropertyName = propiedades[i];
                columna.HeaderText = titulos[i];
                grdBitacora.Columns.Add(columna);
            }

            grdBitacora.Columns["fechaHora"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            grdBitacora.Columns["movimiento"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdBitacora.Columns["movimiento"].MinimumWidth = 220;
        }
    }
}
