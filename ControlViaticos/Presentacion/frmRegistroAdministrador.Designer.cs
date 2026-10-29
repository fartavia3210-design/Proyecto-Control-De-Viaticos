namespace ControlViaticos.Presentacion
{
    partial class frmRegistroAdministrador
    {
        private System.ComponentModel.IContainer components = null;

        // Libera los recursos del formulario.
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        // Crea los controles y configura el formulario.
        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblIdentificacion = new System.Windows.Forms.Label();
            this.lblNombreCompleto = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblClave = new System.Windows.Forms.Label();
            this.txtIdentificacion = new System.Windows.Forms.TextBox();
            this.txtNombreCompleto = new System.Windows.Forms.TextBox();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.btnCrear = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // Título
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(32, 25);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Text = "Crear primer administrador";
            // Etiquetas
            this.lblIdentificacion.AutoSize = true;
            this.lblIdentificacion.Location = new System.Drawing.Point(32, 96);
            this.lblIdentificacion.Name = "lblIdentificacion";
            this.lblIdentificacion.Text = "Identificación:";
            this.lblNombreCompleto.AutoSize = true;
            this.lblNombreCompleto.Location = new System.Drawing.Point(32, 146);
            this.lblNombreCompleto.Name = "lblNombreCompleto";
            this.lblNombreCompleto.Text = "Nombre completo:";
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(32, 196);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Text = "Usuario:";
            this.lblClave.AutoSize = true;
            this.lblClave.Location = new System.Drawing.Point(32, 246);
            this.lblClave.Name = "lblClave";
            this.lblClave.Text = "Contraseña:";
            // Campos
            this.txtIdentificacion.Location = new System.Drawing.Point(190, 92);
            this.txtIdentificacion.Name = "txtIdentificacion";
            this.txtIdentificacion.Size = new System.Drawing.Size(290, 27);
            this.txtIdentificacion.TabIndex = 0;
            this.txtNombreCompleto.Location = new System.Drawing.Point(190, 142);
            this.txtNombreCompleto.Name = "txtNombreCompleto";
            this.txtNombreCompleto.Size = new System.Drawing.Size(290, 27);
            this.txtNombreCompleto.TabIndex = 1;
            this.txtUsuario.Location = new System.Drawing.Point(190, 192);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(290, 27);
            this.txtUsuario.TabIndex = 2;
            this.txtClave.Location = new System.Drawing.Point(190, 242);
            this.txtClave.Name = "txtClave";
            this.txtClave.Size = new System.Drawing.Size(290, 27);
            this.txtClave.TabIndex = 3;
            this.txtClave.UseSystemPasswordChar = true;
            // Botones
            this.btnCrear.Location = new System.Drawing.Point(125, 310);
            this.btnCrear.Name = "btnCrear";
            this.btnCrear.Size = new System.Drawing.Size(190, 40);
            this.btnCrear.TabIndex = 4;
            this.btnCrear.Text = "Crear administrador";
            this.btnCrear.UseVisualStyleBackColor = true;
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSalir.Location = new System.Drawing.Point(335, 310);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(100, 40);
            this.btnSalir.TabIndex = 5;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // Formulario
            this.AcceptButton = this.btnCrear;
            this.CancelButton = this.btnSalir;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 385);
            this.Font = new System.Drawing.Font("Tahoma", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Name = "frmRegistroAdministrador";
            this.Text = "Registro del primer administrador";
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblIdentificacion);
            this.Controls.Add(this.lblNombreCompleto);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.lblClave);
            this.Controls.Add(this.txtIdentificacion);
            this.Controls.Add(this.txtNombreCompleto);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.txtClave);
            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.btnSalir);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblIdentificacion;
        private System.Windows.Forms.Label lblNombreCompleto;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblClave;
        private System.Windows.Forms.TextBox txtIdentificacion;
        private System.Windows.Forms.TextBox txtNombreCompleto;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Button btnSalir;
    }
}
