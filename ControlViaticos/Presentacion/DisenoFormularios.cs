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
            FlowLayoutPanel campo = new FlowLayoutPanel { AutoSize = true,
                FlowDirection = FlowDirection.TopDown, WrapContents = false, Margin = new Padding(0, 0, 18, 12) };
            etiqueta.AutoSize = true;
            etiqueta.Margin = new Padding(0, 0, 0, 5);
            entrada.Width = buscar == null ? 270 : 190;
            entrada.Margin = new Padding(0);
            ComboBox combo = entrada as ComboBox;
            if (combo != null) combo.DropDownWidth = 440;
            campo.Controls.Add(etiqueta);
            if (buscar == null) campo.Controls.Add(entrada);
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
            FlowLayoutPanel fila = new FlowLayoutPanel { AutoSize = true, Dock = DockStyle.Fill,
                WrapContents = true, Margin = new Padding(0) };
            foreach (Control control in controles) fila.Controls.Add(control);
            return fila;
        }

        public static void Pagina(Form formulario, Label titulo, Control filtros,
            Control acciones, DataGridView tabla, Control pie = null)
        {
            formulario.SuspendLayout();
            formulario.AutoScaleMode = AutoScaleMode.Dpi;
            formulario.AutoScaleDimensions = new SizeF(96, 96);
            formulario.Font = new Font("Tahoma", 10);
            formulario.AutoScroll = true;
            formulario.Controls.Clear();
            Panel pagina = new Panel { Name = "pagina", Dock = DockStyle.Top,
                MinimumSize = new Size(320, 0) };
            List<Control> filas = new List<Control>();
            titulo.AutoSize = true;
            titulo.TextAlign = ContentAlignment.MiddleLeft;
            filas.Add(titulo);
            if (filtros != null) filas.Add(filtros);
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
            if (pie != null) filas.Add(pie);
            foreach (Control fila in filas)
            {
                fila.Dock = DockStyle.None;
                pagina.Controls.Add(fila);
            }
            // Medir de arriba hacia abajo con el ancho real, sin depender de una
            // altura anterior del formulario cuando una fila pasa a dos líneas.
            bool midiendo = false;
            EventHandler medir = delegate
            {
                if (midiendo) return;
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
                            fila.MaximumSize = new Size(ancho, 0);
                            fila.MinimumSize = new Size(ancho, 0);
                            fila.PerformLayout();
                            fila.Height = fila.GetPreferredSize(new Size(ancho, 0)).Height;
                        }
                        y += fila.Height + 14;
                    }
                    pagina.Height = y + 16;
                }
                finally { midiendo = false; }
            };
            pagina.SizeChanged += medir;
            formulario.Layout += delegate { medir(formulario, EventArgs.Empty); };
            formulario.Controls.Add(pagina);
            formulario.ResumeLayout(true);
            medir(formulario, EventArgs.Empty);
        }

        public static void Menu(Form formulario, Panel menu, Panel subreportes, params Control[] opciones)
        {
            formulario.AutoScaleMode = AutoScaleMode.Dpi;
            formulario.AutoScaleDimensions = new SizeF(96, 96);
            formulario.MinimumSize = new Size(760, 480);
            formulario.ClientSize = new Size(1180, 760);
            menu.Width = 210;
            FlowLayoutPanel lista = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true,
                FlowDirection = FlowDirection.TopDown, WrapContents = false, Padding = new Padding(10) };
            menu.Controls.Clear();
            foreach (Control opcion in opciones)
            {
                opcion.Margin = new Padding(0, 0, 0, 6);
                opcion.Size = new Size(168, opcion == subreportes ? 132 : 44);
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
    }
}
