using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using ControlViaticos.Logica;
using ControlViaticos.Objetos;
using ControlViaticos.Presentacion;

// Casos de estado al editar: conservar historial no autoriza asignaciones nuevas.
internal static class EstadosLiquidacionesRegression
{
    private static int comprobaciones;
    private static readonly DateTime fecha = new DateTime(2026, 10, 25);
    private static readonly LogicaLiquidaciones liquidaciones = new LogicaLiquidaciones();
    private static readonly LogicaEmpleados empleados = new LogicaEmpleados();
    private static readonly LogicaTiposViatico tipos = new LogicaTiposViatico();

    private static void Verificar(bool condicion, string mensaje)
    {
        if (!condicion) throw new Exception(mensaje);
        Console.WriteLine("OK: " + mensaje);
        comprobaciones++;
    }

    private static Empleado Empleado(int id, bool activo)
    {
        return new Empleado { id = id, cedula = "000" + id, nombre = "Ana",
            apellido1 = "Mora", apellido2 = "Solis", telefono = "00123",
            direccion = "San Jose", activo = activo };
    }

    private static Liquidacion Viaje(int id)
    {
        return new Liquidacion { id = id, idEmpleado = 42, inicio = fecha.AddHours(6),
            fin = fecha.AddDays(1).AddHours(21), justificacion = "Trabajo" };
    }

    private static DetalleLiquidacion Detalle(int idLiquidacion, int idViatico = 83)
    {
        return new DetalleLiquidacion { idLiquidacion = idLiquidacion, idViatico = idViatico,
            monto = idViatico == 83 ? 10000 : 5000, fechaConsumo = fecha };
    }

    private static T Control<T>(Form formulario, string nombre) where T : Control
    {
        return (T)formulario.Controls.Find(nombre, true)[0];
    }

    private static void Evento(Form formulario, string nombre)
    {
        formulario.GetType().GetMethod(nombre, BindingFlags.NonPublic | BindingFlags.Instance)
            .Invoke(formulario, new object[] { formulario, EventArgs.Empty });
        Application.DoEvents();
    }

    private static void ProbarLogica()
    {
        Usuario admin = new Usuario { id = 1, nombreCompleto = "Administrador",
            nombreUsuario = "admin", clave = "prueba", tipo = "Administrador", activo = true };
        Verificar(new LogicaAcceso().CrearPrimerAdministrador(admin) &&
            SesionActual.IniciarSesion("admin", "prueba"), "Prepara sesion de administrador");
        Verificar(empleados.AgregarEmpleado(Empleado(42, true)) &&
            empleados.AgregarEmpleado(Empleado(43, true)), "Prepara dos empleados distintos");
        Verificar(tipos.AgregarTipoViatico(new TipoViatico { id = 83, nombre = "Hospedaje", monto = 10000, activo = true }) &&
            tipos.AgregarTipoViatico(new TipoViatico { id = 84, nombre = "Transporte", monto = 5000, activo = true }),
            "Prepara catalogo activo");
        Verificar(liquidaciones.AgregarLiquidacion(Viaje(100), new List<DetalleLiquidacion> {
            Detalle(100), Detalle(100, 84) }), "Guarda cabecera con dos detalles");
        DetalleLiquidacion ajeno = Detalle(101);
        ajeno.id = liquidaciones.ObtenerDetalles(100)[0].id;
        Verificar(!liquidaciones.AgregarLiquidacion(Viaje(101), new List<DetalleLiquidacion> { ajeno }) &&
            liquidaciones.buscarLiquidacion(101) == null, "Rechaza un ID historico en una cabecera nueva sin excepciones");
        Verificar(empleados.ModificarEmpleado(Empleado(42, false)), "Desactiva empleado con historial");
        Verificar(!liquidaciones.AgregarLiquidacion(Viaje(101), new List<DetalleLiquidacion> { Detalle(101) }),
            "Un empleado inactivo no recibe una liquidacion nueva");
        Liquidacion cabecera = liquidaciones.buscarLiquidacion(100);
        cabecera.justificacion = "Correccion administrativa";
        Verificar(liquidaciones.modificarLiquidacion(cabecera, liquidaciones.ObtenerDetalles(100)),
            "Permite corregir justificacion conservando consumos del empleado inactivo");
        List<DetalleLiquidacion> detalles = liquidaciones.ObtenerDetalles(100);
        detalles.Add(Detalle(100));
        cabecera.justificacion = "No debe guardarse";
        Verificar(!liquidaciones.modificarLiquidacion(cabecera, detalles) && liquidaciones.ultimoError.Length > 0,
            "Rechaza nuevos pagos al empleado inactivo al editar");
        Verificar(liquidaciones.calcularTotal(100) == 15000 && liquidaciones.ObtenerDetalles(100).Count == 2 &&
            liquidaciones.buscarLiquidacion(100).justificacion == "Correccion administrativa",
            "El rechazo conserva cabecera, detalles y monto anterior");
        detalles = liquidaciones.ObtenerDetalles(100);
        detalles[0].fechaConsumo = fecha.AddDays(1);
        Verificar(!liquidaciones.modificarLiquidacion(liquidaciones.buscarLiquidacion(100), detalles),
            "No reasigna el consumo del empleado inactivo a otra fecha");
        Verificar(liquidaciones.ObtenerDetalles(100)[0].fechaConsumo == fecha,
            "El rechazo conserva la fecha de consumo historica");
        detalles = liquidaciones.ObtenerDetalles(100);
        detalles.RemoveAt(1);
        Verificar(liquidaciones.modificarLiquidacion(liquidaciones.buscarLiquidacion(100), detalles) &&
            liquidaciones.calcularTotal(100) == 10000, "Permite retirar un cobro sin asignar nuevos pagos al inactivo");
        Verificar(!liquidaciones.modificarLiquidacion(liquidaciones.buscarLiquidacion(100), new List<DetalleLiquidacion>()),
            "El retiro tampoco permite guardar una liquidacion vacia");
        Verificar(empleados.ModificarEmpleado(Empleado(42, true)), "Reactiva empleado para nuevas asignaciones");
        detalles = liquidaciones.ObtenerDetalles(100);
        detalles.Add(Detalle(100, 84));
        Verificar(liquidaciones.modificarLiquidacion(liquidaciones.buscarLiquidacion(100), detalles),
            "Al reactivar se permite agregar un concepto activo");

        Verificar(tipos.ModificarTipoViatico(new TipoViatico { id = 83, nombre = "Hospedaje", monto = 12000, activo = false }),
            "Desactiva tipo y cambia precio sin modificar historial");
        Verificar(liquidaciones.modificarLiquidacion(liquidaciones.buscarLiquidacion(100), liquidaciones.ObtenerDetalles(100)) &&
            liquidaciones.calcularTotal(100) == 15000, "Conserva monto historico de un tipo inactivo");
        detalles = liquidaciones.ObtenerDetalles(100);
        detalles[0].fechaConsumo = fecha.AddDays(1);
        Verificar(!liquidaciones.modificarLiquidacion(liquidaciones.buscarLiquidacion(100), detalles),
            "No traslada un tipo inactivo a una fecha nueva");
        cabecera = liquidaciones.buscarLiquidacion(100);
        cabecera.idEmpleado = 43;
        Verificar(!liquidaciones.modificarLiquidacion(cabecera, liquidaciones.ObtenerDetalles(100)),
            "No transfiere un tipo inactivo a otro empleado");
        Verificar(liquidaciones.buscarLiquidacion(100).idEmpleado == 42 &&
            liquidaciones.ObtenerDetalles(100)[0].fechaConsumo == fecha,
            "Rechazar reasignaciones conserva beneficiario y fecha originales");
        detalles = liquidaciones.ObtenerDetalles(100);
        DetalleLiquidacion nuevo = Detalle(100);
        nuevo.monto = 12000;
        detalles.Add(nuevo);
        Verificar(!liquidaciones.modificarLiquidacion(liquidaciones.buscarLiquidacion(100), detalles),
            "Un detalle sin identidad historica no puede usar un tipo inactivo");
        Verificar(tipos.ModificarTipoViatico(new TipoViatico { id = 83, nombre = "Hospedaje", monto = 12000, activo = true }) &&
            liquidaciones.modificarLiquidacion(cabecera, liquidaciones.ObtenerDetalles(100)),
            "Con ambos catalogos activos permite corregir el beneficiario");
        detalles = liquidaciones.ObtenerDetalles(100);
        detalles[0].fechaConsumo = fecha.AddDays(1);
        Verificar(liquidaciones.modificarLiquidacion(liquidaciones.buscarLiquidacion(100), detalles) &&
            liquidaciones.calcularTotal(100) == 15000, "Corregir fecha activa mantiene el precio historico");
    }

    private static void Buscar(Form formulario, int id)
    {
        Control<TextBox>(formulario, "txtConsecutivo").Text = id.ToString();
        Evento(formulario, "btnBuscar_Click");
    }

    private static void ProbarFormulario()
    {
        // La fecha y el monto de este ejemplo siguen el catalogo actual.
        DetalleLiquidacion original = Detalle(200);
        original.monto = 12000;
        Verificar(liquidaciones.AgregarLiquidacion(Viaje(200), new List<DetalleLiquidacion> { original }),
            "Prepara liquidacion para probar controles y bitacora");
        using (IDisposable dialogos = DialogosPrueba.Iniciar(() => DialogResult.Yes))
        using (frmLiquidaciones formulario = new frmLiquidaciones())
        {
            formulario.Show();
            Application.DoEvents();
            Buscar(formulario, 200);
            Control<ComboBox>(formulario, "cmbTipoViatico").SelectedValue = 84;
            Evento(formulario, "btnAgregarDetalle_Click");
            Verificar(Control<DataGridView>(formulario, "grdDetalle").Rows.Count == 2,
                "Prepara un detalle nuevo con empleado activo");
            empleados.ModificarEmpleado(Empleado(42, false));
            int bitacoraAntes = Estructuras.listaBitacora.Count;
            Evento(formulario, "btnGuardar_Click");
            Verificar(liquidaciones.ObtenerDetalles(200).Count == 1 && liquidaciones.calcularTotal(200) == 12000 &&
                Estructuras.listaBitacora.Count == bitacoraAntes,
                "Si se desactiva antes de guardar no hay cambios parciales ni bitacora de exito");
            Verificar(Control<DataGridView>(formulario, "grdDetalle").Rows.Count == 2,
                "Un guardado rechazado conserva la preparacion para corregirla");
            Evento(formulario, "btnAgregarDetalle_Click");
            Verificar(Control<DataGridView>(formulario, "grdDetalle").Rows.Count == 2,
                "Agregar avisa y no prepara mas pagos para el empleado inactivo");
            empleados.ModificarEmpleado(Empleado(42, true));
            Evento(formulario, "btnGuardar_Click");
            Verificar(liquidaciones.calcularTotal(200) == 17000 && Estructuras.listaBitacora.Count == bitacoraAntes + 1,
                "Reactivar y guardar completa el conjunto con una sola entrada de bitacora");
            Buscar(formulario, 200);
            Verificar(Control<DataGridView>(formulario, "grdDetalle").Rows.Count == 2,
                "Buscar recupera los detalles despues del guardado corregido");
            empleados.ModificarEmpleado(Empleado(42, false));
            Evento(formulario, "btnCancelar_Click");
            Buscar(formulario, 200);
            Verificar(((Empleado)Control<ComboBox>(formulario, "cmbEmpleado").SelectedItem).id == 42,
                "El empleado inactivo sigue disponible al consultar su historial");
            Control<ComboBox>(formulario, "cmbTipoViatico").SelectedValue = 84;
            Evento(formulario, "btnAgregarDetalle_Click");
            Verificar(Control<DataGridView>(formulario, "grdDetalle").Rows.Count == 2 &&
                liquidaciones.calcularTotal(200) == 17000, "Consultar historial inactivo no habilita nuevos detalles");
            formulario.Close();
        }
    }

    private static void ProbarPrecisionHoraria()
    {
        Verificar(tipos.AgregarTipoViatico(new TipoViatico {
            id = 85, nombre = "Desayuno", monto = 3000, activo = true }) &&
            tipos.AgregarTipoViatico(new TipoViatico {
            id = 86, nombre = "Almuerzo", monto = 5000, activo = true }) &&
            tipos.AgregarTipoViatico(new TipoViatico {
            id = 87, nombre = "Cena", monto = 5000, activo = true }),
            "Prepara comidas para probar limites visibles en pantalla");
        int[] horasSalida = { 7, 10, 16 };
        int[] horasLlegada = { 10, 14, 20 };
        string[] comidas = { "Desayuno", "Almuerzo", "Cena" };
        using (IDisposable dialogos = DialogosPrueba.Iniciar(() => DialogResult.Yes))
        {
            for (int i = 0; i < comidas.Length; i++)
            using (frmLiquidaciones formulario = new frmLiquidaciones())
            {
                formulario.Show();
                Application.DoEvents();
                Control<TextBox>(formulario, "txtConsecutivo").Text = (300 + i).ToString();
                Control<TextBox>(formulario, "txtJustificacion").Text = "Limite visible";
                Control<ComboBox>(formulario, "cmbEmpleado").SelectedValue = 43;
                // DateTimePicker conserva segundos al editar aunque solo muestre HH:mm.
                Control<DateTimePicker>(formulario, "dtpInicio").Value = fecha.AddHours(horasSalida[i]).AddSeconds(37);
                Control<DateTimePicker>(formulario, "dtpFin").Value = fecha.AddHours(horasLlegada[i]).AddSeconds(13);
                Control<DateTimePicker>(formulario, "dtpFechaConsumo").Value = fecha;
                Control<ComboBox>(formulario, "cmbTipoViatico").SelectedValue = 85 + i;
                Evento(formulario, "btnAgregarDetalle_Click");
                Verificar(Control<DataGridView>(formulario, "grdDetalle").Rows.Count == 1,
                    comidas[i] + " acepta el limite mostrado sin segundos ocultos");
                Evento(formulario, "btnGuardar_Click");
                Liquidacion guardada = liquidaciones.buscarLiquidacion(300 + i);
                Verificar(guardada != null && guardada.inicio == fecha.AddHours(horasSalida[i]) &&
                    guardada.fin == fecha.AddHours(horasLlegada[i]),
                    comidas[i] + " guarda las mismas horas visibles que se validaron");
                formulario.Close();
            }
        }
    }

    [STAThread]
    private static int Main()
    {
        try
        {
            Console.OutputEncoding = new System.Text.UTF8Encoding(false);
            Application.EnableVisualStyles();
            ProbarLogica();
            ProbarFormulario();
            ProbarPrecisionHoraria();
            Console.WriteLine(comprobaciones + " comprobaciones de estados correctas.");
            return 0;
        }
        catch (Exception error) { Console.Error.WriteLine(error); return 1; }
    }
}
