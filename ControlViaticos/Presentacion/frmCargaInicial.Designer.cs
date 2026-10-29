namespace ControlViaticos.Presentacion
{
    partial class frmCargaInicial
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
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lblTiposIniciales = new System.Windows.Forms.Label();
            this.btnEjecutar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(1140, 80);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Carga inicial";
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.TabIndex = 1;
            this.lblDescripcion.Text = "Esta opción inserta los datos iniciales en las listas de usuarios, empleados y tipos de viático. No se ejecuta sola al abrir la aplicación y solo la usa el administrador.";
            // 
            // lblTiposIniciales
            // 
            this.lblTiposIniciales.Name = "lblTiposIniciales";
            this.lblTiposIniciales.TabIndex = 2;
            this.lblTiposIniciales.Text = "Tipos iniciales:  Desayuno ₡3 000 - Almuerzo ₡5 000 - Cena ₡5 000";
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.TabIndex = 3;
            this.btnEjecutar.Text = "Ejecutar carga inicial";
            this.btnEjecutar.UseVisualStyleBackColor = true;
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // frmCargaInicial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCargaInicial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Carga inicial";
            // Distribucion manual mediante propiedades de los controles.
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Font = new System.Drawing.Font("Tahoma", 10F);
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.AutoScroll = true;
            this.pagina.Name = "pagina";
            this.pagina.Location = new System.Drawing.Point(0, 0);
            this.pagina.Size = new System.Drawing.Size(880, 262);
            this.pagina.MinimumSize = new System.Drawing.Size(650, 262);
            this.pagina.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(20, 20);
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitulo.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lblDescripcion.Location = new System.Drawing.Point(20, 78);
            this.lblDescripcion.Size = new System.Drawing.Size(600, 55);
            this.lblDescripcion.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblDescripcion.MaximumSize = new System.Drawing.Size(600, 0);
            this.lblDescripcion.AutoSize = true;
            this.lblTiposIniciales.Location = new System.Drawing.Point(20, 142);
            this.lblTiposIniciales.Size = new System.Drawing.Size(600, 55);
            this.lblTiposIniciales.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblTiposIniciales.MaximumSize = new System.Drawing.Size(600, 0);
            this.lblTiposIniciales.AutoSize = true;
            this.btnEjecutar.Location = new System.Drawing.Point(20, 185);
            this.btnEjecutar.Font = new System.Drawing.Font("Tahoma", 10F);
            this.pagina.Controls.Add(this.lblTitulo);
            this.pagina.Controls.Add(this.lblDescripcion);
            this.pagina.Controls.Add(this.lblTiposIniciales);
            this.pagina.Controls.Add(this.btnEjecutar);
            this.Controls.Add(this.pagina);
            this.btnEjecutar.Size = new System.Drawing.Size(185, 34);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Label lblTiposIniciales;
        private System.Windows.Forms.Button btnEjecutar;
    }
}
