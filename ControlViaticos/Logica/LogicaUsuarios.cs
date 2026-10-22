using System;
using System.Collections.Generic;
using ControlViaticos.Objetos;

namespace ControlViaticos.Logica
{
    internal class LogicaUsuarios
    {
        // Devuelve false si los datos no son válidos o ya existe el usuario.
        public bool AgregarUsuario(Usuario miUsuario)
        {
            if (!SesionActual.EsAdministrador())
            {
                return false;
            }

            return AgregarUsuarioValidado(miUsuario);
        }

        // Excepción limitada a la configuración inicial, antes de poder iniciar sesión.
        internal bool AgregarPrimerAdministrador(Usuario administrador)
        {
            if (Estructuras.listaUsuarios.Count > 0 || administrador == null ||
                administrador.tipo != "Administrador" || !administrador.activo)
            {
                return false;
            }

            return AgregarUsuarioValidado(administrador);
        }

        private bool AgregarUsuarioValidado(Usuario miUsuario)
        {
            if (miUsuario == null || miUsuario.id <= 0 ||
                string.IsNullOrWhiteSpace(miUsuario.nombreCompleto) ||
                string.IsNullOrWhiteSpace(miUsuario.nombreUsuario) ||
                string.IsNullOrWhiteSpace(miUsuario.clave) ||
                (miUsuario.tipo != "Normal" && miUsuario.tipo != "Administrador"))
            {
                return false;
            }

            foreach (Usuario usuario in Estructuras.listaUsuarios)
            {
                if (usuario.id == miUsuario.id ||
                    string.Equals(usuario.nombreUsuario, miUsuario.nombreUsuario.Trim(),
                        StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            miUsuario.nombreCompleto = miUsuario.nombreCompleto.Trim();
            miUsuario.nombreUsuario = miUsuario.nombreUsuario.Trim();
            Estructuras.listaUsuarios.Add(miUsuario);
            return true;
        }

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

        public bool ModificarUsuario(Usuario miUsuario)
        {
            if (!SesionActual.EsAdministrador())
            {
                return false;
            }

            if (miUsuario == null || miUsuario.id <= 0 ||
                string.IsNullOrWhiteSpace(miUsuario.nombreCompleto) ||
                string.IsNullOrWhiteSpace(miUsuario.nombreUsuario) ||
                string.IsNullOrWhiteSpace(miUsuario.clave) ||
                (miUsuario.tipo != "Normal" && miUsuario.tipo != "Administrador"))
            {
                return false;
            }

            Usuario encontrado = BuscarUsuario(miUsuario.id);
            if (encontrado == null)
            {
                return false;
            }

            foreach (Usuario usuario in Estructuras.listaUsuarios)
            {
                if (usuario.id != miUsuario.id &&
                    string.Equals(usuario.nombreUsuario, miUsuario.nombreUsuario.Trim(),
                        StringComparison.OrdinalIgnoreCase))
                {
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

        public bool EliminarUsuario(int id)
        {
            if (!SesionActual.EsAdministrador())
            {
                return false;
            }

            Usuario encontrado = BuscarUsuario(id);
            if (encontrado == null)
            {
                return false;
            }

            // Conserva el usuario si tiene operaciones registradas en la bitácora.
            foreach (RegistroBitacora registro in Estructuras.listaBitacora)
            {
                if (registro.idUsuario == id)
                {
                    return false;
                }
            }

            Estructuras.listaUsuarios.Remove(encontrado);
            return true;
        }

        public List<Usuario> ObtenerUsuarios()
        {
            return new List<Usuario>(Estructuras.listaUsuarios);
        }
    }
}
