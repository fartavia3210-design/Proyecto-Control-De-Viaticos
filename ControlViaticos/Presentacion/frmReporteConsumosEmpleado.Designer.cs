namespace ControlViaticos.Presentacion
{
    partial class frmReporteConsumosEmpleado
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
            this.lblTitulo.Font = new System.Drawing.Font("Tahoma", 28.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(0, 10);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(1140, 80);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Consumos de un empleado";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEmpleado
            // 
            this.lblEmpleado.AutoSize = true;
            this.lblEmpleado.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpleado.Location = new System.Drawing.Point(30, 104);
            this.lblEmpleado.Name = "lblEmpleado";
            this.lblEmpleado.Size = new System.Drawing.Size(171, 33);
            this.lblEmpleado.TabIndex = 1;
            this.lblEmpleado.Text = "Empleado:";
            // 
            // cmbEmpleado
            // 
            this.cmbEmpleado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmpleado.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEmpleado.FormattingEnabled = true;
            this.cmbEmpleado.Location = new System.Drawing.Point(170, 100);
            this.cmbEmpleado.Name = "cmbEmpleado";
            this.cmbEmpleado.Size = new System.Drawing.Size(420, 40);
            this.cmbEmpleado.TabIndex = 2;
            // 
            // btnConsultar
            // 
            this.btnConsultar.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsultar.Location = new System.Drawing.Point(640, 88);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(190, 64);
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
            this.grdConsumos.Location = new System.Drawing.Point(30, 175);
            this.grdConsumos.Name = "grdConsumos";
            this.grdConsumos.ReadOnly = true;
            this.grdConsumos.RowHeadersWidth = 82;
            this.grdConsumos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdConsumos.Size = new System.Drawing.Size(1080, 480);
            this.grdConsumos.TabIndex = 4;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Tahoma", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(760, 680);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(120, 33);
            this.lblTotal.TabIndex = 5;
            this.lblTotal.Text = "Total:";
            // 
            // lblMontoTotal
            // 
            this.lblMontoTotal.AutoSize = true;
            this.lblMontoTotal.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMontoTotal.Location = new System.Drawing.Point(870, 672);
            this.lblMontoTotal.Name = "lblMontoTotal";
            this.lblMontoTotal.Size = new System.Drawing.Size(120, 48);
            this.lblMontoTotal.TabIndex = 6;
            this.lblMontoTotal.Text = "₡ 0";
            // 
            // frmReporteConsumosEmpleado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1140, 727);
            this.Controls.Add(this.lblMontoTotal);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.grdConsumos);
            this.Controls.Add(this.btnConsultar);
            this.Controls.Add(this.cmbEmpleado);
            this.Controls.Add(this.lblEmpleado);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmReporteConsumosEmpleado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consumos de un empleado";
            ((System.ComponentModel.ISupportInitialize)(this.grdConsumos)).EndInit();
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
