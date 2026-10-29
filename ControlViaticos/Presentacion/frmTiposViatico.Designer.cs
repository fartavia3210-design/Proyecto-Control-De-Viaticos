namespace ControlViaticos.Presentacion
{
    partial class frmTiposViatico
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.Windows.Forms.Panel pagina;

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
            this.pagina = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblIdentificador = new System.Windows.Forms.Label();
            this.txtIdentificador = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblMonto = new System.Windows.Forms.Label();
            this.txtMonto = new System.Windows.Forms.TextBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.grdTiposViatico = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdTiposViatico)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(1140, 80);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Tipos de viático";
            // 
            // lblIdentificador
            // 
            this.lblIdentificador.Name = "lblIdentificador";
            this.lblIdentificador.TabIndex = 1;
            this.lblIdentificador.Text = "Identificador:";
            // 
            // txtIdentificador
            // 
            this.txtIdentificador.Name = "txtIdentificador";
            this.txtIdentificador.TabIndex = 2;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.TabIndex = 3;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lblNombre
            // 
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.TabIndex = 4;
            this.lblNombre.Text = "Nombre o categoría:";
            // 
            // txtNombre
            // 
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.TabIndex = 5;
            // 
            // lblMonto
            // 
            this.lblMonto.Name = "lblMonto";
            this.lblMonto.TabIndex = 6;
            this.lblMonto.Text = "Monto:";
            // 
            // txtMonto
            // 
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.TabIndex = 7;
            // 
            // lblEstado
            // 
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.TabIndex = 8;
            this.lblEstado.Text = "Estado:";
            // 
            // cmbEstado
            // 
            this.cmbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.TabIndex = 9;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.TabIndex = 10;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.TabIndex = 11;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // grdTiposViatico
            // 
            this.grdTiposViatico.AllowUserToAddRows = false;
            this.grdTiposViatico.AllowUserToDeleteRows = false;
            this.grdTiposViatico.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdTiposViatico.Name = "grdTiposViatico";
            this.grdTiposViatico.ReadOnly = true;
            this.grdTiposViatico.RowHeadersWidth = 82;
            this.grdTiposViatico.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdTiposViatico.TabIndex = 12;
            // 
            // frmTiposViatico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmTiposViatico";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tipos de viático";
            ((System.ComponentModel.ISupportInitialize)(this.grdTiposViatico)).EndInit();
            // Distribucion manual mediante propiedades de los controles.
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Font = new System.Drawing.Font("Tahoma", 10F);
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.AutoScroll = true;
            this.pagina.Name = "pagina";
            this.pagina.Location = new System.Drawing.Point(0, 0);
            this.pagina.Size = new System.Drawing.Size(880, 553);
            this.pagina.MinimumSize = new System.Drawing.Size(650, 553);
            this.pagina.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(20, 20);
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitulo.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lblIdentificador.Location = new System.Drawing.Point(20, 78);
            this.lblIdentificador.Size = new System.Drawing.Size(290, 22);
            this.lblIdentificador.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblIdentificador.AutoSize = true;
            this.txtIdentificador.Dock = System.Windows.Forms.DockStyle.None;
            this.txtIdentificador.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.txtIdentificador.Location = new System.Drawing.Point(20, 104);
            this.txtIdentificador.Size = new System.Drawing.Size(185, 27);
            this.txtIdentificador.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnBuscar.Location = new System.Drawing.Point(212, 102);
            this.btnBuscar.Size = new System.Drawing.Size(82, 30);
            this.btnBuscar.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblNombre.Location = new System.Drawing.Point(340, 78);
            this.lblNombre.Size = new System.Drawing.Size(290, 22);
            this.lblNombre.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblNombre.AutoSize = true;
            this.txtNombre.Location = new System.Drawing.Point(340, 104);
            this.txtNombre.Size = new System.Drawing.Size(270, 27);
            this.txtNombre.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtNombre.Dock = System.Windows.Forms.DockStyle.None;
            this.txtNombre.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblMonto.Location = new System.Drawing.Point(20, 152);
            this.lblMonto.Size = new System.Drawing.Size(290, 22);
            this.lblMonto.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblMonto.AutoSize = true;
            this.txtMonto.Location = new System.Drawing.Point(20, 178);
            this.txtMonto.Size = new System.Drawing.Size(270, 27);
            this.txtMonto.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtMonto.Dock = System.Windows.Forms.DockStyle.None;
            this.txtMonto.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblEstado.Location = new System.Drawing.Point(340, 152);
            this.lblEstado.Size = new System.Drawing.Size(290, 22);
            this.lblEstado.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblEstado.AutoSize = true;
            this.cmbEstado.Location = new System.Drawing.Point(340, 178);
            this.cmbEstado.Size = new System.Drawing.Size(270, 27);
            this.cmbEstado.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cmbEstado.Dock = System.Windows.Forms.DockStyle.None;
            this.cmbEstado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.cmbEstado.DropDownWidth = 440;
            this.btnGuardar.Location = new System.Drawing.Point(20, 226);
            this.btnGuardar.Size = new System.Drawing.Size(125, 34);
            this.btnGuardar.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnEliminar.Location = new System.Drawing.Point(160, 226);
            this.btnEliminar.Size = new System.Drawing.Size(125, 34);
            this.btnEliminar.Font = new System.Drawing.Font("Tahoma", 10F);
            this.grdTiposViatico.Location = new System.Drawing.Point(20, 278);
            this.grdTiposViatico.Size = new System.Drawing.Size(840, 230);
            this.grdTiposViatico.Font = new System.Drawing.Font("Tahoma", 10F);
            this.grdTiposViatico.Dock = System.Windows.Forms.DockStyle.None;
            this.grdTiposViatico.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.pagina.Controls.Add(this.lblTitulo);
            this.pagina.Controls.Add(this.lblIdentificador);
            this.pagina.Controls.Add(this.txtIdentificador);
            this.pagina.Controls.Add(this.btnBuscar);
            this.pagina.Controls.Add(this.lblNombre);
            this.pagina.Controls.Add(this.txtNombre);
            this.pagina.Controls.Add(this.lblMonto);
            this.pagina.Controls.Add(this.txtMonto);
            this.pagina.Controls.Add(this.lblEstado);
            this.pagina.Controls.Add(this.cmbEstado);
            this.pagina.Controls.Add(this.btnGuardar);
            this.pagina.Controls.Add(this.btnEliminar);
            this.pagina.Controls.Add(this.grdTiposViatico);
            this.Controls.Add(this.pagina);
            this.btnBuscar.Text = "Buscar";
            this.grdTiposViatico.RowHeadersVisible = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblIdentificador;
        private System.Windows.Forms.TextBox txtIdentificador;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblMonto;
        private System.Windows.Forms.TextBox txtMonto;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.DataGridView grdTiposViatico;
    }
}
