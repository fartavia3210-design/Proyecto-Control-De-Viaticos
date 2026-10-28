using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using ControlViaticos.Logica;
using ControlViaticos.Objetos;
using ControlViaticos.Presentacion;

internal static class CierreRegression
{
    static int count;
    static volatile DialogResult respuesta = DialogResult.OK;
    static void Check(bool ok, string message) { if (!ok) throw new Exception(message); Console.WriteLine("OK " + (++count) + ": " + message); }
    static T C<T>(Form f, string n) where T : Control { return (T)f.Controls.Find(n, true)[0]; }
    static void Event(Form f, string n) { f.GetType().GetMethod(n, BindingFlags.Instance | BindingFlags.NonPublic).Invoke(f, new object[] {f, EventArgs.Empty}); Application.DoEvents(); }
    static Empleado Emp(int id, string cedula) { return new Empleado {id=id, cedula=cedula, nombre="Ana", apellido1="Mora", apellido2="Solis", telefono="00123", direccion="San Jose", activo=true}; }
    static TipoViatico Tipo(int id, string n, int m) { return new TipoViatico {id=id, nombre=n, monto=m, activo=true}; }
    static string Money(long n) { return "₡ " + n.ToString("N0", CultureInfo.GetCultureInfo("es-CR")); }
    static void FillEmpleado(Form f, int id, string cedula) {
        C<TextBox>(f,"txtIdentificacion").Text=id.ToString(); C<TextBox>(f,"txtCedula").Text=cedula;
        C<TextBox>(f,"txtNombre").Text="Beatriz"; C<TextBox>(f,"txtApellido1").Text="Soto"; C<TextBox>(f,"txtApellido2").Text="Diaz";
        C<TextBox>(f,"txtTelefono").Text="000456"; C<TextBox>(f,"txtDireccion").Text="Heredia";
    }
    static void FillTipo(Form f, int id, string n, string m) { C<TextBox>(f,"txtIdentificador").Text=id.ToString(); C<TextBox>(f,"txtNombre").Text=n; C<TextBox>(f,"txtMonto").Text=m; }
    [STAThread] static int Main() {
        try {
            Console.OutputEncoding = new System.Text.UTF8Encoding(false);
            Application.EnableVisualStyles();
            IDisposable timer = DialogosPrueba.Iniciar(() => respuesta);
            var empleados=new LogicaEmpleados(); var tipos=new LogicaTiposViatico();
            Check(!empleados.AgregarEmpleado(Emp(42,"00042")),"Empleados rechaza alta sin sesion");
            Check(!empleados.ModificarEmpleado(Emp(42,"00042")) && !empleados.EliminarEmpleado(42),"Empleados rechaza modificar/eliminar sin sesion");
            Check(!tipos.AgregarTipoViatico(Tipo(83,"Desayuno",3000)) && !tipos.ModificarTipoViatico(Tipo(83,"Desayuno",3000)) && !tipos.EliminarTipoViatico(83),"Tipos rechaza escrituras sin sesion");
            var admin=new Usuario {id=1,nombreCompleto="Administrador",nombreUsuario="admin",clave="prueba",tipo="Administrador",activo=true};
            Check(new LogicaAcceso().CrearPrimerAdministrador(admin) && SesionActual.IniciarSesion("admin","prueba"),"Registro inicial y sesion para prueba");
            Check(empleados.AgregarEmpleado(Emp(42," 00042 ")) && empleados.BuscarEmpleado(42).cedula=="00042","Alta de empleado y recorte de espacios sin perder ceros");
            Check(!empleados.AgregarEmpleado(Emp(42,"999")),"Rechaza ID de empleado duplicado");
            Check(!empleados.AgregarEmpleado(Emp(43," 00042 ")),"Rechaza cedula de empleado duplicada");
            Check(!empleados.AgregarEmpleado(null) && !empleados.AgregarEmpleado(Emp(-1,"n")),"Rechaza empleado nulo e ID negativo");
            var incompleto=Emp(44,"44"); incompleto.apellido2=" "; Check(!empleados.AgregarEmpleado(incompleto),"Rechaza campos obligatorios vacios");
            Check(empleados.AgregarEmpleado(Emp(43,"00043")),"Segundo empleado con nombre coincidente");
            var cambio=Emp(42,"00043"); cambio.nombre="No persistir"; Check(!empleados.ModificarEmpleado(cambio) && empleados.BuscarEmpleado(42).nombre=="Ana","Modificar cedula duplicada no altera empleado original");
            cambio=Emp(42,"00042"); cambio.telefono="000999"; Check(empleados.ModificarEmpleado(cambio) && empleados.BuscarEmpleado(42).telefono=="000999","Modifica empleado sin duplicarlo");
            var copia=empleados.ObtenerEmpleados(); copia.Clear(); Check(empleados.ObtenerEmpleados().Count==2,"ObtenerEmpleados no comparte contenedor de lista");
            Check(tipos.AgregarTipoViatico(Tipo(83," Desayuno ",3000)) && tipos.BuscarTipoViatico(83).nombre=="Desayuno","Alta de viatico y recorte de nombre");
            Check(!tipos.AgregarTipoViatico(Tipo(83,"Otro",1)) && !tipos.AgregarTipoViatico(Tipo(84," DESAYUNO ",1)),"Rechaza ID y nombre duplicados ignorando mayusculas/espacios");
            Check(!tipos.AgregarTipoViatico(Tipo(84,"Hospedaje",0)) && !tipos.AgregarTipoViatico(Tipo(84,"Hospedaje",-1)),"Rechaza montos cero y negativos");
            Check(!tipos.AgregarTipoViatico(null) && !tipos.AgregarTipoViatico(Tipo(0,"Hospedaje",1)),"Rechaza tipo nulo e ID cero");
            Check(tipos.AgregarTipoViatico(Tipo(84,"Hospedaje",7000)),"Alta de segundo tipo");
            Check(!tipos.ModificarTipoViatico(Tipo(84," desayuno ",8000)) && tipos.BuscarTipoViatico(84).monto==7000,"Rechazo de nombre duplicado conserva monto anterior");
            Check(tipos.ModificarTipoViatico(Tipo(84,"Hospedaje",7000)) && tipos.ObtenerTiposViatico().Count==2,"Modificar tipo conserva identidad y cantidad");
            var ls=new LogicaLiquidaciones(); var fecha=new DateTime(2026,10,25);
            var l1=new Liquidacion {id=100,idEmpleado=42,inicio=fecha.AddHours(6),fin=fecha.AddDays(1).AddHours(21),justificacion="Trabajo"};
            Check(ls.AgregarLiquidacion(l1,new List<DetalleLiquidacion> {new DetalleLiquidacion {idLiquidacion=100,idViatico=83,monto=3000,fechaConsumo=fecha},new DetalleLiquidacion {idLiquidacion=100,idViatico=84,monto=7000,fechaConsumo=fecha.AddDays(1)}}),"Liquidaciones consume catalogos de Mango por IDs reales");
            var l2=new Liquidacion {id=101,idEmpleado=43,inicio=fecha.AddDays(1).AddHours(6),fin=fecha.AddDays(1).AddHours(21),justificacion="Trabajo 2"};
            Check(ls.AgregarLiquidacion(l2,new List<DetalleLiquidacion> {new DetalleLiquidacion {idLiquidacion=101,idViatico=84,monto=7000,fechaConsumo=fecha.AddDays(1)}}),"Segunda liquidacion para segundo empleado");
            Check(!empleados.EliminarEmpleado(42) && empleados.BuscarEmpleado(42)!=null,"Protege empleado con liquidaciones");
            Check(!tipos.EliminarTipoViatico(83) && tipos.BuscarTipoViatico(83)!=null,"Protege tipo utilizado en detalles");
            var inactivo=Emp(43,"00043"); inactivo.activo=false; Check(empleados.ModificarEmpleado(inactivo),"Permite desactivar empleado con historial");
            var tInactivo=Tipo(83,"Desayuno",3000); tInactivo.activo=false; Check(tipos.ModificarTipoViatico(tInactivo),"Permite desactivar tipo con historial");
            Check(tipos.ModificarTipoViatico(Tipo(84,"Hospedaje",9000)) && ls.calcularTotal(100)==10000,"Cambio de catalogo conserva monto historico");
            var normal=new Usuario {id=2,nombreCompleto="Usuario Normal",nombreUsuario="normal",clave="prueba",tipo="Normal",activo=true};
            Check(new LogicaUsuarios().AgregarUsuario(normal) && SesionActual.IniciarSesion("normal","prueba"),"Sesion Normal para probar permisos de catalogos");
            Check(empleados.AgregarEmpleado(Emp(45,"00045")) && empleados.EliminarEmpleado(45),"Usuario Normal crea y elimina empleado no relacionado");
            Check(tipos.AgregarTipoViatico(Tipo(85,"Otro",1)) && tipos.EliminarTipoViatico(85),"Usuario Normal crea y elimina tipo no relacionado");
            using(var f=new frmEmpleados()) {
                f.Show(); Application.DoEvents(); Check(C<DataGridView>(f,"grdEmpleados").Rows.Count==2,"Pantalla Empleados carga lista compartida");
                C<TextBox>(f,"txtIdentificacion").Text="42"; Event(f,"btnBuscar_Click"); Check(C<TextBox>(f,"txtCedula").Text=="00042","Buscar empleado completa los campos");
                FillEmpleado(f,44,"00044"); int b=Estructuras.listaBitacora.Count; Event(f,"btnGuardar_Click");
                Check(empleados.BuscarEmpleado(44)!=null && Estructuras.listaBitacora.Count==b+1 && Estructuras.listaBitacora[b].idUsuario==2,"Guardar empleado agrega una sola entrada de bitacora con autor correcto");
                Check(C<TextBox>(f,"txtIdentificacion").Text=="" && C<DataGridView>(f,"grdEmpleados").Rows.Count==3,"Alta actualiza tabla y limpia campos");
                FillEmpleado(f,44,"00044"); C<TextBox>(f,"txtNombre").Text="Cambio"; b=Estructuras.listaBitacora.Count; Event(f,"btnGuardar_Click");
                Check(empleados.BuscarEmpleado(44).nombre=="Cambio" && empleados.ObtenerEmpleados().Count==3 && Estructuras.listaBitacora.Count==b+1,"Guardar modifica empleado y registra una operacion");
                FillEmpleado(f,44,"00042"); b=Estructuras.listaBitacora.Count; Event(f,"btnGuardar_Click");
                Check(C<TextBox>(f,"txtCedula").Text=="00042" && empleados.BuscarEmpleado(44).cedula=="00044" && Estructuras.listaBitacora.Count==b,"Rechazo conserva campos, datos previos y bitacora");
                respuesta=DialogResult.No; Event(f,"btnEliminar_Click"); Check(empleados.BuscarEmpleado(44)!=null && Estructuras.listaBitacora.Count==b,"Cancelar eliminacion de empleado no modifica datos");
                respuesta=DialogResult.Yes; Event(f,"btnEliminar_Click"); respuesta=DialogResult.OK;
                Check(empleados.BuscarEmpleado(44)==null && Estructuras.listaBitacora.Count==b+1,"Eliminar empleado confirmado registra una operacion");
                f.Close();
            }
            using(var f=new frmEmpleados()) {f.Show(); Application.DoEvents(); Check(C<DataGridView>(f,"grdEmpleados").Rows.Count==2,"Volver a Empleados conserva listado"); f.Close();}
            using(var f=new frmTiposViatico()) {
                f.Show(); Application.DoEvents(); Check(C<DataGridView>(f,"grdTiposViatico").Rows.Count==2,"Pantalla Tipos carga catalogo compartido");
                C<TextBox>(f,"txtIdentificador").Text="84"; Event(f,"btnBuscar_Click"); Check(C<TextBox>(f,"txtMonto").Text=="9000","Buscar tipo recupera monto actual");
                FillTipo(f,85,"Transporte","1200"); int b=Estructuras.listaBitacora.Count; Event(f,"btnGuardar_Click");
                Check(tipos.BuscarTipoViatico(85)!=null && Estructuras.listaBitacora.Count==b+1 && Estructuras.listaBitacora[b].idUsuario==2,"Alta de tipo registra una sola operacion con autor correcto");
                FillTipo(f,85,"Transporte","1500"); b=Estructuras.listaBitacora.Count; Event(f,"btnGuardar_Click");
                Check(tipos.BuscarTipoViatico(85).monto==1500 && Estructuras.listaBitacora.Count==b+1,"Modificar tipo actualiza catalogo y bitacora");
                FillTipo(f,85,"Transporte","1.5"); b=Estructuras.listaBitacora.Count; Event(f,"btnGuardar_Click");
                Check(tipos.BuscarTipoViatico(85).monto==1500 && C<TextBox>(f,"txtMonto").Text=="1.5" && Estructuras.listaBitacora.Count==b,"Monto no entero rechazado sin cambios");
                respuesta=DialogResult.No; Event(f,"btnEliminar_Click"); Check(tipos.BuscarTipoViatico(85)!=null,"Cancelar eliminacion de tipo conserva registro");
                respuesta=DialogResult.Yes; Event(f,"btnEliminar_Click"); respuesta=DialogResult.OK;
                Check(tipos.BuscarTipoViatico(85)==null && Estructuras.listaBitacora.Count==b+1,"Eliminar tipo confirmado registra una operacion"); f.Close();
            }
            int bitacoraAntes=Estructuras.listaBitacora.Count;
            using(var f=new frmReporteConsumosFechas()) {
                f.Show(); Application.DoEvents(); C<DateTimePicker>(f,"dtpDesde").Value=fecha; C<DateTimePicker>(f,"dtpHasta").Value=fecha.AddDays(1); Event(f,"btnConsultar_Click");
                Check(C<DataGridView>(f,"grdConsumos").Rows.Count==3 && C<Label>(f,"lblMontoTotal").Text==Money(17000),"Reporte fechas integra ambos dias, empleados inactivos y montos historicos");
                C<DateTimePicker>(f,"dtpHasta").Value=fecha; Event(f,"btnConsultar_Click"); Check(C<DataGridView>(f,"grdConsumos").Rows.Count==1 && C<Label>(f,"lblMontoTotal").Text==Money(3000),"Filtro de un dia cambia filas y total");
                C<DateTimePicker>(f,"dtpDesde").Value=fecha.AddDays(2); Event(f,"btnConsultar_Click"); Check(C<DataGridView>(f,"grdConsumos").Rows.Count==0 && C<Label>(f,"lblMontoTotal").Text=="₡ 0","Rango invertido limpia resultados previos y total");
                C<DateTimePicker>(f,"dtpHasta").Value=fecha.AddDays(2); Event(f,"btnConsultar_Click"); Check(C<DataGridView>(f,"grdConsumos").Rows.Count==0 && C<Label>(f,"lblMontoTotal").Text==Money(0),"Rango sin consumos muestra cero"); f.Close();
            }
            using(var f=new frmReporteConsumosEmpleado()) {
                f.Show(); Application.DoEvents(); var combo=C<ComboBox>(f,"cmbEmpleado"); Check(combo.Items.Count==2 && combo.SelectedIndex==-1,"Reporte empleado incluye inactivos y exige seleccion explicita");
                combo.SelectedValue=43; Event(f,"btnConsultar_Click"); Check(C<DataGridView>(f,"grdConsumos").Rows.Count==1 && C<Label>(f,"lblMontoTotal").Text==Money(7000),"Reporte filtra por ID real incluso con nombres iguales");
                combo.SelectedValue=42; Event(f,"btnConsultar_Click"); Check(C<DataGridView>(f,"grdConsumos").Rows.Count==2 && C<Label>(f,"lblMontoTotal").Text==Money(10000),"Cambio de empleado actualiza resultados y monto historico");
                combo.SelectedIndex=-1; Event(f,"btnConsultar_Click"); Check(C<DataGridView>(f,"grdConsumos").Rows.Count==0 && C<Label>(f,"lblMontoTotal").Text=="₡ 0","Sin seleccion limpia resultados previos");
                combo.SelectedIndex=0; string n1=combo.Text; combo.SelectedIndex=1; Check(n1!=combo.Text && combo.Text.Contains("43"),"Selector distingue empleados homonimos por ID"); f.Close();
            }
            Check(Estructuras.listaBitacora.Count==bitacoraAntes,"Consultar reportes no genera movimientos de bitacora");
            Check(tipos.AgregarTipoViatico(Tipo(99,"Especial",1500000000)),"Prepara monto alto valido para comprobar acumulacion");
            for(int i=0;i<2;i++) {var l=new Liquidacion {id=200+i,idEmpleado=42,inicio=fecha.AddHours(6),fin=fecha.AddHours(21),justificacion="Limite"}; Check(ls.AgregarLiquidacion(l,new List<DetalleLiquidacion>{new DetalleLiquidacion{idLiquidacion=l.id,idViatico=99,monto=1500000000,fechaConsumo=fecha}}),"Liquidacion individual alta permanece dentro del limite int");}
            using(var f=new frmReporteConsumosEmpleado()) {f.Show(); Application.DoEvents(); C<ComboBox>(f,"cmbEmpleado").SelectedValue=42; Event(f,"btnConsultar_Click"); string total=C<Label>(f,"lblMontoTotal").Text; Check(total==Money(3000010000L),"Total acumulado por empleado admite montos superiores a int: "+total); f.Close();}
            using(var f=new frmReporteConsumosFechas()) {f.Show(); Application.DoEvents(); C<DateTimePicker>(f,"dtpDesde").Value=fecha; C<DateTimePicker>(f,"dtpHasta").Value=fecha.AddDays(1); Event(f,"btnConsultar_Click"); string total=C<Label>(f,"lblMontoTotal").Text; Check(total==Money(3000017000L),"Total acumulado por fechas admite montos superiores a int: "+total); f.Close();}
            SesionActual.CerrarSesion();
            using(var f=new frmEmpleados()) {f.Show(); Application.DoEvents(); Check(f.IsDisposed || !f.Visible,"Empleados no abre sin sesion"); f.Close();}
            using(var f=new frmTiposViatico()) {f.Show(); Application.DoEvents(); Check(f.IsDisposed || !f.Visible,"Tipos no abre sin sesion"); f.Close();}
            SesionActual.IniciarSesion("admin", "prueba");
            Check(!tipos.ModificarTipoViatico(Tipo(83,"Desayuno especial",3000)), "No reinterpreta horarios de un tipo con historial");
            foreach (string comida in new[] { "Desayuno", "Almuerzo", "Cena" })
            {
                int salida = comida == "Desayuno" ? 7 : comida == "Almuerzo" ? 10 : 16;
                int llegada = comida == "Desayuno" ? 10 : comida == "Almuerzo" ? 14 : 20;
                Check(ls.cumpleHorario(comida, fecha.AddHours(salida), fecha.AddHours(llegada), fecha), comida + " incluye limites exactos");
                Check(!ls.cumpleHorario(comida, fecha.AddHours(salida).AddMinutes(1), fecha.AddHours(llegada), fecha), comida + " rechaza salida un minuto tarde");
                Check(!ls.cumpleHorario(comida, fecha.AddHours(salida), fecha.AddHours(llegada).AddMinutes(-1), fecha), comida + " rechaza llegada un minuto antes");
            }
            Check(ls.cumpleHorario("Hospedaje", fecha.AddHours(23), fecha.AddDays(1).AddHours(1), fecha), "Hospedaje no aplica ventanas de comida");
            Check(!ls.cumpleHorario("Hospedaje", fecha, fecha.AddDays(1), fecha.AddDays(2)), "Incluso conceptos sin horario rechazan fecha fuera del viaje");
            Check(Identificadores.Siguiente(new[] {1, 3, int.MaxValue}, x => x)==2, "Generador usa un hueco sin desbordar por ID maximo");
            Check(Identificadores.Siguiente(new[] {int.MaxValue}, x => x)==1, "Generador mantiene IDs positivos con maximo existente");
            Estructuras.listaUsuarios.Add(new Usuario {id=int.MaxValue,nombreUsuario="max",activo=true,tipo="Normal",clave="max"});
            Estructuras.listaTiposViatico.Add(Tipo(int.MaxValue,"Maximo",1));
            Check(new LogicaCargaInicial().EjecutarCargaInicial(SesionActual.usuarioActual)>0, "Carga inicial compatible con identificadores externos maximos");
            Check(new LogicaCargaInicial().EjecutarCargaInicial(SesionActual.usuarioActual)==0, "Repetir carga no duplica ejemplos");
            using(var principal = new frmPrincipal())
            {
                principal.Show(); Application.DoEvents(); Event(principal,"btnLiquidaciones_Click");
                var panel=C<Panel>(principal,"pnlContenedor"); var actual=(frmLiquidaciones)panel.Tag;
                C<TextBox>(actual,"txtJustificacion").Text="Borrador conservado";
                respuesta=DialogResult.No; Event(principal,"btnEmpleados_Click");
                Check(panel.Tag==actual && !actual.IsDisposed && C<TextBox>(actual,"txtJustificacion").Text=="Borrador conservado", "Cancelar navegacion conserva formulario y borrador");
                principal.Close(); Application.DoEvents();
                Check(!principal.IsDisposed && SesionActual.HaySesionActiva(), "Cancelar cierre del menu conserva sesion");
                respuesta=DialogResult.Yes; Event(principal,"btnEmpleados_Click"); respuesta=DialogResult.OK;
                Check(actual.IsDisposed && panel.Tag is frmEmpleados && ls.buscarLiquidacion(999)==null, "Aceptar descarte libera formulario sin guardar cabecera parcial");
                foreach(string opcion in new[] {"btnEmpleados_Click","btnTiposViatico_Click","btnUsuarios_Click","btnRepFechas_Click","btnRepEmpleado_Click","btnRepOperaciones_Click","btnBitacora_Click","btnCargaInicial_Click"})
                {
                    Event(principal,opcion); Form f=(Form)panel.Tag; f.ClientSize=new System.Drawing.Size(420,300); Application.DoEvents();
                    Check(f.AutoScroll && f.Controls.Find("pagina",true).Length==1, "Pagina desplazable: "+opcion);
                    Control[] guardar=f.Controls.Find("btnGuardar",true);
                    if(guardar.Length>0)
                    {
                        var objetivo=guardar[0];
                        int y=objetivo.Top + objetivo.Parent.Top;
                        f.AutoScrollPosition=new System.Drawing.Point(0,y);
                        Application.DoEvents();
                        var rect=f.RectangleToClient(objetivo.RectangleToScreen(objetivo.ClientRectangle));
                        Check(f.ClientRectangle.Contains(rect), "Guardar accesible tras desplazar: "+opcion);
                    }
                }
                principal.Close(); Application.DoEvents();
                Check(!SesionActual.HaySesionActiva(), "Cerrar menu aceptado termina sesion");
            }
            timer.Dispose(); Console.WriteLine("TOTAL: "+count+" comprobaciones de cierre correctas."); return 0;
        } catch(Exception ex) {Console.Error.WriteLine(ex); return 1;}
    }
}
