using System;
using System.Collections.Generic;

namespace ControlViaticos.Logica
{
    internal static class Identificadores
    {
        // Un ID identifica un registro vigente. Busca un hueco sin sumar a int.MaxValue.
        public static int Siguiente<T>(IEnumerable<T> registros, Func<T, int> obtenerId)
        {
            List<int> ocupados = new List<int>();
            foreach (T registro in registros) ocupados.Add(obtenerId(registro));
            ocupados.Sort();
            int candidato = 1;
            foreach (int ocupado in ocupados)
            {
                if (ocupado < candidato) continue;
                if (ocupado > candidato) return candidato;
                if (candidato == int.MaxValue) return 0;
                candidato++;
            }
            return candidato;
        }
    }
}
