using System;
using System.Collections.Generic;
using ControlViaticos.Objetos;

namespace ControlViaticos.Logica
{
    internal class LogicaCargaInicial
    {
        public string ultimoError = "";

        // Devuelve -1 si no tiene permiso; en otro caso, la cantidad de registros agregados.
        public int EjecutarCargaInicial(Usuario administrador)
        {
            ultimoError = "";
            if (!SesionActual.EsAdministrador() || administrador == null)
            {
                return -1;
            }

            if (administrador.id != SesionActual.usuarioActual.id)
            {
                return -1;
            }

            LogicaUsuarios logicaUsuarios = new LogicaUsuarios();
            Usuario encontrado = logicaUsuarios.BuscarUsuario(administrador.id);
            if (encontrado == null || !encontrado.activo || encontrado.tipo != "Administrador")
            {
                return -1;
            }

            List<Usuario> usuariosNuevos = new List<Usuario>();
            List<Empleado> empleadosNuevos = new List<Empleado>();
            List<TipoViatico> tiposNuevos = new List<TipoViatico>();
            List<TipoViatico> catalogo = new List<TipoViatico>(Estructuras.listaTiposViatico);
            int agregados = 0;
            int idUsuario = Identificadores.Siguiente(Estructuras.listaUsuarios, u => u.id);
            bool existeUsuario = false;
            foreach (Usuario usuario in Estructuras.listaUsuarios)
            {
                if (usuario.nombreUsuario != null &&
                    string.Equals(usuario.nombreUsuario.Trim(), "usuarioEjemplo", StringComparison.OrdinalIgnoreCase))
                {
                    existeUsuario = true;
                }
            }

            if (!existeUsuario)
            {
                Usuario nuevoUsuario = new Usuario();
                nuevoUsuario.id = idUsuario;
                nuevoUsuario.nombreCompleto = "Usuario de ejemplo";
                nuevoUsuario.nombreUsuario = "usuarioEjemplo";
                nuevoUsuario.clave = "ejemplo123";
                nuevoUsuario.tipo = "Normal";
                nuevoUsuario.activo = true;
                usuariosNuevos.Add(nuevoUsuario);
                agregados++;
            }

            int idEmpleado = Identificadores.Siguiente(Estructuras.listaEmpleados, e => e.id);
            bool existeEmpleado = false;
            foreach (Empleado empleado in Estructuras.listaEmpleados)
            {
                if (empleado.cedula != null &&
                    string.Equals(empleado.cedula.Trim(), "000000001", StringComparison.OrdinalIgnoreCase))
                {
                    existeEmpleado = true;
                }
            }

            if (!existeEmpleado)
            {
                // Datos ficticios destinados a probar la aplicación.
                Empleado nuevoEmpleado = new Empleado();
                nuevoEmpleado.id = idEmpleado;
                nuevoEmpleado.cedula = "000000001";
                nuevoEmpleado.nombre = "Empleado";
                nuevoEmpleado.apellido1 = "De";
                nuevoEmpleado.apellido2 = "Ejemplo";
                nuevoEmpleado.telefono = "00000000";
                nuevoEmpleado.direccion = "Dirección de ejemplo";
                nuevoEmpleado.activo = true;
                empleadosNuevos.Add(nuevoEmpleado);
                agregados++;
            }

            string[] nombres = { "Desayuno", "Almuerzo", "Cena" };
            int[] montos = { 3000, 5000, 5000 };
            for (int i = 0; i < nombres.Length; i++)
            {
                int idViatico = Identificadores.Siguiente(catalogo, t => t.id);
                bool existeViatico = false;
                foreach (TipoViatico viatico in catalogo)
                {
                    if (viatico.nombre != null &&
                        string.Equals(viatico.nombre.Trim(), nombres[i], StringComparison.OrdinalIgnoreCase))
                    {
                        existeViatico = true;
                    }
                }

                if (!existeViatico)
                {
                    TipoViatico nuevoViatico = new TipoViatico();
                    nuevoViatico.id = idViatico;
                    nuevoViatico.nombre = nombres[i];
                    nuevoViatico.monto = montos[i];
                    nuevoViatico.activo = true;
                    catalogo.Add(nuevoViatico);
                    tiposNuevos.Add(nuevoViatico);
                    agregados++;
                }
            }

            if (usuariosNuevos.Exists(u => u.id == 0) || empleadosNuevos.Exists(e => e.id == 0) ||
                tiposNuevos.Exists(t => t.id == 0))
            {
                ultimoError = "No quedan identificadores disponibles. No se aplicó la carga.";
                return -2;
            }
            foreach (Usuario nuevo in usuariosNuevos)
                if (!logicaUsuarios.AgregarUsuario(nuevo))
                {
                    ultimoError = logicaUsuarios.ultimoError;
                    return -2;
                }
            Estructuras.listaEmpleados.AddRange(empleadosNuevos);
            Estructuras.listaTiposViatico.AddRange(tiposNuevos);

            if (agregados > 0)
            {
                LogicaBitacora logicaBitacora = new LogicaBitacora();
                logicaBitacora.RegistrarOperacion(encontrado, "Carga inicial",
                    "Agregó " + agregados + " registros de ejemplo");
            }

            return agregados;
        }
    }
}
