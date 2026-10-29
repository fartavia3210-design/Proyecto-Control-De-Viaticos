using System;
using System.Collections.Generic;
using ControlViaticos.Objetos;

namespace ControlViaticos.Logica
{
    internal class LogicaBitacora
    {
        // Llamar únicamente después de completar correctamente la operación.
        public bool RegistrarOperacion(Usuario usuario, string seccion, string movimiento)
        {
            if (usuario == null || string.IsNullOrWhiteSpace(seccion) || string.IsNullOrWhiteSpace(movimiento))
            {
                return false;
            }

            LogicaUsuarios logicaUsuarios = new LogicaUsuarios();
            Usuario encontrado = logicaUsuarios.BuscarUsuario(usuario.id);

            if (encontrado == null || !encontrado.activo)
            {
                return false;
            }

            int consecutivo = Identificadores.Siguiente(Estructuras.listaBitacora);
            if (consecutivo == 0)
            {
                return false;
            }

            RegistroBitacora nuevoRegistro = new RegistroBitacora();
            nuevoRegistro.id = consecutivo;
            nuevoRegistro.idUsuario = encontrado.id;
            nuevoRegistro.nombreUsuario = encontrado.nombreUsuario;
            nuevoRegistro.fechaHora = DateTime.Now;
            nuevoRegistro.seccion = seccion.Trim();
            nuevoRegistro.movimiento = movimiento.Trim();

            Estructuras.listaBitacora.Add(nuevoRegistro);
            return true;
        }

        // Obtiene los registros de la bitácora.
        public List<RegistroBitacora> ObtenerBitacora()
        {
            return new List<RegistroBitacora>(Estructuras.listaBitacora);
        }

        // Filtra las operaciones por usuario y fechas.
        public List<RegistroBitacora> ConsultarOperaciones(int idUsuario, DateTime desde, DateTime hasta)
        {
            List<RegistroBitacora> resultado = new List<RegistroBitacora>();
            if (idUsuario <= 0 || desde.Date > hasta.Date)
            {
                return resultado;
            }

            foreach (RegistroBitacora registro in Estructuras.listaBitacora)
            {
                // Incluye todas las horas de los días inicial y final.
                if (registro.idUsuario == idUsuario && registro.fechaHora.Date >= desde.Date && registro.fechaHora.Date <= hasta.Date)
                {
                    resultado.Add(registro);
                }
            }

            return resultado;
        }
    }
}
