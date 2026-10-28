using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using ControlViaticos.Logica;
using ControlViaticos.Objetos;
using ControlViaticos.Presentacion;

internal static class CapturasFinales
{
    private static T C<T>(Control f, string n) where T : Control { return (T)f.Controls.Find(n, true)[0]; }
    private static void Evento(Form f, string n) { f.GetType().GetMethod(n, BindingFlags.Instance | BindingFlags.NonPublic).Invoke(f, new object[] { f, EventArgs.Empty }); Application.DoEvents(); }
    private static string carpeta;
    private static void Guardar(Form f, string nombre)
    {
        f.BringToFront();
        Application.DoEvents();
        System.Threading.Thread.Sleep(80);
        using (Bitmap imagen = new Bitmap(f.Width, f.Height))
        {
            // Usar las coordenadas del formulario evita mezclar pixeles físicos
            // del escritorio con tamaños virtualizados por el DPI de Windows.
            // También impide capturar otra aplicación que cubra esta ventana.
            f.DrawToBitmap(imagen, new Rectangle(Point.Empty, imagen.Size));
            imagen.Save(Path.Combine(carpeta, nombre + ".png"), System.Drawing.Imaging.ImageFormat.Png);
        }
        Console.WriteLine("CAPTURA: " + nombre);
    }

    [STAThread] private static void Main(string[] args)
    {
        carpeta = args[0]; Directory.CreateDirectory(carpeta);
        Application.EnableVisualStyles();
        Usuario admin = new Usuario { id = 1, nombreCompleto = "Administrador de ejemplo", nombreUsuario = "admin", clave = "demo", tipo = "Administrador", activo = true };
        new LogicaAcceso().CrearPrimerAdministrador(admin); SesionActual.IniciarSesion("admin", "demo");
        new LogicaCargaInicial().EjecutarCargaInicial(admin);
        Empleado empleado = Estructuras.listaEmpleados[0];
        empleado.nombre = "Ana María"; empleado.apellido1 = "Mora"; empleado.apellido2 = "Solís";
        new LogicaEmpleados().AgregarEmpleado(new Empleado { id = 42, cedula = "00042", nombre = "Ana María", apellido1 = "Soto", apellido2 = "Díaz", telefono = "00012345", direccion = "San José", activo = false });
        DateTime fecha = new DateTime(2026, 10, 25);
        int tipo = Estructuras.listaTiposViatico[0].id;
        new LogicaLiquidaciones().AgregarLiquidacion(new Liquidacion { id = 101, idEmpleado = empleado.id, inicio = fecha.AddHours(6), fin = fecha.AddHours(21), justificacion = "Visita a la oficina regional" },
            new System.Collections.Generic.List<DetalleLiquidacion> { new DetalleLiquidacion { idLiquidacion = 101, idViatico = tipo, monto = 3000, fechaConsumo = fecha } });
        using (Form login = new frmLogin()) { login.Show(); Guardar(login, "login"); }
        using (Form registro = new frmRegistroAdministrador()) { registro.Show(); Guardar(registro, "registro-inicial"); }
        using (frmPrincipal principal = new frmPrincipal())
        {
            principal.Show();
            Rectangle area = Screen.PrimaryScreen.WorkingArea;
            Size normal = new Size(Math.Min(1180, area.Width - 32), Math.Min(760, area.Height - 64));
            Console.WriteLine("Área de trabajo: " + area + "; ventana normal: " + normal);
            foreach (string modulo in new[] { "Usuarios", "Empleados", "TiposViatico", "Liquidaciones", "Bitacora", "RepFechas", "RepEmpleado", "RepOperaciones", "CargaInicial" })
            {
                Evento(principal, "btn" + modulo + "_Click");
                Form f = (Form)C<Panel>(principal, "pnlContenedor").Tag;
                if (modulo == "Liquidaciones") { C<TextBox>(f, "txtConsecutivo").Text = "101"; Evento(f, "btnBuscar_Click"); }
                if (modulo == "RepFechas") { C<DateTimePicker>(f, "dtpDesde").Value = fecha; C<DateTimePicker>(f, "dtpHasta").Value = fecha; Evento(f, "btnConsultar_Click"); }
                if (modulo == "RepEmpleado") { C<ComboBox>(f, "cmbEmpleado").SelectedValue = empleado.id; Evento(f, "btnConsultar_Click"); }
                if (modulo == "RepOperaciones") { C<ComboBox>(f, "cmbUsuario").SelectedValue = admin.id; Evento(f, "btnConsultar_Click"); }
                principal.ClientSize = normal; Guardar(principal, modulo + "-normal");
                principal.ClientSize = new Size(780, 500); Guardar(principal, modulo + "-pequena");
                Control destino = f.Controls.Find("lblMontoTotal", true).Length > 0 ? (Control)C<Label>(f, "lblMontoTotal") : f.Controls.Find("btnGuardar", true).Length > 0 ? C<Button>(f, "btnGuardar") : null;
                if (destino != null) { Point posicion = f.PointToClient(destino.PointToScreen(Point.Empty));
                    f.AutoScrollPosition = new Point(posicion.X - f.AutoScrollPosition.X, posicion.Y - f.AutoScrollPosition.Y); Guardar(principal, modulo + "-pequena-desplazada"); }
                f.AutoScrollPosition = Point.Empty;
                f.Scale(new SizeF(1.25f, 1.25f)); Guardar(principal, modulo + "-escala-simulada-125");
                f.Scale(new SizeF(1.2f, 1.2f)); Guardar(principal, modulo + "-escala-simulada-150");
                if (destino != null) { Point posicion = f.PointToClient(destino.PointToScreen(Point.Empty));
                    f.AutoScrollPosition = new Point(posicion.X - f.AutoScrollPosition.X, posicion.Y - f.AutoScrollPosition.Y); Guardar(principal, modulo + "-escala-simulada-150-desplazada"); }
            }
            principal.Close();
        }
    }
}
