namespace ControlViaticos.Presentacion
{
    partial class frmUsuarios
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
            this.lblIdentificacion = new System.Windows.Forms.Label();
            this.txtIdentificacion = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.lblNombreCompleto = new System.Windows.Forms.Label();
            this.txtNombreCompleto = new System.Windows.Forms.TextBox();
            this.lblNombreUsuario = new System.Windows.Forms.Label();
            this.txtNombreUsuario = new System.Windows.Forms.TextBox();
            this.lblClave = new System.Windows.Forms.Label();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.lblTipo = new System.Windows.Forms.Label();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.grdUsuarios = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdUsuarios)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(570, 42);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Usuarios";
            // 
            // lblIdentificacion
            // 
            this.lblIdentificacion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblIdentificacion.Name = "lblIdentificacion";
            this.lblIdentificacion.TabIndex = 1;
            this.lblIdentificacion.Text = "Identificación:";
            // 
            // txtIdentificacion
            // 
            this.txtIdentificacion.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtIdentificacion.Name = "txtIdentificacion";
            this.txtIdentificacion.TabIndex = 2;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.TabIndex = 3;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lblNombreCompleto
            // 
            this.lblNombreCompleto.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNombreCompleto.Name = "lblNombreCompleto";
            this.lblNombreCompleto.TabIndex = 4;
            this.lblNombreCompleto.Text = "Nombre completo:";
            // 
            // txtNombreCompleto
            // 
            this.txtNombreCompleto.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtNombreCompleto.Name = "txtNombreCompleto";
            this.txtNombreCompleto.TabIndex = 5;
            // 
            // lblNombreUsuario
            // 
            this.lblNombreUsuario.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNombreUsuario.Name = "lblNombreUsuario";
            this.lblNombreUsuario.TabIndex = 6;
            this.lblNombreUsuario.Text = "Nombre de usuario:";
            // 
            // txtNombreUsuario
            // 
            this.txtNombreUsuario.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtNombreUsuario.Name = "txtNombreUsuario";
            this.txtNombreUsuario.TabIndex = 7;
            // 
            // lblClave
            // 
            this.lblClave.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblClave.Name = "lblClave";
            this.lblClave.TabIndex = 8;
            this.lblClave.Text = "Clave:";
            // 
            // txtClave
            // 
            this.txtClave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtClave.Name = "txtClave";
            this.txtClave.PasswordChar = '*';
            this.txtClave.TabIndex = 9;
            // 
            // lblTipo
            // 
            this.lblTipo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.TabIndex = 10;
            this.lblTipo.Text = "Tipo de usuario:";
            // 
            // cmbTipo
            // 
            this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.TabIndex = 11;
            // 
            // lblEstado
            // 
            this.lblEstado.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.TabIndex = 12;
            this.lblEstado.Text = "Estado:";
            // 
            // cmbEstado
            // 
            this.cmbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.TabIndex = 13;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.TabIndex = 14;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.TabIndex = 15;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // grdUsuarios
            // 
            this.grdUsuarios.AllowUserToAddRows = false;
            this.grdUsuarios.AllowUserToDeleteRows = false;
            this.grdUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdUsuarios.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grdUsuarios.Name = "grdUsuarios";
            this.grdUsuarios.ReadOnly = true;
            this.grdUsuarios.RowHeadersWidth = 82;
            this.grdUsuarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdUsuarios.TabIndex = 16;
            // 
            // frmUsuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmUsuarios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Usuarios";
            ((System.ComponentModel.ISupportInitialize)(this.grdUsuarios)).EndInit();
            // Distribucion manual mediante propiedades de los controles.
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Font = new System.Drawing.Font("Tahoma", 10F);
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.AutoScroll = true;
            this.pagina.Name = "pagina";
            this.pagina.Location = new System.Drawing.Point(0, 0);
            this.pagina.Size = new System.Drawing.Size(880, 627);
            this.pagina.MinimumSize = new System.Drawing.Size(650, 627);
            this.pagina.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(20, 20);
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitulo.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lblIdentificacion.Location = new System.Drawing.Point(20, 78);
            this.lblIdentificacion.Size = new System.Drawing.Size(290, 22);
            this.lblIdentificacion.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblIdentificacion.AutoSize = true;
            this.txtIdentificacion.Dock = System.Windows.Forms.DockStyle.None;
            this.txtIdentificacion.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.txtIdentificacion.Location = new System.Drawing.Point(20, 104);
            this.txtIdentificacion.Size = new System.Drawing.Size(185, 27);
            this.txtIdentificacion.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnBuscar.Location = new System.Drawing.Point(212, 102);
            this.btnBuscar.Size = new System.Drawing.Size(82, 30);
            this.btnBuscar.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblNombreCompleto.Location = new System.Drawing.Point(340, 78);
            this.lblNombreCompleto.Size = new System.Drawing.Size(290, 22);
            this.lblNombreCompleto.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblNombreCompleto.AutoSize = true;
            this.txtNombreCompleto.Location = new System.Drawing.Point(340, 104);
            this.txtNombreCompleto.Size = new System.Drawing.Size(270, 27);
            this.txtNombreCompleto.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtNombreCompleto.Dock = System.Windows.Forms.DockStyle.None;
            this.txtNombreCompleto.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblNombreUsuario.Location = new System.Drawing.Point(20, 152);
            this.lblNombreUsuario.Size = new System.Drawing.Size(290, 22);
            this.lblNombreUsuario.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblNombreUsuario.AutoSize = true;
            this.txtNombreUsuario.Location = new System.Drawing.Point(20, 178);
            this.txtNombreUsuario.Size = new System.Drawing.Size(270, 27);
            this.txtNombreUsuario.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtNombreUsuario.Dock = System.Windows.Forms.DockStyle.None;
            this.txtNombreUsuario.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblClave.Location = new System.Drawing.Point(340, 152);
            this.lblClave.Size = new System.Drawing.Size(290, 22);
            this.lblClave.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblClave.AutoSize = true;
            this.txtClave.Location = new System.Drawing.Point(340, 178);
            this.txtClave.Size = new System.Drawing.Size(270, 27);
            this.txtClave.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtClave.Dock = System.Windows.Forms.DockStyle.None;
            this.txtClave.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblTipo.Location = new System.Drawing.Point(20, 226);
            this.lblTipo.Size = new System.Drawing.Size(290, 22);
            this.lblTipo.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblTipo.AutoSize = true;
            this.cmbTipo.Location = new System.Drawing.Point(20, 252);
            this.cmbTipo.Size = new System.Drawing.Size(270, 27);
            this.cmbTipo.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cmbTipo.Dock = System.Windows.Forms.DockStyle.None;
            this.cmbTipo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.cmbTipo.DropDownWidth = 440;
            this.lblEstado.Location = new System.Drawing.Point(340, 226);
            this.lblEstado.Size = new System.Drawing.Size(290, 22);
            this.lblEstado.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblEstado.AutoSize = true;
            this.cmbEstado.Location = new System.Drawing.Point(340, 252);
            this.cmbEstado.Size = new System.Drawing.Size(270, 27);
            this.cmbEstado.Font = new System.Drawing.Font("Tahoma", 10F);
            this.cmbEstado.Dock = System.Windows.Forms.DockStyle.None;
            this.cmbEstado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.cmbEstado.DropDownWidth = 440;
            this.btnGuardar.Location = new System.Drawing.Point(20, 300);
            this.btnGuardar.Size = new System.Drawing.Size(125, 34);
            this.btnGuardar.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnEliminar.Location = new System.Drawing.Point(160, 300);
            this.btnEliminar.Size = new System.Drawing.Size(125, 34);
            this.btnEliminar.Font = new System.Drawing.Font("Tahoma", 10F);
            this.grdUsuarios.Location = new System.Drawing.Point(20, 352);
            this.grdUsuarios.Size = new System.Drawing.Size(840, 230);
            this.grdUsuarios.Font = new System.Drawing.Font("Tahoma", 10F);
            this.grdUsuarios.Dock = System.Windows.Forms.DockStyle.None;
            this.grdUsuarios.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.pagina.Controls.Add(this.lblTitulo);
            this.pagina.Controls.Add(this.lblIdentificacion);
            this.pagina.Controls.Add(this.txtIdentificacion);
            this.pagina.Controls.Add(this.btnBuscar);
            this.pagina.Controls.Add(this.lblNombreCompleto);
            this.pagina.Controls.Add(this.txtNombreCompleto);
            this.pagina.Controls.Add(this.lblNombreUsuario);
            this.pagina.Controls.Add(this.txtNombreUsuario);
            this.pagina.Controls.Add(this.lblClave);
            this.pagina.Controls.Add(this.txtClave);
            this.pagina.Controls.Add(this.lblTipo);
            this.pagina.Controls.Add(this.cmbTipo);
            this.pagina.Controls.Add(this.lblEstado);
            this.pagina.Controls.Add(this.cmbEstado);
            this.pagina.Controls.Add(this.btnGuardar);
            this.pagina.Controls.Add(this.btnEliminar);
            this.pagina.Controls.Add(this.grdUsuarios);
            this.Controls.Add(this.pagina);
            this.btnBuscar.Text = "Buscar";
            this.grdUsuarios.RowHeadersVisible = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblIdentificacion;
        private System.Windows.Forms.TextBox txtIdentificacion;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label lblNombreCompleto;
        private System.Windows.Forms.TextBox txtNombreCompleto;
        private System.Windows.Forms.Label lblNombreUsuario;
        private System.Windows.Forms.TextBox txtNombreUsuario;
        private System.Windows.Forms.Label lblClave;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.DataGridView grdUsuarios;
    }
}

