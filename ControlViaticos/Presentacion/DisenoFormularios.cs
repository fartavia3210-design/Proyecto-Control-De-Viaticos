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
            TableLayoutPanel pagina = new TableLayoutPanel { Name = "pagina", AutoSize = true,
                Dock = DockStyle.Top, ColumnCount = 1, Padding = new Padding(16), MinimumSize = new Size(320, 0) };
            pagina.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            titulo.AutoSize = true;
            titulo.TextAlign = ContentAlignment.MiddleLeft;
            titulo.Margin = new Padding(0, 0, 0, 18);
            titulo.Dock = DockStyle.Fill;
            AgregarFila(pagina, titulo);
            if (filtros != null) AgregarFila(pagina, filtros);
            if (acciones != null)
            {
                foreach (Control boton in acciones.Controls)
                {
                    boton.AutoSize = true;
                    boton.MinimumSize = new Size(110, 34);
                    boton.Margin = new Padding(0, 0, 12, 12);
                }
                AgregarFila(pagina, acciones);
            }
            if (tabla != null)
            {
                tabla.Dock = DockStyle.Fill;
                tabla.RowHeadersVisible = false;
                tabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                tabla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                tabla.Margin = new Padding(0, 8, 0, 12);
                pagina.RowStyles.Add(new RowStyle(SizeType.Absolute, 280));
                pagina.Controls.Add(tabla, 0, pagina.RowCount++);
            }
            if (pie != null) AgregarFila(pagina, pie);
            formulario.Controls.Add(pagina);
            formulario.ResumeLayout(true);
        }

        private static void AgregarFila(TableLayoutPanel pagina, Control control)
        {
            pagina.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            pagina.Controls.Add(control, 0, pagina.RowCount++);
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
