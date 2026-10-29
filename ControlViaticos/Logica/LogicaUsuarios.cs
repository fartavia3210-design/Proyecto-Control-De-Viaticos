using System;
using System.Collections.Generic;
using ControlViaticos.Objetos;

namespace ControlViaticos.Logica
{
    internal class LogicaUsuarios
    {
        public string ultimoError = "";

        // Devuelve false si los datos no son válidos o ya existe el usuario.
        public bool AgregarUsuario(Usuario miUsuario)
        {
            ultimoError = "";
            if (!SesionActual.EsAdministrador())
            {
                ultimoError = "Solo un administrador puede agregar usuarios.";
                return false;
            }

            return AgregarUsuarioValidado(miUsuario);
        }

        // Excepción limitada a la configuración inicial, antes de poder iniciar sesión.
        internal bool AgregarPrimerAdministrador(Usuario administrador)
        {
            ultimoError = "";
            if (Estructuras.listaUsuarios.Count > 0 || administrador == null || administrador.tipo != "Administrador" || !administrador.activo)
            {
                ultimoError = "La configuración inicial requiere una lista vacía y un administrador activo.";
                return false;
            }

            return AgregarUsuarioValidado(administrador);
        }

        // Valida los datos y agrega el usuario.
        private bool AgregarUsuarioValidado(Usuario miUsuario)
        {
            if (miUsuario == null || miUsuario.id <= 0 || string.IsNullOrWhiteSpace(miUsuario.nombreCompleto) || string.IsNullOrWhiteSpace(miUsuario.nombreUsuario) || string.IsNullOrWhiteSpace(miUsuario.clave) || (miUsuario.tipo != "Normal" && miUsuario.tipo != "Administrador"))
            {
                ultimoError = "Complete todos los datos, use una identificación positiva y seleccione un tipo válido.";
                return false;
            }

            foreach (Usuario usuario in Estructuras.listaUsuarios)
            {
                if (usuario.id == miUsuario.id || string.Equals(usuario.nombreUsuario, miUsuario.nombreUsuario.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    ultimoError = "Ya existe un usuario con esa identificación o nombre de usuario.";
                    return false;
                }
            }

            miUsuario.nombreCompleto = miUsuario.nombreCompleto.Trim();
            miUsuario.nombreUsuario = miUsuario.nombreUsuario.Trim();
            Estructuras.listaUsuarios.Add(miUsuario);
            return true;
        }

        // Busca el usuario.
        public Usuario BuscarUsuario(int id)
        {
            foreach (Usuario usuario in Estructuras.listaUsuarios)
            {
                if (usuario.id == id)
                {
                    return usuario;
                }
            }
            return null;
        }

        // Valida y modifica el usuario.
        public bool ModificarUsuario(Usuario miUsuario)
        {
            ultimoError = "";
            if (!SesionActual.EsAdministrador())
            {
                ultimoError = "Solo un administrador puede modificar usuarios.";
                return false;
            }

            if (miUsuario == null || miUsuario.id <= 0 || string.IsNullOrWhiteSpace(miUsuario.nombreCompleto) || string.IsNullOrWhiteSpace(miUsuario.nombreUsuario) || string.IsNullOrWhiteSpace(miUsuario.clave) || (miUsuario.tipo != "Normal" && miUsuario.tipo != "Administrador"))
            {
                ultimoError = "Complete todos los datos, use una identificación positiva y seleccione un tipo válido.";
                return false;
            }

            Usuario encontrado = BuscarUsuario(miUsuario.id);
            if (encontrado == null)
            {
                ultimoError = "No existe un usuario con esa identificación.";
                return false;
            }

            if (encontrado.id == SesionActual.usuarioActual.id && (!miUsuario.activo || miUsuario.tipo != "Administrador"))
            {
                ultimoError = "No puede desactivar su cuenta ni quitarse el rol de administrador durante la sesión.";
                return false;
            }

            if (encontrado.tipo == "Administrador" && encontrado.activo && (!miUsuario.activo || miUsuario.tipo != "Administrador") && ContarAdministradoresActivos() <= 1)
            {
                ultimoError = "Debe permanecer al menos un administrador activo.";
                return false;
            }

            foreach (Usuario usuario in Estructuras.listaUsuarios)
            {
                if (usuario.id != miUsuario.id && string.Equals(usuario.nombreUsuario, miUsuario.nombreUsuario.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    ultimoError = "Ya existe otro usuario con ese nombre de usuario.";
                    return false;
                }
            }

            encontrado.nombreCompleto = miUsuario.nombreCompleto.Trim();
            encontrado.nombreUsuario = miUsuario.nombreUsuario.Trim();
            encontrado.clave = miUsuario.clave;
            encontrado.tipo = miUsuario.tipo;
            encontrado.activo = miUsuario.activo;
            return true;
        }

        // Valida y elimina el usuario.
        public bool EliminarUsuario(int id)
        {
            ultimoError = "";
            if (!SesionActual.EsAdministrador())
            {
                ultimoError = "Solo un administrador puede eliminar usuarios.";
                return false;
            }

            Usuario encontrado = BuscarUsuario(id);
            if (encontrado == null)
            {
                ultimoError = "No existe un usuario con esa identificación.";
                return false;
            }

            if (encontrado.id == SesionActual.usuarioActual.id)
            {
                ultimoError = "No puede eliminar la cuenta que está usando.";
                return false;
            }

            if (encontrado.tipo == "Administrador" && encontrado.activo && ContarAdministradoresActivos() <= 1)
            {
                ultimoError = "Debe permanecer al menos un administrador activo.";
                return false;
            }

            // Conserva el usuario si tiene operaciones registradas en la bitácora.
            foreach (RegistroBitacora registro in Estructuras.listaBitacora)
            {
                if (registro.idUsuario == id)
                {
                    ultimoError = "El usuario tiene historial en bitácora. Puede marcarlo como inactivo.";
                    return false;
                }
            }

            Estructuras.listaUsuarios.Remove(encontrado);
            return true;
        }

        // Obtiene los usuarios.
        public List<Usuario> ObtenerUsuarios()
        {
            return new List<Usuario>(Estructuras.listaUsuarios);
        }

        // Cuenta los administradores activos.
        private int ContarAdministradoresActivos()
        {
            int cantidad = 0;
            foreach (Usuario usuario in Estructuras.listaUsuarios)
            {
                if (usuario.tipo == "Administrador" && usuario.activo)
                {
                    cantidad++;
                }
            }
            return cantidad;
        }
    }
}
