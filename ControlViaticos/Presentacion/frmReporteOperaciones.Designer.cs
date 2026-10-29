namespace ControlViaticos.Presentacion
{
    partial class frmReporteOperaciones
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
            this.lblUsuario = new System.Windows.Forms.Label();
            this.cmbUsuario = new System.Windows.Forms.ComboBox();
            this.lblDesde = new System.Windows.Forms.Label();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.lblHasta = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.grdOperaciones = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdOperaciones)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(1140, 80);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Operaciones por usuario";
            // 
            // lblUsuario
            // 
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.TabIndex = 1;
            this.lblUsuario.Text = "Usuario:";
            // 
            // cmbUsuario
            // 
            this.cmbUsuario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUsuario.FormattingEnabled = true;
            this.cmbUsuario.Name = "cmbUsuario";
            this.cmbUsuario.TabIndex = 2;
            // 
            // lblDesde
            // 
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.TabIndex = 3;
            this.lblDesde.Text = "Desde:";
            // 
            // dtpDesde
            // 
            this.dtpDesde.CustomFormat = "dd/MM/yyyy";
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.TabIndex = 4;
            // 
            // lblHasta
            // 
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.TabIndex = 5;
            this.lblHasta.Text = "Hasta:";
            // 
            // dtpHasta
            // 
            this.dtpHasta.CustomFormat = "dd/MM/yyyy";
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.TabIndex = 6;
            // 
            // btnConsultar
            // 
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.TabIndex = 7;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // grdOperaciones
            // 
            this.grdOperaciones.AllowUserToAddRows = false;
            this.grdOperaciones.AllowUserToDeleteRows = false;
            this.grdOperaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdOperaciones.Name = "grdOperaciones";
            this.grdOperaciones.ReadOnly = true;
            this.grdOperaciones.RowHeadersWidth = 82;
            this.grdOperaciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdOperaciones.TabIndex = 8;
            // 
            // frmReporteOperaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmReporteOperaciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Operaciones por usuario";
            ((System.ComponentModel.ISupportInitialize)(this.grdOperaciones)).EndInit();
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
            this.lblUsuario.Location = new System.Drawing.Point(20, 78);
            this.lblUsuario.Size = new System.Drawing.Size(290, 22);
            this.lblUsuario.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblUsuario.AutoSize = true;
            this.cmbUsuario.Location = new System.Drawing.Point(20, 104);
            this.cmbUsuario.Size = new System.Drawing.Size(270, 27);
            this.cmbUsuario.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cmbUsuario.Dock = System.Windows.Forms.DockStyle.None;
            this.cmbUsuario.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.cmbUsuario.DropDownWidth = 440;
            this.lblDesde.Location = new System.Drawing.Point(340, 78);
            this.lblDesde.Size = new System.Drawing.Size(290, 22);
            this.lblDesde.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblDesde.AutoSize = true;
            this.dtpDesde.Location = new System.Drawing.Point(340, 104);
            this.dtpDesde.Size = new System.Drawing.Size(270, 27);
            this.dtpDesde.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dtpDesde.Dock = System.Windows.Forms.DockStyle.None;
            this.dtpDesde.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblHasta.Location = new System.Drawing.Point(20, 152);
            this.lblHasta.Size = new System.Drawing.Size(290, 22);
            this.lblHasta.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblHasta.AutoSize = true;
            this.dtpHasta.Location = new System.Drawing.Point(20, 178);
            this.dtpHasta.Size = new System.Drawing.Size(270, 27);
            this.dtpHasta.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dtpHasta.Dock = System.Windows.Forms.DockStyle.None;
            this.dtpHasta.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.btnConsultar.Location = new System.Drawing.Point(20, 226);
            this.btnConsultar.Size = new System.Drawing.Size(125, 34);
            this.btnConsultar.Font = new System.Drawing.Font("Tahoma", 10F);
            this.grdOperaciones.Location = new System.Drawing.Point(20, 278);
            this.grdOperaciones.Size = new System.Drawing.Size(840, 230);
            this.grdOperaciones.Font = new System.Drawing.Font("Tahoma", 10F);
            this.grdOperaciones.Dock = System.Windows.Forms.DockStyle.None;
            this.grdOperaciones.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.pagina.Controls.Add(this.lblTitulo);
            this.pagina.Controls.Add(this.lblUsuario);
            this.pagina.Controls.Add(this.cmbUsuario);
            this.pagina.Controls.Add(this.lblDesde);
            this.pagina.Controls.Add(this.dtpDesde);
            this.pagina.Controls.Add(this.lblHasta);
            this.pagina.Controls.Add(this.dtpHasta);
            this.pagina.Controls.Add(this.btnConsultar);
            this.pagina.Controls.Add(this.grdOperaciones);
            this.Controls.Add(this.pagina);
            this.grdOperaciones.RowHeadersVisible = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.ComboBox cmbUsuario;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.DataGridView grdOperaciones;
    }
}
