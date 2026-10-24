using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControlViaticos.Objetos;
using ControlViaticos.Logica;

namespace ControlViaticos.Presentacion
{
    public partial class frmLiquidaciones : Form
    {
        public frmLiquidaciones()
        {
            InitializeComponent();
            ConfigurarTablas();
        }

        //Instancia de la lógica para trabajar con las liquidaciones y sus detalles.
        LogicaLiquidaciones almacen = new LogicaLiquidaciones();
        LogicaBitacora logicaBitacora = new LogicaBitacora();

        //--Detalles que se preparan en pantalla; solo pasan a la lista compartida al Guardar
        List<DetalleLiquidacion> detallesPreparados = new List<DetalleLiquidacion>();

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!ValidarSesion())
            {
                return;
            }

            int idLiquidacion;
            if (!int.TryParse(txtConsecutivo.Text, out idLiquidacion) || idLiquidacion <= 0)
            {
                MessageBox.Show("El consecutivo debe ser un número entero positivo", "AVISO",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Liquidacion encontrado = almacen.buscarLiquidacion(idLiquidacion);
            if (encontrado == null)
            {
                MessageBox.Show("No existe una liquidacion con ese consecutivo", "AVISO",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //--Si el empleado ya está inactivo se agrega al combo para poder mostrar la liquidación
            List<Empleado> empleados = almacen.ObtenerEmpleadosActivos();
            Empleado empleado = almacen.buscarEmpleado(encontrado.idEmpleado);
            if (empleado != null && !empleados.Contains(empleado))
            {
                empleados.Add(empleado);
            }
            cmbEmpleado.DataSource = empleados;
            cmbEmpleado.SelectedItem = empleado;

            //--Se pasan los datos guardados a la pantalla
            dtpInicio.Value = encontrado.inicio;
            dtpFin.Value = encontrado.fin;
            txtJustificacion.Text = encontrado.justificacion;

            detallesPreparados = almacen.ObtenerDetalles(idLiquidacion);
            MostrarDetalles();
        }

        private void btnAgregarDetalle_Click(object sender, EventArgs e)
        {
            TipoViatico tipo = cmbTipoViatico.SelectedItem as TipoViatico;
            if (tipo == null)
            {
                MessageBox.Show("Debe seleccionar el tipo de viatico", "AVISO",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (dtpFechaConsumo.Value.Date < dtpInicio.Value.Date ||
                     dtpFechaConsumo.Value.Date > dtpFin.Value.Date)
            {
                MessageBox.Show("La fecha de consumo debe estar entre el inicio y el fin del viaje", "AVISO",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!almacen.cumpleHorario(tipo.nombre, dtpInicio.Value, dtpFin.Value, dtpFechaConsumo.Value))
            {
                MessageBox.Show("El horario del viaje no permite " + tipo.nombre + " ese día", "AVISO",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //--id 0 indica un detalle nuevo; la lógica le asigna su id al guardar
                DetalleLiquidacion miDetalle = new DetalleLiquidacion();
                miDetalle.idViatico = tipo.id;
                miDetalle.monto = tipo.monto;
                miDetalle.fechaConsumo = dtpFechaConsumo.Value.Date;

                detallesPreparados.Add(miDetalle);
                MostrarDetalles();
            }
        }

        private void btnQuitarDetalle_Click(object sender, EventArgs e)
        {
            if (grdDetalle.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un detalle de la tabla", "AVISO",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //--El objeto que esta detras de la fila seleccionada
                DetalleLiquidacion seleccionado = (DetalleLiquidacion)grdDetalle.CurrentRow.DataBoundItem;

                detallesPreparados.Remove(seleccionado);
                MostrarDetalles();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarSesion())
            {
                return;
            }

            string error = "";
            int idLiquidacion;

            if (!int.TryParse(txtConsecutivo.Text, out idLiquidacion) || idLiquidacion <= 0)
            {
                error = error + " -Consecutivo- ";
            }
            if (cmbEmpleado.SelectedItem == null)
            {
                error = error + " -Empleado- ";
            }
            if (txtJustificacion.Text.Trim().Equals(""))
            {
                error = error + " -Justificacion- ";
            }

            if (!error.Equals(""))
            {
                MessageBox.Show("Debe completar los campos de: " + error, "AVISO",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Liquidacion miLiquidacion = new Liquidacion();
            miLiquidacion.id = idLiquidacion;
            miLiquidacion.idEmpleado = ((Empleado)cmbEmpleado.SelectedItem).id;
            miLiquidacion.inicio = dtpInicio.Value;
            miLiquidacion.fin = dtpFin.Value;
            miLiquidacion.justificacion = txtJustificacion.Text;

            bool esNuevo = almacen.buscarLiquidacion(idLiquidacion) == null;
            bool guardado;
            if (esNuevo)
            {
                guardado = almacen.AgregarLiquidacion(miLiquidacion, detallesPreparados);
            }
            else
            {
                guardado = almacen.modificarLiquidacion(miLiquidacion, detallesPreparados);
            }

            if (!guardado)
            {
                MessageBox.Show(almacen.ultimoError, "AVISO",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (esNuevo)
            {
                logicaBitacora.RegistrarOperacion(SesionActual.usuarioActual, "Liquidaciones",
                    "Creó liquidación " + idLiquidacion);
                MessageBox.Show("Liquidacion guardada", "AVISO",
                   MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                logicaBitacora.RegistrarOperacion(SesionActual.usuarioActual, "Liquidaciones",
                    "Modificó liquidación " + idLiquidacion);
                MessageBox.Show("Liquidacion modificada", "AVISO",
                   MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            int idLiquidacion;
            if (!int.TryParse(txtConsecutivo.Text, out idLiquidacion) || idLiquidacion <= 0)
            {
                MessageBox.Show("El consecutivo debe ser un número entero positivo", "AVISO",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (almacen.buscarLiquidacion(idLiquidacion) == null)
            {
                MessageBox.Show("No existe una liquidacion con ese consecutivo", "AVISO",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (MessageBox.Show("¿Eliminar la liquidacion y todos sus detalles?", "CONFIRMAR",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                almacen.eliminarLiquidacion(idLiquidacion);

                logicaBitacora.RegistrarOperacion(SesionActual.usuarioActual, "Liquidaciones",
                    "Eliminó liquidación " + idLiquidacion);

                ActualizarTabla();
                LimpiarCampos();
            }
        }

        private void lblMonto_Click(object sender, EventArgs e)
        {

        }

        private void frmLiquidaciones_Load(object sender, EventArgs e)
        {
            if (ValidarSesion())
            {
                LimpiarCampos();
                ActualizarTabla();
            }
        }

        private void cmbTipoViatico_SelectedIndexChanged(object sender, EventArgs e)
        {
            //--Monto actual del catalogo
            TipoViatico tipo = cmbTipoViatico.SelectedItem as TipoViatico;
            if (tipo != null)
            {
                txtMonto.Text = tipo.monto.ToString();
            }
            else
            {
                txtMonto.Text = "";
            }
        }

        private void cmbEmpleado_Format(object sender, ListControlConvertEventArgs e)
        {
            Empleado empleado = e.ListItem as Empleado;
            if (empleado != null)
            {
                e.Value = empleado.nombre + " " + empleado.apellido1 + " " + empleado.apellido2;
            }
        }

        private void ConfigurarTablas()
        {
            cmbEmpleado.Format += new ListControlConvertEventHandler(cmbEmpleado_Format);
            cmbTipoViatico.DisplayMember = "nombre";

            grdDetalle.AutoGenerateColumns = false;
            grdDetalle.Columns.Clear();
            grdDetalle.MultiSelect = false;
            grdDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            string[] propiedades = { "fechaConsumo", "idViatico", "monto" };
            string[] titulos = { "Fecha", "Tipo de viático", "Monto" };
            for (int i = 0; i < propiedades.Length; i++)
            {
                DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn();
                columna.Name = propiedades[i];
                columna.DataPropertyName = propiedades[i];
                columna.HeaderText = titulos[i];
                grdDetalle.Columns.Add(columna);
            }
            grdDetalle.Columns["fechaConsumo"].DefaultCellStyle.Format = "dd/MM/yyyy";

            grdLiquidaciones.AutoGenerateColumns = false;
            grdLiquidaciones.Columns.Clear();
            grdLiquidaciones.MultiSelect = false;
            grdLiquidaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            string[] propiedadesLiq = { "id", "idEmpleado", "inicio", "fin", "justificacion" };
            string[] titulosLiq = { "Consecutivo", "Empleado", "Inicio", "Fin", "Justificación" };
            for (int i = 0; i < propiedadesLiq.Length; i++)
            {
                DataGridViewTextBoxColumn columna = new DataGridViewTextBoxColumn();
                columna.Name = propiedadesLiq[i];
                columna.DataPropertyName = propiedadesLiq[i];
                columna.HeaderText = titulosLiq[i];
                grdLiquidaciones.Columns.Add(columna);
            }
            grdLiquidaciones.Columns["inicio"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            grdLiquidaciones.Columns["fin"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            grdDetalle.CellFormatting += new DataGridViewCellFormattingEventHandler(grdDetalle_CellFormatting);
            grdLiquidaciones.CellFormatting += new DataGridViewCellFormattingEventHandler(grdLiquidaciones_CellFormatting);
        }

        //--Se muestra el nombre en lugar del id, sin cambiar el id guardado
        private void grdDetalle_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && grdDetalle.Columns[e.ColumnIndex].Name == "idViatico" && e.Value is int)
            {
                TipoViatico tipo = almacen.buscarTipoViatico((int)e.Value);
                if (tipo != null)
                {
                    e.Value = tipo.nombre;
                    e.FormattingApplied = true;
                }
            }
        }

        private void grdLiquidaciones_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && grdLiquidaciones.Columns[e.ColumnIndex].Name == "idEmpleado" && e.Value is int)
            {
                Empleado empleado = almacen.buscarEmpleado((int)e.Value);
                if (empleado != null)
                {
                    e.Value = empleado.nombre + " " + empleado.apellido1 + " " + empleado.apellido2;
                    e.FormattingApplied = true;
                }
            }
        }

        private void MostrarDetalles()
        {
            grdDetalle.DataSource = null;
            grdDetalle.DataSource = detallesPreparados;
            lblMontoTotal.Text = "₡ " + almacen.calcularTotal(detallesPreparados);
        }

        private void ActualizarTabla()
        {
            grdLiquidaciones.DataSource = almacen.ObtenerLiquidaciones();
            grdLiquidaciones.ClearSelection();
        }

        //--Deja la pantalla lista para una liquidación nueva, con los catálogos activos
        private void LimpiarCampos()
        {
            txtConsecutivo.Text = "";
            txtJustificacion.Text = "";
            cmbEmpleado.DataSource = almacen.ObtenerEmpleadosActivos();
            cmbEmpleado.SelectedIndex = -1;
            cmbTipoViatico.DataSource = almacen.ObtenerTiposActivos();
            cmbTipoViatico.SelectedIndex = -1;
            dtpFechaConsumo.Value = DateTime.Today;
            detallesPreparados = new List<DetalleLiquidacion>();
            MostrarDetalles();
        }

        private bool ValidarSesion()
        {
            if (!SesionActual.HaySesionActiva())
            {
                MessageBox.Show("Debe iniciar sesión para trabajar con liquidaciones.");
                Close();
                return false;
            }
            return true;
        }
    }
}
