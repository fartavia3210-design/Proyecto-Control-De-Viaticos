using System;
using ControlViaticos.Objetos;

namespace ControlViaticos.Logica
{
    internal class LogicaAcceso
    {
        // Se llama explícitamente para configurar la primera cuenta, sin cargar ejemplos.
        public bool CrearPrimerAdministrador(Usuario administrador)
        {
            if (Estructuras.listaUsuarios.Count > 0 || administrador == null)
            {
                return false;
            }

            if (administrador.tipo != "Administrador" || !administrador.activo)
            {
                return false;
            }

            LogicaUsuarios logicaUsuarios = new LogicaUsuarios();
            return logicaUsuarios.AgregarPrimerAdministrador(administrador);
        }

        // Devuelve el usuario autorizado o null cuando el acceso es rechazado.
        public Usuario ValidarAcceso(string nombreUsuario, string clave)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario) || string.IsNullOrWhiteSpace(clave))
            {
                return null;
            }

            foreach (Usuario usuario in Estructuras.listaUsuarios)
            {
                if (string.Equals(usuario.nombreUsuario, nombreUsuario.Trim(), StringComparison.OrdinalIgnoreCase) && usuario.clave == clave && usuario.activo)
                {
                    return usuario;
                }
            }

            return null;
        }
    }
}
