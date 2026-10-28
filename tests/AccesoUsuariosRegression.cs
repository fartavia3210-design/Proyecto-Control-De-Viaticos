using System;
using System.Reflection;
using System.Windows.Forms;
using ControlViaticos.Logica;
using ControlViaticos.Objetos;
using ControlViaticos.Presentacion;

internal static class AccesoUsuariosRegression
{
    private static int total;
    private static void Verificar(bool ok, string mensaje) { if (!ok) throw new Exception(mensaje); Console.WriteLine("OK: " + mensaje); total++; }
    private static Usuario Usuario(int id, string nombre, string rol = "Normal")
    { return new Usuario { id = id, nombreCompleto = "Nombre " + nombre, nombreUsuario = nombre, clave = "prueba", tipo = rol, activo = true }; }
    private static T C<T>(Form f, string nombre) where T : Control { return (T)f.Controls.Find(nombre, true)[0]; }
    private static void Evento(Form f, string nombre) { f.GetType().GetMethod(nombre, BindingFlags.NonPublic | BindingFlags.Instance).Invoke(f, new object[] { f, EventArgs.Empty }); }
    [STAThread] private static int Main()
    {
        try
        {
            Application.EnableVisualStyles();
            System.Windows.Forms.Timer dialogos = new System.Windows.Forms.Timer { Interval = 30 };
            dialogos.Tick += delegate { DialogosPrueba.Responder(DialogResult.OK); }; dialogos.Start();
            LogicaUsuarios usuarios = new LogicaUsuarios(); LogicaAcceso acceso = new LogicaAcceso(); LogicaBitacora bitacora = new LogicaBitacora();
            Verificar(!usuarios.AgregarUsuario(Usuario(2, "normal")), "No crea usuarios sin administrador");
            Verificar(acceso.CrearPrimerAdministrador(Usuario(1, "admin", "Administrador")), "Crea primer administrador explicitamente");
            Verificar(!acceso.CrearPrimerAdministrador(Usuario(3, "otro", "Administrador")), "No repite configuracion inicial");
            Verificar(SesionActual.IniciarSesion(" ADMIN ", "prueba"), "Login normaliza nombre de usuario");
            Verificar(!SesionActual.IniciarSesion("admin", "Prueba") && !SesionActual.HaySesionActiva(), "Clave exacta rechazada limpia sesion anterior");
            SesionActual.IniciarSesion("admin", "prueba");
            Verificar(usuarios.AgregarUsuario(Usuario(2, "normal")), "Crea usuario Normal");
            Verificar(!usuarios.AgregarUsuario(Usuario(3, " NORMAL ")), "Rechaza usuario duplicado con mayusculas y espacios");
            Usuario invalido = Usuario(3, "invalido"); invalido.clave = " "; Verificar(!usuarios.AgregarUsuario(invalido), "Rechaza clave vacia");
            Usuario editado = Usuario(2, "cambio"); Verificar(usuarios.ModificarUsuario(editado) && usuarios.BuscarUsuario(2).nombreUsuario == "cambio", "Modifica usuario sin duplicarlo");
            Verificar(!usuarios.EliminarUsuario(1), "No elimina cuenta de la sesion");
            Usuario desactivado = Usuario(1, "admin", "Administrador"); desactivado.activo = false;
            Verificar(!usuarios.ModificarUsuario(desactivado) && usuarios.BuscarUsuario(1).activo, "No desactiva administrador de la sesion");
            Verificar(!usuarios.ModificarUsuario(Usuario(1, "admin")), "No se quita rol administrador durante sesion");
            Verificar(usuarios.AgregarUsuario(Usuario(3, "temporal")) && usuarios.EliminarUsuario(3), "Elimina usuario sin historial");
            Verificar(bitacora.RegistrarOperacion(SesionActual.usuarioActual, "Usuarios", "Movimiento de administrador"), "Registra primer autor");
            Verificar(SesionActual.IniciarSesion("cambio", "prueba") && !SesionActual.EsAdministrador(), "Entra Normal con nuevo nombre");
            Verificar(!usuarios.AgregarUsuario(Usuario(3, "prohibido")) && !usuarios.EliminarUsuario(1), "Normal no escribe en Usuarios");
            Verificar(new LogicaCargaInicial().EjecutarCargaInicial(SesionActual.usuarioActual) == -1, "Normal no ejecuta carga inicial");
            Verificar(bitacora.RegistrarOperacion(SesionActual.usuarioActual, "Empleados", "Movimiento de Normal"), "Registra segundo autor");
            DateTime fecha = new DateTime(2026, 10, 25);
            Estructuras.listaBitacora[0].fechaHora = fecha;
            Estructuras.listaBitacora[1].fechaHora = fecha.AddDays(1).AddTicks(-1);
            Verificar(bitacora.ConsultarOperaciones(1, fecha, fecha).Count == 1 && bitacora.ConsultarOperaciones(2, fecha, fecha).Count == 1, "Bitacora filtra autor e incluye ultimo instante del dia");
            Verificar(bitacora.ConsultarOperaciones(1, fecha.AddDays(1), fecha).Count == 0, "Bitacora rechaza rango invertido");
            using (frmPrincipal principal = new frmPrincipal())
            {
                principal.Show(); Application.DoEvents();
                Verificar(!C<Button>(principal, "btnUsuarios").Enabled && !C<Button>(principal, "btnCargaInicial").Enabled && C<Button>(principal, "btnEmpleados").Enabled, "Menu aplica permisos por rol");
            }
            SesionActual.IniciarSesion("admin", "prueba");
            Verificar(!usuarios.EliminarUsuario(2), "No elimina autor con historial");
            editado = Usuario(2, "renombrado"); editado.activo = false;
            Verificar(usuarios.ModificarUsuario(editado) && Estructuras.listaBitacora[1].nombreUsuario == "cambio", "Desactivar y renombrar conserva autor historico");
            Verificar(!SesionActual.IniciarSesion("renombrado", "prueba"), "Inactivo no entra aunque acierte clave");
            SesionActual.IniciarSesion("admin", "prueba");
            using (frmReporteOperaciones reporte = new frmReporteOperaciones())
            {
                reporte.Show(); Application.DoEvents();
                C<ComboBox>(reporte, "cmbUsuario").SelectedValue = 2;
                C<DateTimePicker>(reporte, "dtpDesde").Value = fecha; C<DateTimePicker>(reporte, "dtpHasta").Value = fecha;
                Evento(reporte, "btnConsultar_Click");
                Verificar(C<DataGridView>(reporte, "grdOperaciones").Rows.Count == 1, "Reporte operaciones consulta autor inactivo por ID");
                C<DateTimePicker>(reporte, "dtpDesde").Value = fecha.AddDays(2); Evento(reporte, "btnConsultar_Click");
                Verificar(C<DataGridView>(reporte, "grdOperaciones").Rows.Count == 0, "Reporte limpia filas ante rango invalido");
            }
            dialogos.Stop(); Console.WriteLine(total + " comprobaciones de acceso correctas."); return 0;
        }
        catch (Exception error) { Console.Error.WriteLine(error); return 1; }
    }
}
