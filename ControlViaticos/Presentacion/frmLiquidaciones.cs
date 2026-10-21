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
        }

        //Instancia de la lógica para trabajar con las liquidaciones y sus detalles.
        LogicaLiquidaciones almacen = new LogicaLiquidaciones();
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtConsecutivo.Text.Equals(""))
            {
                MessageBox.Show("Debe escribir el consecutivo", "AVISO",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int idLiquidacion = int.Parse(txtConsecutivo.Text);
                Liquidacion encontrado = almacen.buscarLiquidacion(idLiquidacion);

                if (encontrado == null)
                {
                    MessageBox.Show("No existe una liquidacion con ese consecutivo", "AVISO",
                       MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    //--Se pasan los datos guardados a la pantalla
                    cmbEmpleado.SelectedIndex = encontrado.idEmpleado;
                    dtpInicio.Value = encontrado.inicio;
                    dtpFin.Value = encontrado.fin;
                    txtJustificacion.Text = encontrado.justificacion;

                    grdDetalle.DataSource = almacen.ObtenerDetalles(idLiquidacion);
                    lblMontoTotal.Text = "₡ " + almacen.calcularTotal(idLiquidacion);
                }
            }
        }

        private void btnAgregarDetalle_Click(object sender, EventArgs e)
        {
            string error = "";

            if (txtConsecutivo.Text.Equals(""))
            {
                error = error + " -Consecutivo- ";
            }
            if (cmbTipoViatico.SelectedIndex == -1)
            {
                error = error + " -Tipo de viatico- ";
            }

            if (!error.Equals(""))
            {
                MessageBox.Show("Debe completar los campos de: " + error, "AVISO",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (almacen.buscarLiquidacion(int.Parse(txtConsecutivo.Text)) == null)
            {
                MessageBox.Show("Primero guarde la liquidacion", "AVISO",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int idLiquidacion = int.Parse(txtConsecutivo.Text);
                Liquidacion miLiquidacion = almacen.buscarLiquidacion(idLiquidacion);

                if (!almacen.cumpleHorario(cmbTipoViatico.Text, miLiquidacion.inicio, miLiquidacion.fin))
                {
                    MessageBox.Show("El horario del viaje no permite " + cmbTipoViatico.Text, "AVISO",
                       MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DetalleLiquidacion miDetalle = new DetalleLiquidacion();
                    miDetalle.idLiquidacion = idLiquidacion;
                    miDetalle.idViatico = cmbTipoViatico.SelectedIndex;
                    miDetalle.monto = int.Parse(txtMonto.Text);

                    almacen.AgregarDetalle(miDetalle);

                    grdDetalle.DataSource = almacen.ObtenerDetalles(idLiquidacion);
                    lblMontoTotal.Text = "₡ " + almacen.calcularTotal(idLiquidacion);
                }
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

                almacen.eliminarDetalle(seleccionado.idLiquidacion, seleccionado.idViatico);

                grdDetalle.DataSource = almacen.ObtenerDetalles(seleccionado.idLiquidacion);
                lblMontoTotal.Text = "₡ " + almacen.calcularTotal(seleccionado.idLiquidacion);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string error = "";

            if (txtConsecutivo.Text.Equals(""))
            {
                error = error + " -Consecutivo- ";
            }
            if (cmbEmpleado.SelectedIndex == -1)
            {
                error = error + " -Empleado- ";
            }
            if (txtJustificacion.Text.Equals(""))
            {
                error = error + " -Justificacion- ";
            }

            if (!error.Equals(""))
            {
                MessageBox.Show("Debe completar los campos de: " + error, "AVISO",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (dtpFin.Value <= dtpInicio.Value)
            {
                MessageBox.Show("La fecha de fin debe ser posterior al inicio", "AVISO",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Liquidacion miLiquidacion = new Liquidacion();
                miLiquidacion.id = int.Parse(txtConsecutivo.Text);
                miLiquidacion.idEmpleado = cmbEmpleado.SelectedIndex;
                miLiquidacion.inicio = dtpInicio.Value;
                miLiquidacion.fin = dtpFin.Value;
                miLiquidacion.justificacion = txtJustificacion.Text;

                if (almacen.buscarLiquidacion(miLiquidacion.id) == null)
                {
                    almacen.AgregarLiquidacion(miLiquidacion);
                    MessageBox.Show("Liquidacion guardada", "AVISO",
                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    almacen.modificarLiquidacion(miLiquidacion);
                    MessageBox.Show("Liquidacion modificada", "AVISO",
                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                grdLiquidaciones.DataSource = almacen.ObtenerLiquidaciones();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtConsecutivo.Text.Equals(""))
            {
                MessageBox.Show("Debe escribir el consecutivo", "AVISO",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (almacen.buscarLiquidacion(int.Parse(txtConsecutivo.Text)) == null)
            {
                MessageBox.Show("No existe una liquidacion con ese consecutivo", "AVISO",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (MessageBox.Show("¿Eliminar la liquidacion y todos sus detalles?", "CONFIRMAR",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                almacen.eliminarLiquidacion(int.Parse(txtConsecutivo.Text));

                //--Se limpia la pantalla
                txtConsecutivo.Text = "";
                txtJustificacion.Text = "";
                cmbEmpleado.SelectedIndex = -1;
                grdDetalle.DataSource = null;
                lblMontoTotal.Text = "₡ 0";

                grdLiquidaciones.DataSource = almacen.ObtenerLiquidaciones();
            }
        }

        private void lblMonto_Click(object sender, EventArgs e)
        {

        }

        private void frmLiquidaciones_Load(object sender, EventArgs e)
        {

        }

        private void cmbTipoViatico_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Precios de prueba
            if (cmbTipoViatico.Text.Equals("Desayuno"))
            {
                txtMonto.Text = "3000";
            }
            else if (cmbTipoViatico.Text.Equals("Almuerzo"))
            {
                txtMonto.Text = "7000";
            }
            else if (cmbTipoViatico.Text.Equals("Cena"))
            {
                txtMonto.Text = "10000";
            }
        }
    
    }
}       
