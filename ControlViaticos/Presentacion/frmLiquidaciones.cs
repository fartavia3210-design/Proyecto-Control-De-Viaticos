using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using ControlViaticos.Objetos;
using ControlViaticos.Logica;

namespace ControlViaticos.Presentacion
{
    public partial class frmLiquidaciones : Form
    {
        private readonly LogicaLiquidaciones almacen = new LogicaLiquidaciones();
        private readonly List<DetalleLiquidacion> detallesPreparados = new List<DetalleLiquidacion>();
        private int? consecutivoEnEdicion;
        private bool preparacionModificada;

        public frmLiquidaciones()
        {
            InitializeComponent();
            FormClosing += frmLiquidaciones_FormClosing;
        }

        private void frmLiquidaciones_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ConfirmarDescartar()) e.Cancel = true;
        }

        private bool ValidarSesion()
        {
            if (SesionActual.HaySesionActiva()) return true;
            Avisar("Debe iniciar sesión.");
            return false;
        }

        private void Avisar(string mensaje) { MessageBox.Show(mensaje, "Liquidaciones", MessageBoxButtons.OK, MessageBoxIcon.Information); }

        private bool LeerConsecutivo(out int id)
        {
            if (int.TryParse(txtConsecutivo.Text, out id) && id > 0) return true;
            Avisar("El consecutivo debe ser un entero positivo.");
            return false;
        }

        private bool ConfirmarDescartar()
        {
            return !preparacionModificada || MessageBox.Show("¿Descartar la preparación sin guardar?", "Liquidaciones",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            int id;
            if (!ValidarSesion() || !LeerConsecutivo(out id)) return;
            Liquidacion encontrada = almacen.buscarLiquidacion(id);
            if (encontrada == null) { Avisar("No existe una liquidación con ese consecutivo."); return; }
            if (!ConfirmarDescartar()) return;
            consecutivoEnEdicion = id;
            CargarEmpleados(encontrada.idEmpleado);
            dtpInicio.Value = encontrada.inicio;
            dtpFin.Value = encontrada.fin;
            txtJustificacion.Text = encontrada.justificacion;
            detallesPreparados.Clear();
            detallesPreparados.AddRange(almacen.ObtenerDetalles(id));
            dtpFechaConsumo.Value = detallesPreparados.Count == 0 ? encontrada.inicio.Date : detallesPreparados[0].fechaConsumo.Date;
            txtConsecutivo.ReadOnly = true;
            ActualizarDetalles();
            preparacionModificada = false;
        }

        private void btnAgregarDetalle_Click(object sender, EventArgs e)
        {
            int id;
            if (!ValidarSesion() || !LeerConsecutivo(out id)) return;
            if (!consecutivoEnEdicion.HasValue && almacen.buscarLiquidacion(id) != null)
            { Avisar("Busque la liquidación existente antes de modificar sus detalles."); return; }
            TipoViatico tipo = cmbTipoViatico.SelectedItem as TipoViatico;
            if (tipo == null || !tipo.activo || tipo.monto <= 0)
            { Avisar("Seleccione un tipo de viático activo con monto positivo."); return; }
            DateTime fecha = dtpFechaConsumo.Value.Date;
            if (dtpFin.Value <= dtpInicio.Value || fecha < dtpInicio.Value.Date ||
                fecha > dtpFin.Value.Date || !almacen.cumpleHorario(tipo.nombre, dtpInicio.Value, dtpFin.Value, fecha))
            { Avisar("Revise la fecha de consumo y el horario permitido para " + tipo.nombre + "."); return; }
            detallesPreparados.Add(new DetalleLiquidacion { idLiquidacion = id, idViatico = tipo.id,
                monto = tipo.monto, fechaConsumo = fecha });
            txtConsecutivo.ReadOnly = true;
            preparacionModificada = true;
            ActualizarDetalles();
        }

        private void btnQuitarDetalle_Click(object sender, EventArgs e)
        {
            if (!ValidarSesion()) return;
            DetalleLiquidacion detalle = grdDetalle.CurrentRow == null ? null : grdDetalle.CurrentRow.DataBoundItem as DetalleLiquidacion;
            if (detalle == null) { Avisar("Seleccione el detalle que desea quitar."); return; }
            // Elimina exactamente la fila elegida, incluso cuando se repite un tipo.
            detallesPreparados.Remove(detalle);
            preparacionModificada = true;
            ActualizarDetalles();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int id;
            if (!ValidarSesion() || !LeerConsecutivo(out id)) return;
            Empleado empleado = cmbEmpleado.SelectedItem as Empleado;
            if (empleado == null) { Avisar("Seleccione un empleado. Si no hay opciones, cargue los catálogos desde Carga inicial."); return; }
            if (!consecutivoEnEdicion.HasValue && almacen.buscarLiquidacion(id) != null)
            { Avisar("Busque la liquidación existente antes de modificarla."); return; }
            Liquidacion liquidacion = new Liquidacion { id = id, idEmpleado = empleado.id,
                inicio = dtpInicio.Value, fin = dtpFin.Value, justificacion = txtJustificacion.Text };
            bool modificar = consecutivoEnEdicion.HasValue;
            bool guardada = modificar ? almacen.modificarLiquidacion(liquidacion, detallesPreparados) : almacen.AgregarLiquidacion(liquidacion, detallesPreparados);
            if (!guardada) { Avisar(almacen.ultimoError); return; }
            new LogicaBitacora().RegistrarOperacion(SesionActual.usuarioActual, "Liquidaciones",
                (modificar ? "Modificó" : "Creó") + " liquidación " + id);
            ActualizarListado();
            LimpiarPreparacion();
            Avisar("Liquidación guardada con todos sus detalles.");
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int id;
            if (!ValidarSesion() || !LeerConsecutivo(out id)) return;
            if (almacen.buscarLiquidacion(id) == null) { Avisar("La liquidación no existe."); return; }
            if (MessageBox.Show("¿Eliminar la liquidación y todos sus detalles guardados?", "Liquidaciones",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            if (!almacen.eliminarLiquidacion(id)) { Avisar(almacen.ultimoError); return; }
            new LogicaBitacora().RegistrarOperacion(SesionActual.usuarioActual, "Liquidaciones", "Eliminó liquidación " + id);
            LimpiarPreparacion();
            ActualizarListado();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (ConfirmarDescartar()) LimpiarPreparacion();
        }

        private void LimpiarPreparacion()
        {
            consecutivoEnEdicion = null;
            detallesPreparados.Clear();
            txtConsecutivo.ReadOnly = false;
            txtConsecutivo.Clear();
            txtJustificacion.Clear();
            CargarEmpleados(null);
            cmbTipoViatico.SelectedIndex = -1;
            dtpInicio.Value = DateTime.Now;
            dtpFin.Value = dtpInicio.Value;
            dtpFechaConsumo.Value = dtpInicio.Value.Date;
            ActualizarDetalles();
            preparacionModificada = false;
        }

        private void CargarEmpleados(int? seleccionado)
        {
            List<Empleado> empleados = almacen.ObtenerEmpleados().FindAll(e => e.activo || e.id == seleccionado);
            cmbEmpleado.DataSource = empleados;
            cmbEmpleado.DisplayMember = "descripcion";
            cmbEmpleado.ValueMember = "id";
            if (seleccionado.HasValue) cmbEmpleado.SelectedValue = seleccionado.Value;
            else cmbEmpleado.SelectedIndex = -1;
        }

        private void frmLiquidaciones_Load(object sender, EventArgs e)
        {
            if (!ValidarSesion()) { Close(); return; }
            ConfigurarTabla(grdLiquidaciones);
            AgregarColumna(grdLiquidaciones, "id", "Consecutivo");
            AgregarColumna(grdLiquidaciones, "idEmpleado", "Empleado");
            AgregarColumna(grdLiquidaciones, "inicio", "Inicio", "dd/MM/yyyy HH:mm");
            AgregarColumna(grdLiquidaciones, "fin", "Fin", "dd/MM/yyyy HH:mm");
            AgregarColumna(grdLiquidaciones, "justificacion", "Justificación");
            grdLiquidaciones.CellFormatting += FormatearLiquidacion;
            ConfigurarTabla(grdDetalle);
            AgregarColumna(grdDetalle, "idViatico", "Tipo de viático");
            AgregarColumna(grdDetalle, "fechaConsumo", "Fecha de consumo", "dd/MM/yyyy");
            AgregarColumna(grdDetalle, "monto", "Monto (₡)", "N0");
            grdDetalle.CellFormatting += FormatearDetalle;
            cmbTipoViatico.DisplayMember = "nombre";
            cmbTipoViatico.ValueMember = "id";
            cmbTipoViatico.DataSource = almacen.ObtenerTiposViatico().FindAll(t => t.activo);
            LimpiarPreparacion();
            ActualizarListado();
            txtJustificacion.TextChanged += MarcarCambio;
            cmbEmpleado.SelectedIndexChanged += MarcarCambio;
            dtpInicio.ValueChanged += InicioCambiado;
            dtpFin.ValueChanged += MarcarCambio;
        }

        private void MarcarCambio(object sender, EventArgs e) { preparacionModificada = true; }
        private void InicioCambiado(object sender, EventArgs e)
        {
            if (detallesPreparados.Count == 0 ||
                dtpFechaConsumo.Value.Date < dtpInicio.Value.Date ||
                dtpFechaConsumo.Value.Date > dtpFin.Value.Date)
                dtpFechaConsumo.Value = dtpInicio.Value.Date;
            preparacionModificada = true;
        }
        private void ActualizarListado() { grdLiquidaciones.DataSource = almacen.ObtenerLiquidaciones(); }
        private void ActualizarDetalles()
        {
            grdDetalle.DataSource = null;
            grdDetalle.DataSource = new List<DetalleLiquidacion>(detallesPreparados);
            long total = 0;
            foreach (DetalleLiquidacion d in detallesPreparados) total += d.monto;
            lblMontoTotal.Text = "₡ " + total.ToString("N0", CultureInfo.GetCultureInfo("es-CR"));
        }
        private void cmbTipoViatico_SelectedIndexChanged(object sender, EventArgs e)
        {
            TipoViatico tipo = cmbTipoViatico.SelectedItem as TipoViatico;
            txtMonto.Text = tipo == null ? "" : tipo.monto.ToString("N0");
        }
        private static void ConfigurarTabla(DataGridView tabla)
        {
            tabla.AutoGenerateColumns = false;
            tabla.Columns.Clear();
            tabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            tabla.RowHeadersVisible = false;
            tabla.MultiSelect = false;
        }
        private static void AgregarColumna(DataGridView tabla, string propiedad, string titulo, string formato = "")
        {
            tabla.Columns.Add(new DataGridViewTextBoxColumn { Name = propiedad, DataPropertyName = propiedad,
                HeaderText = titulo, MinimumWidth = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = formato } });
        }
        private void FormatearLiquidacion(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && grdLiquidaciones.Columns[e.ColumnIndex].Name == "idEmpleado")
            {
                Liquidacion l = grdLiquidaciones.Rows[e.RowIndex].DataBoundItem as Liquidacion;
                if (l != null) { e.Value = almacen.NombreEmpleado(l.idEmpleado); e.FormattingApplied = true; }
            }
        }
        private void FormatearDetalle(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && grdDetalle.Columns[e.ColumnIndex].Name == "idViatico")
            {
                DetalleLiquidacion d = grdDetalle.Rows[e.RowIndex].DataBoundItem as DetalleLiquidacion;
                if (d != null) { e.Value = almacen.NombreViatico(d.idViatico); e.FormattingApplied = true; }
            }
        }
    }
}
