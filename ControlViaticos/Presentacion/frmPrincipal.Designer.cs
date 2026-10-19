namespace ControlViaticos.Presentacion
{
    partial class frmPrincipal
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
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnCargaInicial = new System.Windows.Forms.Button();
            this.pnlSubReportes = new System.Windows.Forms.Panel();
            this.btnRepOperaciones = new System.Windows.Forms.Button();
            this.btnRepEmpleado = new System.Windows.Forms.Button();
            this.btnRepFechas = new System.Windows.Forms.Button();
            this.btnReportes = new System.Windows.Forms.Button();
            this.btnBitacora = new System.Windows.Forms.Button();
            this.btnLiquidaciones = new System.Windows.Forms.Button();
            this.btnTiposViatico = new System.Windows.Forms.Button();
            this.btnEmpleados = new System.Windows.Forms.Button();
            this.btnUsuarios = new System.Windows.Forms.Button();
            this.pnlTitulo = new System.Windows.Forms.Panel();
            this.titulo = new System.Windows.Forms.Label();
            this.pnlContenedor = new System.Windows.Forms.Panel();
            this.pnlMenu.SuspendLayout();
            this.pnlSubReportes.SuspendLayout();
            this.pnlTitulo.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlMenu
            //
            this.pnlMenu.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlMenu.Controls.Add(this.btnSalir);
            this.pnlMenu.Controls.Add(this.btnCargaInicial);
            this.pnlMenu.Controls.Add(this.pnlSubReportes);
            this.pnlMenu.Controls.Add(this.btnReportes);
            this.pnlMenu.Controls.Add(this.btnBitacora);
            this.pnlMenu.Controls.Add(this.btnLiquidaciones);
            this.pnlMenu.Controls.Add(this.btnTiposViatico);
            this.pnlMenu.Controls.Add(this.btnEmpleados);
            this.pnlMenu.Controls.Add(this.btnUsuarios);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenu.Location = new System.Drawing.Point(0, 0);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(260, 800);
            this.pnlMenu.TabIndex = 0;
            //
            // btnSalir
            //
            this.btnSalir.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.Location = new System.Drawing.Point(12, 720);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(236, 48);
            this.btnSalir.TabIndex = 8;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            //
            // btnCargaInicial
            //
            this.btnCargaInicial.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCargaInicial.Location = new System.Drawing.Point(12, 650);
            this.btnCargaInicial.Name = "btnCargaInicial";
            this.btnCargaInicial.Size = new System.Drawing.Size(236, 48);
            this.btnCargaInicial.TabIndex = 7;
            this.btnCargaInicial.Text = "Carga inicial";
            this.btnCargaInicial.UseVisualStyleBackColor = true;
            this.btnCargaInicial.Click += new System.EventHandler(this.btnCargaInicial_Click);
            //
            // pnlSubReportes
            //
            this.pnlSubReportes.BackColor = System.Drawing.SystemColors.Highlight;
            this.pnlSubReportes.Controls.Add(this.btnRepOperaciones);
            this.pnlSubReportes.Controls.Add(this.btnRepEmpleado);
            this.pnlSubReportes.Controls.Add(this.btnRepFechas);
            this.pnlSubReportes.Location = new System.Drawing.Point(12, 433);
            this.pnlSubReportes.Name = "pnlSubReportes";
            this.pnlSubReportes.Size = new System.Drawing.Size(236, 195);
            this.pnlSubReportes.TabIndex = 6;
            this.pnlSubReportes.Visible = false;
            //
            // btnRepOperaciones
            //
            this.btnRepOperaciones.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnRepOperaciones.FlatAppearance.BorderSize = 0;
            this.btnRepOperaciones.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRepOperaciones.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepOperaciones.ForeColor = System.Drawing.Color.White;
            this.btnRepOperaciones.Location = new System.Drawing.Point(0, 130);
            this.btnRepOperaciones.Name = "btnRepOperaciones";
            this.btnRepOperaciones.Size = new System.Drawing.Size(236, 62);
            this.btnRepOperaciones.TabIndex = 2;
            this.btnRepOperaciones.Text = "Operaciones";
            this.btnRepOperaciones.UseVisualStyleBackColor = false;
            this.btnRepOperaciones.Click += new System.EventHandler(this.btnRepOperaciones_Click);
            //
            // btnRepEmpleado
            //
            this.btnRepEmpleado.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnRepEmpleado.FlatAppearance.BorderSize = 0;
            this.btnRepEmpleado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRepEmpleado.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepEmpleado.ForeColor = System.Drawing.Color.White;
            this.btnRepEmpleado.Location = new System.Drawing.Point(0, 65);
            this.btnRepEmpleado.Name = "btnRepEmpleado";
            this.btnRepEmpleado.Size = new System.Drawing.Size(236, 62);
            this.btnRepEmpleado.TabIndex = 1;
            this.btnRepEmpleado.Text = "Por empleado";
            this.btnRepEmpleado.UseVisualStyleBackColor = false;
            this.btnRepEmpleado.Click += new System.EventHandler(this.btnRepEmpleado_Click);
            //
            // btnRepFechas
            //
            this.btnRepFechas.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnRepFechas.FlatAppearance.BorderSize = 0;
            this.btnRepFechas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRepFechas.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepFechas.ForeColor = System.Drawing.Color.White;
            this.btnRepFechas.Location = new System.Drawing.Point(0, 0);
            this.btnRepFechas.Name = "btnRepFechas";
            this.btnRepFechas.Size = new System.Drawing.Size(236, 62);
            this.btnRepFechas.TabIndex = 0;
            this.btnRepFechas.Text = "Por fechas";
            this.btnRepFechas.UseVisualStyleBackColor = false;
            this.btnRepFechas.Click += new System.EventHandler(this.btnRepFechas_Click);
            //
            // btnReportes
            //
            this.btnReportes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReportes.Location = new System.Drawing.Point(12, 373);
            this.btnReportes.Name = "btnReportes";
            this.btnReportes.Size = new System.Drawing.Size(236, 48);
            this.btnReportes.TabIndex = 5;
            this.btnReportes.Text = "Reportes";
            this.btnReportes.UseVisualStyleBackColor = true;
            this.btnReportes.Click += new System.EventHandler(this.btnReportes_Click);
            //
            // btnBitacora
            //
            this.btnBitacora.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBitacora.Location = new System.Drawing.Point(12, 313);
            this.btnBitacora.Name = "btnBitacora";
            this.btnBitacora.Size = new System.Drawing.Size(236, 48);
            this.btnBitacora.TabIndex = 4;
            this.btnBitacora.Text = "Bitácora";
            this.btnBitacora.UseVisualStyleBackColor = true;
            this.btnBitacora.Click += new System.EventHandler(this.btnBitacora_Click);
            //
            // btnLiquidaciones
            //
            this.btnLiquidaciones.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLiquidaciones.Location = new System.Drawing.Point(12, 253);
            this.btnLiquidaciones.Name = "btnLiquidaciones";
            this.btnLiquidaciones.Size = new System.Drawing.Size(236, 48);
            this.btnLiquidaciones.TabIndex = 3;
            this.btnLiquidaciones.Text = "Liquidaciones";
            this.btnLiquidaciones.UseVisualStyleBackColor = true;
            this.btnLiquidaciones.Click += new System.EventHandler(this.btnLiquidaciones_Click);
            //
            // btnTiposViatico
            //
            this.btnTiposViatico.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTiposViatico.Location = new System.Drawing.Point(12, 193);
            this.btnTiposViatico.Name = "btnTiposViatico";
            this.btnTiposViatico.Size = new System.Drawing.Size(236, 48);
            this.btnTiposViatico.TabIndex = 2;
            this.btnTiposViatico.Text = "Tipos de viático";
            this.btnTiposViatico.UseVisualStyleBackColor = true;
            this.btnTiposViatico.Click += new System.EventHandler(this.btnTiposViatico_Click);
            //
            // btnEmpleados
            //
            this.btnEmpleados.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmpleados.Location = new System.Drawing.Point(12, 133);
            this.btnEmpleados.Name = "btnEmpleados";
            this.btnEmpleados.Size = new System.Drawing.Size(236, 48);
            this.btnEmpleados.TabIndex = 1;
            this.btnEmpleados.Text = "Empleados";
            this.btnEmpleados.UseVisualStyleBackColor = true;
            this.btnEmpleados.Click += new System.EventHandler(this.btnEmpleados_Click);
            //
            // btnUsuarios
            //
            this.btnUsuarios.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUsuarios.Location = new System.Drawing.Point(12, 73);
            this.btnUsuarios.Name = "btnUsuarios";
            this.btnUsuarios.Size = new System.Drawing.Size(236, 48);
            this.btnUsuarios.TabIndex = 0;
            this.btnUsuarios.Text = "Usuarios";
            this.btnUsuarios.UseVisualStyleBackColor = true;
            this.btnUsuarios.Click += new System.EventHandler(this.btnUsuarios_Click);
            //
            // pnlTitulo
            //
            this.pnlTitulo.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlTitulo.Controls.Add(this.titulo);
            this.pnlTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitulo.Location = new System.Drawing.Point(260, 0);
            this.pnlTitulo.Name = "pnlTitulo";
            this.pnlTitulo.Size = new System.Drawing.Size(1140, 73);
            this.pnlTitulo.TabIndex = 1;
            //
            // titulo
            //
            this.titulo.AutoSize = true;
            this.titulo.Font = new System.Drawing.Font("Tahoma", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titulo.Location = new System.Drawing.Point(20, 16);
            this.titulo.Name = "titulo";
            this.titulo.Size = new System.Drawing.Size(441, 45);
            this.titulo.TabIndex = 0;
            this.titulo.Text = "Control de viáticos";
            //
            // pnlContenedor
            //
            this.pnlContenedor.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pnlContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenedor.Location = new System.Drawing.Point(260, 73);
            this.pnlContenedor.Name = "pnlContenedor";
            this.pnlContenedor.Size = new System.Drawing.Size(1140, 727);
            this.pnlContenedor.TabIndex = 2;
            //
            // frmPrincipal
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 800);
            this.Controls.Add(this.pnlContenedor);
            this.Controls.Add(this.pnlTitulo);
            this.Controls.Add(this.pnlMenu);
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menú Control de Viáticos";
            this.pnlMenu.ResumeLayout(false);
            this.pnlSubReportes.ResumeLayout(false);
            this.pnlTitulo.ResumeLayout(false);
            this.pnlTitulo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnCargaInicial;
        private System.Windows.Forms.Panel pnlSubReportes;
        private System.Windows.Forms.Button btnRepOperaciones;
        private System.Windows.Forms.Button btnRepEmpleado;
        private System.Windows.Forms.Button btnRepFechas;
        private System.Windows.Forms.Button btnReportes;
        private System.Windows.Forms.Button btnBitacora;
        private System.Windows.Forms.Button btnLiquidaciones;
        private System.Windows.Forms.Button btnTiposViatico;
        private System.Windows.Forms.Button btnEmpleados;
        private System.Windows.Forms.Button btnUsuarios;
        private System.Windows.Forms.Panel pnlTitulo;
        private System.Windows.Forms.Label titulo;
        private System.Windows.Forms.Panel pnlContenedor;
    }
}
