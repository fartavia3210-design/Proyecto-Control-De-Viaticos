namespace ControlViaticos.Presentacion
{
    partial class frmLiquidaciones
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblConsecutivo = new System.Windows.Forms.Label();
            this.txtConsecutivo = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lblEmpleado = new System.Windows.Forms.Label();
            this.cmbEmpleado = new System.Windows.Forms.ComboBox();
            this.lblInicio = new System.Windows.Forms.Label();
            this.dtpInicio = new System.Windows.Forms.DateTimePicker();
            this.lblFin = new System.Windows.Forms.Label();
            this.dtpFin = new System.Windows.Forms.DateTimePicker();
            this.lblJustificacion = new System.Windows.Forms.Label();
            this.txtJustificacion = new System.Windows.Forms.TextBox();
            this.lblDetalle = new System.Windows.Forms.Label();
            this.lblTipoViatico = new System.Windows.Forms.Label();
            this.cmbTipoViatico = new System.Windows.Forms.ComboBox();
            this.lblMonto = new System.Windows.Forms.Label();
            this.txtMonto = new System.Windows.Forms.TextBox();
            this.btnAgregarDetalle = new System.Windows.Forms.Button();
            this.grdDetalle = new System.Windows.Forms.DataGridView();
            this.btnQuitarDetalle = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblMontoTotal = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.grdLiquidaciones = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdDetalle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLiquidaciones)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Tahoma", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(0, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(1140, 80);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Liquidaciones";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblConsecutivo
            // 
            this.lblConsecutivo.AutoSize = true;
            this.lblConsecutivo.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConsecutivo.Location = new System.Drawing.Point(30, 89);
            this.lblConsecutivo.Name = "lblConsecutivo";
            this.lblConsecutivo.Size = new System.Drawing.Size(228, 33);
            this.lblConsecutivo.TabIndex = 1;
            this.lblConsecutivo.Text = "Consecutivo:";
            // 
            // txtConsecutivo
            // 
            this.txtConsecutivo.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConsecutivo.Location = new System.Drawing.Point(340, 85);
            this.txtConsecutivo.Name = "txtConsecutivo";
            this.txtConsecutivo.Size = new System.Drawing.Size(205, 40);
            this.txtConsecutivo.TabIndex = 2;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Location = new System.Drawing.Point(551, 85);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(59, 40);
            this.btnBuscar.TabIndex = 3;
            this.btnBuscar.Text = "...";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lblEmpleado
            // 
            this.lblEmpleado.AutoSize = true;
            this.lblEmpleado.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpleado.Location = new System.Drawing.Point(30, 149);
            this.lblEmpleado.Name = "lblEmpleado";
            this.lblEmpleado.Size = new System.Drawing.Size(171, 33);
            this.lblEmpleado.TabIndex = 4;
            this.lblEmpleado.Text = "Empleado:";
            // 
            // cmbEmpleado
            // 
            this.cmbEmpleado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmpleado.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEmpleado.FormattingEnabled = true;
            this.cmbEmpleado.Location = new System.Drawing.Point(340, 145);
            this.cmbEmpleado.Name = "cmbEmpleado";
            this.cmbEmpleado.Size = new System.Drawing.Size(270, 40);
            this.cmbEmpleado.TabIndex = 5;
            // 
            // lblInicio
            // 
            this.lblInicio.AutoSize = true;
            this.lblInicio.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInicio.Location = new System.Drawing.Point(30, 209);
            this.lblInicio.Name = "lblInicio";
            this.lblInicio.Size = new System.Drawing.Size(133, 33);
            this.lblInicio.TabIndex = 6;
            this.lblInicio.Text = "Inicio:";
            // 
            // dtpInicio
            // 
            this.dtpInicio.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpInicio.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInicio.Location = new System.Drawing.Point(340, 205);
            this.dtpInicio.Name = "dtpInicio";
            this.dtpInicio.Size = new System.Drawing.Size(270, 40);
            this.dtpInicio.TabIndex = 7;
            // 
            // lblFin
            // 
            this.lblFin.AutoSize = true;
            this.lblFin.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFin.Location = new System.Drawing.Point(30, 269);
            this.lblFin.Name = "lblFin";
            this.lblFin.Size = new System.Drawing.Size(120, 33);
            this.lblFin.TabIndex = 8;
            this.lblFin.Text = "Fin:";
            // 
            // dtpFin
            // 
            this.dtpFin.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpFin.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFin.Location = new System.Drawing.Point(340, 265);
            this.dtpFin.Name = "dtpFin";
            this.dtpFin.Size = new System.Drawing.Size(270, 40);
            this.dtpFin.TabIndex = 9;
            // 
            // lblJustificacion
            // 
            this.lblJustificacion.AutoSize = true;
            this.lblJustificacion.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJustificacion.Location = new System.Drawing.Point(640, 89);
            this.lblJustificacion.Name = "lblJustificacion";
            this.lblJustificacion.Size = new System.Drawing.Size(266, 33);
            this.lblJustificacion.TabIndex = 10;
            this.lblJustificacion.Text = "Justificación:";
            // 
            // txtJustificacion
            // 
            this.txtJustificacion.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJustificacion.Location = new System.Drawing.Point(640, 130);
            this.txtJustificacion.Multiline = true;
            this.txtJustificacion.Name = "txtJustificacion";
            this.txtJustificacion.Size = new System.Drawing.Size(480, 175);
            this.txtJustificacion.TabIndex = 11;
            // 
            // lblDetalle
            // 
            this.lblDetalle.AutoSize = true;
            this.lblDetalle.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetalle.Location = new System.Drawing.Point(30, 318);
            this.lblDetalle.Name = "lblDetalle";
            this.lblDetalle.Size = new System.Drawing.Size(475, 38);
            this.lblDetalle.TabIndex = 12;
            this.lblDetalle.Text = "Detalle de la liquidación";
            // 
            // lblTipoViatico
            // 
            this.lblTipoViatico.AutoSize = true;
            this.lblTipoViatico.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoViatico.Location = new System.Drawing.Point(30, 363);
            this.lblTipoViatico.Name = "lblTipoViatico";
            this.lblTipoViatico.Size = new System.Drawing.Size(304, 33);
            this.lblTipoViatico.TabIndex = 13;
            this.lblTipoViatico.Text = "Tipo de viático:";
            // 
            // cmbTipoViatico
            // 
            this.cmbTipoViatico.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoViatico.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipoViatico.FormattingEnabled = true;
            this.cmbTipoViatico.Location = new System.Drawing.Point(340, 359);
            this.cmbTipoViatico.Name = "cmbTipoViatico";
            this.cmbTipoViatico.Size = new System.Drawing.Size(270, 40);
            this.cmbTipoViatico.TabIndex = 14;
            // 
            // lblMonto
            // 
            this.lblMonto.AutoSize = true;
            this.lblMonto.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonto.Location = new System.Drawing.Point(640, 363);
            this.lblMonto.Name = "lblMonto";
            this.lblMonto.Size = new System.Drawing.Size(120, 33);
            this.lblMonto.TabIndex = 15;
            this.lblMonto.Text = "Monto:";
            // 
            // txtMonto
            // 
            this.txtMonto.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMonto.Location = new System.Drawing.Point(740, 359);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.ReadOnly = true;
            this.txtMonto.Size = new System.Drawing.Size(180, 40);
            this.txtMonto.TabIndex = 16;
            // 
            // btnAgregarDetalle
            // 
            this.btnAgregarDetalle.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarDetalle.Location = new System.Drawing.Point(940, 351);
            this.btnAgregarDetalle.Name = "btnAgregarDetalle";
            this.btnAgregarDetalle.Size = new System.Drawing.Size(170, 56);
            this.btnAgregarDetalle.TabIndex = 17;
            this.btnAgregarDetalle.Text = "Agregar";
            this.btnAgregarDetalle.UseVisualStyleBackColor = true;
            this.btnAgregarDetalle.Click += new System.EventHandler(this.btnAgregarDetalle_Click);
            // 
            // grdDetalle
            // 
            this.grdDetalle.AllowUserToAddRows = false;
            this.grdDetalle.AllowUserToDeleteRows = false;
            this.grdDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdDetalle.Location = new System.Drawing.Point(30, 420);
            this.grdDetalle.Name = "grdDetalle";
            this.grdDetalle.ReadOnly = true;
            this.grdDetalle.RowHeadersWidth = 82;
            this.grdDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdDetalle.Size = new System.Drawing.Size(720, 150);
            this.grdDetalle.TabIndex = 18;
            // 
            // btnQuitarDetalle
            // 
            this.btnQuitarDetalle.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuitarDetalle.Location = new System.Drawing.Point(780, 420);
            this.btnQuitarDetalle.Name = "btnQuitarDetalle";
            this.btnQuitarDetalle.Size = new System.Drawing.Size(330, 56);
            this.btnQuitarDetalle.TabIndex = 19;
            this.btnQuitarDetalle.Text = "Quitar detalle";
            this.btnQuitarDetalle.UseVisualStyleBackColor = true;
            this.btnQuitarDetalle.Click += new System.EventHandler(this.btnQuitarDetalle_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(780, 505);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(120, 33);
            this.lblTotal.TabIndex = 20;
            this.lblTotal.Text = "Total:";
            // 
            // lblMontoTotal
            // 
            this.lblMontoTotal.AutoSize = true;
            this.lblMontoTotal.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMontoTotal.Location = new System.Drawing.Point(890, 497);
            this.lblMontoTotal.Name = "lblMontoTotal";
            this.lblMontoTotal.Size = new System.Drawing.Size(120, 48);
            this.lblMontoTotal.TabIndex = 21;
            this.lblMontoTotal.Text = "₡ 0";
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnGuardar.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Location = new System.Drawing.Point(330, 580);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(190, 56);
            this.btnGuardar.TabIndex = 22;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnEliminar.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminar.Location = new System.Drawing.Point(620, 580);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(190, 56);
            this.btnEliminar.TabIndex = 23;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // grdLiquidaciones
            // 
            this.grdLiquidaciones.AllowUserToAddRows = false;
            this.grdLiquidaciones.AllowUserToDeleteRows = false;
            this.grdLiquidaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdLiquidaciones.Location = new System.Drawing.Point(30, 645);
            this.grdLiquidaciones.Name = "grdLiquidaciones";
            this.grdLiquidaciones.ReadOnly = true;
            this.grdLiquidaciones.RowHeadersWidth = 82;
            this.grdLiquidaciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdLiquidaciones.Size = new System.Drawing.Size(1080, 85);
            this.grdLiquidaciones.TabIndex = 24;
            // 
            // frmLiquidaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1140, 727);
            this.Controls.Add(this.grdLiquidaciones);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.lblMontoTotal);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnQuitarDetalle);
            this.Controls.Add(this.grdDetalle);
            this.Controls.Add(this.btnAgregarDetalle);
            this.Controls.Add(this.txtMonto);
            this.Controls.Add(this.lblMonto);
            this.Controls.Add(this.cmbTipoViatico);
            this.Controls.Add(this.lblTipoViatico);
            this.Controls.Add(this.lblDetalle);
            this.Controls.Add(this.txtJustificacion);
            this.Controls.Add(this.lblJustificacion);
            this.Controls.Add(this.dtpFin);
            this.Controls.Add(this.lblFin);
            this.Controls.Add(this.dtpInicio);
            this.Controls.Add(this.lblInicio);
            this.Controls.Add(this.cmbEmpleado);
            this.Controls.Add(this.lblEmpleado);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtConsecutivo);
            this.Controls.Add(this.lblConsecutivo);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmLiquidaciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Liquidaciones";
            ((System.ComponentModel.ISupportInitialize)(this.grdDetalle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLiquidaciones)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblConsecutivo;
        private System.Windows.Forms.TextBox txtConsecutivo;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label lblEmpleado;
        private System.Windows.Forms.ComboBox cmbEmpleado;
        private System.Windows.Forms.Label lblInicio;
        private System.Windows.Forms.DateTimePicker dtpInicio;
        private System.Windows.Forms.Label lblFin;
        private System.Windows.Forms.DateTimePicker dtpFin;
        private System.Windows.Forms.Label lblJustificacion;
        private System.Windows.Forms.TextBox txtJustificacion;
        private System.Windows.Forms.Label lblDetalle;
        private System.Windows.Forms.Label lblTipoViatico;
        private System.Windows.Forms.ComboBox cmbTipoViatico;
        private System.Windows.Forms.Label lblMonto;
        private System.Windows.Forms.TextBox txtMonto;
        private System.Windows.Forms.Button btnAgregarDetalle;
        private System.Windows.Forms.DataGridView grdDetalle;
        private System.Windows.Forms.Button btnQuitarDetalle;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblMontoTotal;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.DataGridView grdLiquidaciones;
    }
}
