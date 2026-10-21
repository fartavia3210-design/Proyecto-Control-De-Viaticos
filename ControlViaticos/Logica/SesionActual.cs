using ControlViaticos.Objetos;

namespace ControlViaticos.Logica
{
    internal class SesionActual
    {
        // Compartido por las pantallas durante la ejecución de la aplicación.
        internal static Usuario usuarioActual;

        public static bool IniciarSesion(string nombreUsuario, string clave)
        {
            // Un intento rechazado no debe conservar una sesión anterior.
            CerrarSesion();

            LogicaAcceso logicaAcceso = new LogicaAcceso();
            Usuario encontrado = logicaAcceso.ValidarAcceso(nombreUsuario, clave);
            if (encontrado == null)
            {
                return false;
            }

            usuarioActual = encontrado;
            return true;
        }

        public static bool HaySesionActiva()
        {
            if (usuarioActual == null)
            {
                return false;
            }

            LogicaUsuarios logicaUsuarios = new LogicaUsuarios();
            Usuario encontrado = logicaUsuarios.BuscarUsuario(usuarioActual.id);
            if (encontrado == null || !encontrado.activo)
            {
                CerrarSesion();
                return false;
            }

            usuarioActual = encontrado;
            return true;
        }

        public static bool EsAdministrador()
        {
            if (!HaySesionActiva())
            {
                return false;
            }

            return usuarioActual.tipo == "Administrador";
        }

        public static void CerrarSesion()
        {
            usuarioActual = null;
        }
    }
}
