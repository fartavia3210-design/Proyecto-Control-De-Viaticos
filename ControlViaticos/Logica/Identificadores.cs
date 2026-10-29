using System.Collections.Generic;
using ControlViaticos.Objetos;

namespace ControlViaticos.Logica
{
    internal static class Identificadores
    {
        // Busca un ID libre sin sumar al mayor ID externo.
        public static int Siguiente(int[] registros)
        {
            return Siguiente(new List<int>(registros));
        }

        // Busca un identificador positivo disponible.
        public static int Siguiente(List<int> registros)
        {
            List<int> ocupados = new List<int>(registros);
            ocupados.Sort();
            int candidato = 1;
            foreach (int ocupado in ocupados)
            {
                if (ocupado == candidato)
                {
                    if (candidato == int.MaxValue)
                    {
                        return 0;
                    }
                    candidato++;
                }
                else if (ocupado > candidato)
                {
                    return candidato;
                }
            }
            return candidato;
        }

        // Busca un identificador positivo disponible.
        public static int Siguiente(List<Usuario> registros)
        {
            List<int> identificaciones = new List<int>();
            foreach (Usuario registro in registros)
            {
                identificaciones.Add(registro.id);
            }
            return Siguiente(identificaciones);
        }

        // Busca un identificador positivo disponible.
        public static int Siguiente(List<Empleado> registros)
        {
            List<int> identificaciones = new List<int>();
            foreach (Empleado registro in registros)
            {
                identificaciones.Add(registro.id);
            }
            return Siguiente(identificaciones);
        }

        // Busca un identificador positivo disponible.
        public static int Siguiente(List<TipoViatico> registros)
        {
            List<int> identificaciones = new List<int>();
            foreach (TipoViatico registro in registros)
            {
                identificaciones.Add(registro.id);
            }
            return Siguiente(identificaciones);
        }

        // Busca un identificador positivo disponible.
        public static int Siguiente(List<DetalleLiquidacion> registros)
        {
            List<int> identificaciones = new List<int>();
            foreach (DetalleLiquidacion registro in registros)
            {
                identificaciones.Add(registro.id);
            }
            return Siguiente(identificaciones);
        }

        // Busca un identificador positivo disponible.
        public static int Siguiente(List<RegistroBitacora> registros)
        {
            List<int> identificaciones = new List<int>();
            foreach (RegistroBitacora registro in registros)
            {
                identificaciones.Add(registro.id);
            }
            return Siguiente(identificaciones);
        }
    }
}
