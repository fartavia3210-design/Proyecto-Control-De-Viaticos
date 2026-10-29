using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ControlViaticos.Presentacion
{
    // Contenedores comunes: los controles crecen en filas y la página se desplaza.
    // Los eventos y los datos siguen perteneciendo a cada formulario.
    internal static class DisenoFormularios
    {
        public static Control Campo(Label etiqueta, Control entrada, Button buscar = null)
        {

            FlowLayoutPanel campo = new FlowLayoutPanel();
            campo.AutoSize = true;
            campo.FlowDirection = FlowDirection.TopDown;
            campo.WrapContents = false;
            campo.Margin = new Padding(0, 0, 18, 12);
            etiqueta.AutoSize = true;
            etiqueta.Margin = new Padding(0, 0, 0, 5);
            if (buscar == null)
            {
                entrada.Width = 270;
            }
            else
            {
                entrada.Width = 190;
            }
            entrada.Margin = new Padding(0);
            ComboBox combo = entrada as ComboBox;
            if (combo != null)
            {
                combo.DropDownWidth = 440;
            }
            campo.Controls.Add(etiqueta);
            if (buscar == null)
            {
                campo.Controls.Add(entrada);
            }
            else
            {
                FlowLayoutPanel fila = Fila();
                fila.WrapContents = false;
                fila.Dock = DockStyle.None;
                fila.MinimumSize = new Size(270, 34);
                buscar.Text = "Buscar";
                buscar.AutoSize = true;
                buscar.MinimumSize = new Size(70, 28);
                fila.Controls.Add(entrada);
                fila.Controls.Add(buscar);
                campo.Controls.Add(fila);
            }
            return campo;
        }

        public static FlowLayoutPanel Fila(params Control[] controles)
        {

            FlowLayoutPanel fila = new FlowLayoutPanel();
            fila.AutoSize = true;
            fila.Dock = DockStyle.Fill;
            fila.WrapContents = true;
            fila.Margin = new Padding(0);
            foreach (Control control in controles)
            {
                fila.Controls.Add(control);
            }
            return fila;
        }

        public static void Pagina(Form formulario, Label titulo, Control filtros, Control acciones, DataGridView tabla, Control pie = null)
        {
            formulario.SuspendLayout();
            formulario.AutoScaleMode = AutoScaleMode.Dpi;
            formulario.AutoScaleDimensions = new SizeF(96, 96);
            formulario.Font = new Font("Tahoma", 10);
            formulario.AutoScroll = true;
            formulario.Controls.Clear();

            Panel pagina = new Panel();
            pagina.Name = "pagina";
            pagina.Dock = DockStyle.Top;
            pagina.MinimumSize = new Size(320, 0);
            List<Control> filas = new List<Control>();
            titulo.AutoSize = true;
            titulo.TextAlign = ContentAlignment.MiddleLeft;
            filas.Add(titulo);
            if (filtros != null)
            {
                filas.Add(filtros);
            }
            if (acciones != null)
            {
                foreach (Control boton in acciones.Controls)
                {
                    boton.AutoSize = true;
                    boton.MinimumSize = new Size(110, 34);
                    boton.Margin = new Padding(0, 0, 12, 8);
                }
                filas.Add(acciones);
            }
            if (tabla != null)
            {
                tabla.Height = 260;
                tabla.RowHeadersVisible = false;
                tabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                tabla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                filas.Add(tabla);
            }
            if (pie != null)
            {
                filas.Add(pie);
            }
            foreach (Control fila in filas)
            {
                fila.Dock = DockStyle.None;
                pagina.Controls.Add(fila);
            }
            // Medir de arriba hacia abajo con el ancho real, sin depender de una
            // altura anterior del formulario cuando una fila pasa a dos líneas.
            DistribucionPagina distribucion = new DistribucionPagina(pagina, filas, tabla);
            pagina.SizeChanged += distribucion.Actualizar;
            formulario.ClientSizeChanged += distribucion.Actualizar;
            formulario.FontChanged += distribucion.Actualizar;
            foreach (Control fila in filas)
            {
                fila.SizeChanged += distribucion.Actualizar;
            }
            formulario.Controls.Add(pagina);
            formulario.ResumeLayout(true);
            distribucion.Actualizar(formulario, EventArgs.Empty);
        }

        public static void Menu(Form formulario, Panel menu, Panel subreportes, params Control[] opciones)
        {
            formulario.AutoScaleMode = AutoScaleMode.Dpi;
            formulario.AutoScaleDimensions = new SizeF(96, 96);
            formulario.MinimumSize = new Size(760, 480);
            Rectangle area = Screen.PrimaryScreen.WorkingArea;
            formulario.ClientSize = new Size(Math.Min(1180, Math.Max(740, area.Width - 32)), Math.Min(760, Math.Max(440, area.Height - 64)));
            menu.Width = 210;

            FlowLayoutPanel lista = new FlowLayoutPanel();
            lista.Dock = DockStyle.Fill;
            lista.AutoScroll = true;
            lista.FlowDirection = FlowDirection.TopDown;
            lista.WrapContents = false;
            lista.Padding = new Padding(10);
            menu.Controls.Clear();
            foreach (Control opcion in opciones)
            {
                opcion.Margin = new Padding(0, 0, 0, 6);
                if (opcion == subreportes)
                {
                    opcion.Size = new Size(168, 132);
                }
                else
                {
                    opcion.Size = new Size(168, 44);
                }
                lista.Controls.Add(opcion);
            }
            int y = 0;
            foreach (Control boton in subreportes.Controls)
            {
                boton.SetBounds(0, y, 168, 40);
                boton.Font = new Font("Tahoma", 9);
                y += 44;
            }
            menu.Controls.Add(lista);
        }

        // Cada pagina conserva su propia distribucion y atiende eventos con un metodo nombrado.
        private class DistribucionPagina
        {
            private Panel pagina;
            private List<Control> filas;
            private DataGridView tabla;
            private bool midiendo;

            public DistribucionPagina(Panel pagina, List<Control> filas, DataGridView tabla)
            {
                this.pagina = pagina;
                this.filas = filas;
                this.tabla = tabla;
            }

            public void Actualizar(object sender, EventArgs e)
            {
                if (midiendo)
                {
                    return;
                }
                midiendo = true;
                try
                {
                    int ancho = Math.Max(288, pagina.ClientSize.Width - 32);
                    int y = 16;
                    foreach (Control fila in filas)
                    {
                        fila.Location = new Point(16, y);
                        fila.Width = ancho;
                        if (fila != tabla)
                        {
                            Size limite = new Size(ancho, 0);
                            if (fila.MaximumSize != limite)
                            {
                                fila.MaximumSize = limite;
                            }
                            if (fila.MinimumSize != limite)
                            {
                                fila.MinimumSize = limite;
                            }
                            fila.PerformLayout();
                            int alto = fila.GetPreferredSize(new Size(ancho, 0)).Height;
                            if (fila.Height != alto)
                            {
                                fila.Height = alto;
                            }
                        }
                        y += fila.Height + 14;
                    }
                    if (pagina.Height != y + 16)
                    {
                        pagina.Height = y + 16;
                    }
                }
                finally { midiendo = false; }
            }
        }
    }
}
