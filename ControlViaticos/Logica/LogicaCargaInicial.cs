using System;
using ControlViaticos.Objetos;

namespace ControlViaticos.Logica
{
    internal class LogicaCargaInicial
    {
        // Devuelve -1 si no tiene permiso; en otro caso, la cantidad de registros agregados.
        public int EjecutarCargaInicial(Usuario administrador)
        {
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

            int agregados = 0;
            int idUsuario = 1;
            bool existeUsuario = false;
            foreach (Usuario usuario in Estructuras.listaUsuarios)
            {
                if (usuario.id >= idUsuario)
                {
                    idUsuario = usuario.id + 1;
                }
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
                if (logicaUsuarios.AgregarUsuario(nuevoUsuario))
                {
                    agregados++;
                }
            }

            int idEmpleado = 1;
            bool existeEmpleado = false;
            foreach (Empleado empleado in Estructuras.listaEmpleados)
            {
                if (empleado.id >= idEmpleado)
                {
                    idEmpleado = empleado.id + 1;
                }
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
                Estructuras.listaEmpleados.Add(nuevoEmpleado);
                agregados++;
            }

            string[] nombres = { "Desayuno", "Almuerzo", "Cena" };
            int[] montos = { 3000, 5000, 5000 };
            for (int i = 0; i < nombres.Length; i++)
            {
                int idViatico = 1;
                bool existeViatico = false;
                foreach (TipoViatico viatico in Estructuras.listaTiposViatico)
                {
                    if (viatico.id >= idViatico)
                    {
                        idViatico = viatico.id + 1;
                    }
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
                    Estructuras.listaTiposViatico.Add(nuevoViatico);
                    agregados++;
                }
            }

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
