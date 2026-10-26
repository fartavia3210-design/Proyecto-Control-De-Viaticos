using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using ControlViaticos.Logica;
using ControlViaticos.Objetos;
using ControlViaticos.Presentacion;

internal static class LiquidacionesRegression
{
    private static int comprobaciones;
    private static void Verificar(bool condicion, string mensaje)
    {
        if (!condicion) throw new Exception(mensaje);
        comprobaciones++;
        Console.WriteLine("OK: " + mensaje);
    }
    private static Liquidacion Viaje(int id)
    {
        return new Liquidacion { id = id, idEmpleado = 42, inicio = new DateTime(2026, 10, 25, 6, 0, 0),
            fin = new DateTime(2026, 10, 25, 15, 0, 0), justificacion = "Visita de trabajo" };
    }
    private static List<DetalleLiquidacion> Detalles(int id)
    {
        return new List<DetalleLiquidacion> { new DetalleLiquidacion { idLiquidacion = id, idViatico = 83, monto = 3000, fechaConsumo = Viaje(id).inicio.Date } };
    }
    private static T Control<T>(Form f, string nombre) where T : System.Windows.Forms.Control
    {
        return (T)f.Controls.Find(nombre, true)[0];
    }
    private static void Evento(Form f, string nombre)
    {
        f.GetType().GetMethod(nombre, BindingFlags.NonPublic | BindingFlags.Instance).Invoke(f, new object[] { f, EventArgs.Empty });
    }
    [STAThread]
    private static int Main()
    {
        try
        {
            Usuario autor = new Usuario { id = 1, nombreCompleto = "Prueba", nombreUsuario = "prueba", clave = "prueba", tipo = "Administrador", activo = true };
            Estructuras.listaUsuarios.Add(autor);
            SesionActual.usuarioActual = autor;
            Estructuras.listaEmpleados.Add(new Empleado { id = 42, nombre = "Ana", apellido1 = "Mora", apellido2 = "Solís", activo = true });
            TipoViatico desayuno = new TipoViatico { id = 83, nombre = "Desayuno", monto = 3000, activo = true };
            Estructuras.listaTiposViatico.Add(desayuno);
            LogicaLiquidaciones logica = new LogicaLiquidaciones();
            Verificar(!logica.AgregarLiquidacion(Viaje(1), new List<DetalleLiquidacion>()) && logica.ultimoError.Length > 0, "Rechaza cabecera sin detalles");
            Verificar(Estructuras.listaLiquidaciones.Count == 0 && Estructuras.listaDetalles.Count == 0, "Rechazo no inserta datos parciales");
            List<DetalleLiquidacion> preparados = Detalles(1);
            Verificar(logica.AgregarLiquidacion(Viaje(1), preparados), "Guarda cabecera y viático con IDs distintos de índices");
            preparados[0].monto = 999;
            Verificar(logica.calcularTotal(1) == 3000, "La preparación no comparte objetos con los datos guardados");
            Verificar(!logica.AgregarLiquidacion(Viaje(1), Detalles(1)), "Rechaza consecutivo duplicado");
            Verificar(!logica.modificarLiquidacion(Viaje(1), new List<DetalleLiquidacion>()), "Rechaza modificación sin detalles");
            Verificar(logica.ObtenerDetalles(1).Count == 1 && logica.calcularTotal(1) == 3000, "Quitar último detalle y rechazar conserva versión anterior");
            Liquidacion modificada = Viaje(1);
            modificada.justificacion = "No debe persistir";
            List<DetalleLiquidacion> invalidos = Detalles(1);
            invalidos.Add(new DetalleLiquidacion { idLiquidacion = 1, idViatico = 999, monto = 500, fechaConsumo = Viaje(1).inicio.Date });
            Verificar(!logica.modificarLiquidacion(modificada, invalidos) && logica.buscarLiquidacion(1).justificacion == "Visita de trabajo", "Un detalle inválido conserva toda la cabecera anterior");
            Liquidacion copia = logica.buscarLiquidacion(1);
            copia.justificacion = "Cambio no guardado";
            logica.ObtenerDetalles(1)[0].monto = 1;
            Verificar(logica.buscarLiquidacion(1).justificacion == "Visita de trabajo" && logica.calcularTotal(1) == 3000, "Consultar o cancelar no cambia objetos almacenados");
            desayuno.monto = 4500;
            Verificar(logica.modificarLiquidacion(Viaje(1), logica.ObtenerDetalles(1)) && logica.calcularTotal(1) == 3000, "Conserva monto histórico al cambiar catálogo");
            desayuno.activo = false;
            Verificar(logica.modificarLiquidacion(Viaje(1), logica.ObtenerDetalles(1)), "Permite conservar viático histórico inactivo");
            Verificar(!logica.AgregarLiquidacion(Viaje(2), Detalles(2)), "Rechaza nueva asignación inactiva");
            desayuno.activo = true;
            desayuno.monto = 3000;
            Verificar(logica.AgregarLiquidacion(Viaje(2), Detalles(2)), "Guarda segundo consecutivo");
            SesionActual.CerrarSesion();
            Verificar(!logica.eliminarLiquidacion(1) && !logica.modificarLiquidacion(Viaje(1), Detalles(1)), "Sin sesión no modifica ni elimina");
            SesionActual.usuarioActual = autor;

            Application.EnableVisualStyles();
            using (frmLiquidaciones formulario = new frmLiquidaciones())
            {
                formulario.Show();
                Application.DoEvents();
                Verificar(Control<DataGridView>(formulario, "grdLiquidaciones").Rows.Count == 2, "Al abrir carga ambas liquidaciones sin duplicarlas");
                Control<TextBox>(formulario, "txtConsecutivo").Text = "1";
                Evento(formulario, "btnBuscar_Click");
                Verificar((int)Control<ComboBox>(formulario, "cmbEmpleado").SelectedValue == 42, "Buscar recupera ID real del empleado");
                DataGridView tabla = Control<DataGridView>(formulario, "grdDetalle");
                Verificar(tabla.Rows[0].Cells["idViatico"].FormattedValue.ToString() == "Desayuno", "La tabla muestra nombre y conserva el objeto del detalle");
                Evento(formulario, "btnQuitarDetalle_Click");
                Verificar(tabla.Rows.Count == 0 && logica.ObtenerDetalles(1).Count == 1, "Quitar último detalle solo modifica preparación");
                formulario.ClientSize = new System.Drawing.Size(680, 400);
                formulario.ScrollControlIntoView(Control<DataGridView>(formulario, "grdLiquidaciones"));
                Verificar(formulario.AutoScroll && Control<DataGridView>(formulario, "grdLiquidaciones").Visible, "Tabla inferior accesible mediante desplazamiento en ventana pequeña");
                formulario.ClientSize = new System.Drawing.Size(1000, 900);
                formulario.ClientSize = new System.Drawing.Size(680, 400);
            }
            using (frmLiquidaciones regreso = new frmLiquidaciones())
            {
                regreso.Show();
                Application.DoEvents();
                Verificar(Control<DataGridView>(regreso, "grdLiquidaciones").Rows.Count == 2 && logica.calcularTotal(1) == 3000, "Cerrar preparación y volver conserva datos y listado");
                Control<TextBox>(regreso, "txtConsecutivo").Text = "3";
                Control<ComboBox>(regreso, "cmbTipoViatico").SelectedValue = 83;
                Control<DateTimePicker>(regreso, "dtpInicio").Value = Viaje(3).inicio;
                Control<DateTimePicker>(regreso, "dtpFin").Value = Viaje(3).fin;
                Evento(regreso, "btnAgregarDetalle_Click");
                Verificar(Control<DataGridView>(regreso, "grdDetalle").Rows.Count == 1 && logica.buscarLiquidacion(3) == null, "Permite agregar antes de guardar sin insertar cabecera");
            }
            Verificar(logica.buscarLiquidacion(3) == null && Estructuras.listaDetalles.Count == 2, "Cerrar borrador nuevo no deja registros parciales");
            Verificar(Estructuras.listaBitacora.Count == 0, "Preparación, consultas y cancelaciones no registran éxitos");
            Verificar(logica.eliminarLiquidacion(1) && logica.buscarLiquidacion(1) == null && logica.ObtenerDetalles(1).Count == 0, "Eliminar borra cabecera y detalles conjuntamente");
            Liquidacion viajeLargo = Viaje(4);
            viajeLargo.inicio = new DateTime(2026, 10, 25, 8, 0, 0);
            viajeLargo.fin = new DateTime(2026, 10, 27, 21, 0, 0);
            List<DetalleLiquidacion> primerDia = new List<DetalleLiquidacion> {
                new DetalleLiquidacion { idLiquidacion = 4, idViatico = 83, monto = 3000,
                    fechaConsumo = new DateTime(2026, 10, 25) } };
            Verificar(!logica.AgregarLiquidacion(viajeLargo, primerDia),
                "Rechaza desayuno del primer día cuando la salida es después de las siete");
            List<DetalleLiquidacion> diasValidos = new List<DetalleLiquidacion> {
                new DetalleLiquidacion { idLiquidacion = 4, idViatico = 83, monto = 3000,
                    fechaConsumo = new DateTime(2026, 10, 26) },
                new DetalleLiquidacion { idLiquidacion = 4, idViatico = 83, monto = 3000,
                    fechaConsumo = new DateTime(2026, 10, 27) } };
            Verificar(logica.AgregarLiquidacion(viajeLargo, diasValidos),
                "Guarda viáticos en días intermedio y final del viaje");
            List<DetalleLiquidacion> historicos = logica.ObtenerDetalles(4);
            Verificar(historicos.Count == 2 && historicos[0].id > 0 &&
                historicos[1].id > historicos[0].id && historicos[0].fechaConsumo.Day == 26,
                "Asigna identificadores distintos y conserva fechas por detalle");
            List<DetalleLiquidacion> fueraDeViaje = logica.ObtenerDetalles(4);
            fueraDeViaje[0].fechaConsumo = new DateTime(2026, 10, 28);
            Verificar(!logica.modificarLiquidacion(viajeLargo, fueraDeViaje) &&
                logica.ObtenerDetalles(4)[0].fechaConsumo.Day == 26,
                "Rechaza fecha fuera del viaje sin alterar lo guardado");
            List<DetalleLiquidacion> idDuplicado = logica.ObtenerDetalles(4);
            idDuplicado[1].id = idDuplicado[0].id;
            Verificar(!logica.modificarLiquidacion(viajeLargo, idDuplicado),
                "Rechaza la repetición de un identificador histórico");
            LogicaReportesConsumo reportes = new LogicaReportesConsumo();
            Verificar(reportes.ConsultarConsumosPorFechas(new DateTime(2026, 10, 26),
                new DateTime(2026, 10, 26)).Count == 1,
                "Consulta consumos por fecha incluyendo ambos límites");
            Verificar(reportes.ConsultarConsumosPorFechas(new DateTime(2026, 10, 28),
                new DateTime(2026, 10, 25)).Count == 0,
                "Un rango invertido no produce consumos");
            Verificar(reportes.ConsultarConsumosPorEmpleado(42).Count == 3,
                "Consulta todos los consumos del empleado por su ID real");
            desayuno.monto = 4500;
            Verificar(reportes.ConsultarConsumosPorEmpleado(42)[0].monto == 3000,
                "El reporte conserva el monto histórico del detalle");
            desayuno.monto = 3000;
            Console.WriteLine(comprobaciones + " comprobaciones correctas.");
            return 0;
        }
        catch (Exception error) { Console.Error.WriteLine(error); return 1; }
    }
}
