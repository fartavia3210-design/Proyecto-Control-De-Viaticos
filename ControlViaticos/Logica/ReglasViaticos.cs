using System;

namespace ControlViaticos.Logica
{
    internal static class ReglasViaticos
    {
        // Identifica la categoría del tipo de viático.
        public static string Categoria(string nombre)
        {
            string texto = "";
            if (nombre != null)
            {
                texto = nombre.Trim();
            }
            foreach (string comida in new[] { "Desayuno", "Almuerzo", "Cena" })
            {
                if (string.Equals(texto, comida, StringComparison.OrdinalIgnoreCase))
                {
                    return comida;
                }
            }
            return "Otro";
        }

        // Comprueba la fecha y el horario permitido del consumo.
        public static bool Cumple(string nombre, DateTime salida, DateTime llegada, DateTime fecha)
        {
            if (llegada <= salida || fecha.Date < salida.Date || fecha.Date > llegada.Date)
            {
                return false;
            }
            int horaSalida;
            int horaLlegada;
            switch (Categoria(nombre))
            {
                case "Desayuno":
                    horaSalida = 7;
                    horaLlegada = 10;
                    break;
                case "Almuerzo":
                    horaSalida = 10;
                    horaLlegada = 14;
                    break;
                case "Cena":
                    horaSalida = 16;
                    horaLlegada = 20;
                    break;
                default: return true; // Hospedaje y otros conceptos no son comidas.
            }
            // En días intermedios no se aplica la salida ni la llegada del viaje.
            if (fecha.Date == salida.Date && salida.TimeOfDay > TimeSpan.FromHours(horaSalida))
            {
                return false;
            }
            if (fecha.Date == llegada.Date && llegada.TimeOfDay < TimeSpan.FromHours(horaLlegada))
            {
                return false;
            }
            return true;
        }
    }
}
