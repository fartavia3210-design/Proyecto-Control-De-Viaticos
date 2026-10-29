namespace ControlViaticos.Presentacion
{
    partial class frmReporteConsumosEmpleado
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
            this.lblEmpleado = new System.Windows.Forms.Label();
            this.cmbEmpleado = new System.Windows.Forms.ComboBox();
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
            this.lblTitulo.Text = "Consumos de un empleado";
            // 
            // lblEmpleado
            // 
            this.lblEmpleado.Name = "lblEmpleado";
            this.lblEmpleado.TabIndex = 1;
            this.lblEmpleado.Text = "Empleado:";
            // 
            // cmbEmpleado
            // 
            this.cmbEmpleado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmpleado.FormattingEnabled = true;
            this.cmbEmpleado.Name = "cmbEmpleado";
            this.cmbEmpleado.TabIndex = 2;
            // 
            // btnConsultar
            // 
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.TabIndex = 3;
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
            this.grdConsumos.TabIndex = 4;
            // 
            // lblTotal
            // 
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.TabIndex = 5;
            this.lblTotal.Text = "Total:";
            // 
            // lblMontoTotal
            // 
            this.lblMontoTotal.Name = "lblMontoTotal";
            this.lblMontoTotal.TabIndex = 6;
            this.lblMontoTotal.Text = "₡ 0";
            // 
            // frmReporteConsumosEmpleado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmReporteConsumosEmpleado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consumos de un empleado";
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
            this.lblEmpleado.Location = new System.Drawing.Point(20, 78);
            this.lblEmpleado.Size = new System.Drawing.Size(290, 22);
            this.lblEmpleado.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblEmpleado.AutoSize = true;
            this.cmbEmpleado.Location = new System.Drawing.Point(20, 104);
            this.cmbEmpleado.Size = new System.Drawing.Size(270, 27);
            this.cmbEmpleado.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cmbEmpleado.Dock = System.Windows.Forms.DockStyle.None;
            this.cmbEmpleado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.cmbEmpleado.DropDownWidth = 440;
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
            this.pagina.Controls.Add(this.lblEmpleado);
            this.pagina.Controls.Add(this.cmbEmpleado);
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
        private System.Windows.Forms.Label lblEmpleado;
        private System.Windows.Forms.ComboBox cmbEmpleado;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.DataGridView grdConsumos;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblMontoTotal;
    }
}
