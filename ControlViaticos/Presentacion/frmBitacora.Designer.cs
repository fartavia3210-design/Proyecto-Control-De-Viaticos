namespace ControlViaticos.Presentacion
{
    partial class frmBitacora
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
            this.grdBitacora = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdBitacora)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(1140, 80);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Bitácora";
            // 
            // grdBitacora
            // 
            this.grdBitacora.AllowUserToAddRows = false;
            this.grdBitacora.AllowUserToDeleteRows = false;
            this.grdBitacora.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdBitacora.Name = "grdBitacora";
            this.grdBitacora.ReadOnly = true;
            this.grdBitacora.RowHeadersWidth = 82;
            this.grdBitacora.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdBitacora.TabIndex = 1;
            // 
            // frmBitacora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmBitacora";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bitácora";
            ((System.ComponentModel.ISupportInitialize)(this.grdBitacora)).EndInit();
            // Distribucion manual mediante propiedades de los controles.
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Font = new System.Drawing.Font("Tahoma", 10F);
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.AutoScroll = true;
            this.pagina.Name = "pagina";
            this.pagina.Location = new System.Drawing.Point(0, 0);
            this.pagina.Size = new System.Drawing.Size(880, 353);
            this.pagina.MinimumSize = new System.Drawing.Size(650, 353);
            this.pagina.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(20, 20);
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitulo.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.grdBitacora.Location = new System.Drawing.Point(20, 78);
            this.grdBitacora.Size = new System.Drawing.Size(840, 230);
            this.grdBitacora.Font = new System.Drawing.Font("Tahoma", 10F);
            this.grdBitacora.Dock = System.Windows.Forms.DockStyle.None;
            this.grdBitacora.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.pagina.Controls.Add(this.lblTitulo);
            this.pagina.Controls.Add(this.grdBitacora);
            this.Controls.Add(this.pagina);
            this.grdBitacora.RowHeadersVisible = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.DataGridView grdBitacora;
    }
}
