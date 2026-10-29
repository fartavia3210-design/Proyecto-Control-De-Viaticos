namespace ControlViaticos.Presentacion
{
    partial class frmReporteConsumosFechas
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
            this.lblDesde = new System.Windows.Forms.Label();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.lblHasta = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.grdConsumos = new System.Windows.Forms.DataGridView();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblMontoTotal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdConsumos)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(1140, 80);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Consumos por fechas";
            // 
            // lblDesde
            // 
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.TabIndex = 1;
            this.lblDesde.Text = "Desde:";
            // 
            // dtpDesde
            // 
            this.dtpDesde.CustomFormat = "dd/MM/yyyy";
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.TabIndex = 2;
            // 
            // lblHasta
            // 
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.TabIndex = 3;
            this.lblHasta.Text = "Hasta:";
            // 
            // dtpHasta
            // 
            this.dtpHasta.CustomFormat = "dd/MM/yyyy";
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.TabIndex = 4;
            // 
            // btnConsultar
            // 
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.TabIndex = 5;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // grdConsumos
            // 
            this.grdConsumos.AllowUserToAddRows = false;
            this.grdConsumos.AllowUserToDeleteRows = false;
            this.grdConsumos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdConsumos.Name = "grdConsumos";
            this.grdConsumos.ReadOnly = true;
            this.grdConsumos.RowHeadersWidth = 82;
            this.grdConsumos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdConsumos.TabIndex = 6;
            // 
            // lblTotal
            // 
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.TabIndex = 7;
            this.lblTotal.Text = "Total:";
            // 
            // lblMontoTotal
            // 
            this.lblMontoTotal.Name = "lblMontoTotal";
            this.lblMontoTotal.TabIndex = 8;
            this.lblMontoTotal.Text = "₡ 0";
            // 
            // frmReporteConsumosFechas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmReporteConsumosFechas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consumos por fechas";
            ((System.ComponentModel.ISupportInitialize)(this.grdConsumos)).EndInit();
            // Distribucion manual mediante propiedades de los controles.
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Font = new System.Drawing.Font("Tahoma", 10F);
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.AutoScroll = true;
            this.pagina.Name = "pagina";
            this.pagina.Location = new System.Drawing.Point(0, 0);
            this.pagina.Size = new System.Drawing.Size(880, 529);
            this.pagina.MinimumSize = new System.Drawing.Size(650, 529);
            this.pagina.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(20, 20);
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitulo.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lblDesde.Location = new System.Drawing.Point(20, 78);
            this.lblDesde.Size = new System.Drawing.Size(290, 22);
            this.lblDesde.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblDesde.AutoSize = true;
            this.dtpDesde.Location = new System.Drawing.Point(20, 104);
            this.dtpDesde.Size = new System.Drawing.Size(270, 27);
            this.dtpDesde.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dtpDesde.Dock = System.Windows.Forms.DockStyle.None;
            this.dtpDesde.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblHasta.Location = new System.Drawing.Point(340, 78);
            this.lblHasta.Size = new System.Drawing.Size(290, 22);
            this.lblHasta.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblHasta.AutoSize = true;
            this.dtpHasta.Location = new System.Drawing.Point(340, 104);
            this.dtpHasta.Size = new System.Drawing.Size(270, 27);
            this.dtpHasta.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dtpHasta.Dock = System.Windows.Forms.DockStyle.None;
            this.dtpHasta.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.btnConsultar.Location = new System.Drawing.Point(20, 152);
            this.btnConsultar.Size = new System.Drawing.Size(125, 34);
            this.btnConsultar.Font = new System.Drawing.Font("Tahoma", 10F);
            this.grdConsumos.Location = new System.Drawing.Point(20, 204);
            this.grdConsumos.Size = new System.Drawing.Size(840, 230);
            this.grdConsumos.Font = new System.Drawing.Font("Tahoma", 10F);
            this.grdConsumos.Dock = System.Windows.Forms.DockStyle.None;
            this.grdConsumos.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblTotal.Location = new System.Drawing.Point(20, 454);
            this.lblTotal.Size = new System.Drawing.Size(65, 32);
            this.lblTotal.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblTotal.AutoSize = true;
            this.lblMontoTotal.Location = new System.Drawing.Point(95, 454);
            this.lblMontoTotal.Size = new System.Drawing.Size(400, 32);
            this.lblMontoTotal.AutoSize = true;
            this.pagina.Controls.Add(this.lblTitulo);
            this.pagina.Controls.Add(this.lblDesde);
            this.pagina.Controls.Add(this.dtpDesde);
            this.pagina.Controls.Add(this.lblHasta);
            this.pagina.Controls.Add(this.dtpHasta);
            this.pagina.Controls.Add(this.btnConsultar);
            this.pagina.Controls.Add(this.grdConsumos);
            this.pagina.Controls.Add(this.lblTotal);
            this.pagina.Controls.Add(this.lblMontoTotal);
            this.Controls.Add(this.pagina);
            this.grdConsumos.RowHeadersVisible = false;
            this.lblMontoTotal.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.DataGridView grdConsumos;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblMontoTotal;
    }
}
