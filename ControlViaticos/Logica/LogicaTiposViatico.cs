using System;
using System.Collections.Generic;
using ControlViaticos.Objetos;

namespace ControlViaticos.Logica
{
    internal class LogicaTiposViatico
    {
        public string ultimoError = "";

        // Devuelve false si no hay sesión, los datos no son válidos o ya existe el tipo.
        public bool AgregarTipoViatico(TipoViatico miTipo)
        {
            ultimoError = "";
            if (!SesionActual.HaySesionActiva())
            {
                ultimoError = "Debe iniciar sesión para agregar tipos de viático.";
                return false;
            }

            if (!DatosValidos(miTipo))
            {
                return false;
            }

            foreach (TipoViatico tipo in Estructuras.listaTiposViatico)
            {
                if (tipo.id == miTipo.id)
                {
                    ultimoError = "Ya existe un tipo de viático con ese identificador.";
                    return false;
                }
                if (string.Equals(tipo.nombre.Trim(), miTipo.nombre.Trim(),
                    StringComparison.OrdinalIgnoreCase))
                {
                    ultimoError = "Ya existe un tipo de viático con ese nombre.";
                    return false;
                }
            }

            miTipo.nombre = miTipo.nombre.Trim();
            Estructuras.listaTiposViatico.Add(miTipo);
            return true;
        }

        public TipoViatico BuscarTipoViatico(int id)
        {
            foreach (TipoViatico tipo in Estructuras.listaTiposViatico)
            {
                if (tipo.id == id)
                {
                    return tipo;
                }
            }
            return null;
        }

        public bool ModificarTipoViatico(TipoViatico miTipo)
        {
            ultimoError = "";
            if (!SesionActual.HaySesionActiva())
            {
                ultimoError = "Debe iniciar sesión para modificar tipos de viático.";
                return false;
            }

            if (!DatosValidos(miTipo))
            {
                return false;
            }

            TipoViatico encontrado = BuscarTipoViatico(miTipo.id);
            if (encontrado == null)
            {
                ultimoError = "No existe un tipo de viático con ese identificador.";
                return false;
            }

            // Al comprobar el nombre se excluye el propio tipo.
            foreach (TipoViatico tipo in Estructuras.listaTiposViatico)
            {
                if (tipo.id != miTipo.id &&
                    string.Equals(tipo.nombre.Trim(), miTipo.nombre.Trim(),
                        StringComparison.OrdinalIgnoreCase))
                {
                    ultimoError = "Ya existe otro tipo de viático con ese nombre.";
                    return false;
                }
            }

            // Cambiar una comida por otra categoría reinterpretaría detalles anteriores.
            if (ReglasViaticos.Categoria(encontrado.nombre) != ReglasViaticos.Categoria(miTipo.nombre))
            {
                foreach (DetalleLiquidacion detalle in Estructuras.listaDetalles)
                {
                    if (detalle.idViatico == miTipo.id)
                    {
                        ultimoError = "El tipo tiene consumos. Para cambiar su categoría de horario, cree otro tipo de viático.";
                        return false;
                    }
                }
            }

            // Solo cambia el catálogo: los detalles ya guardados conservan su monto.
            encontrado.nombre = miTipo.nombre.Trim();
            encontrado.monto = miTipo.monto;
            encontrado.activo = miTipo.activo;
            return true;
        }

        public bool EliminarTipoViatico(int id)
        {
            ultimoError = "";
            if (!SesionActual.HaySesionActiva())
            {
                ultimoError = "Debe iniciar sesión para eliminar tipos de viático.";
                return false;
            }

            TipoViatico encontrado = BuscarTipoViatico(id);
            if (encontrado == null)
            {
                ultimoError = "No existe un tipo de viático con ese identificador.";
                return false;
            }

            // Conserva el tipo si algún detalle de liquidación lo usa.
            foreach (DetalleLiquidacion detalle in Estructuras.listaDetalles)
            {
                if (detalle.idViatico == id)
                {
                    ultimoError = "El tipo de viático se usa en liquidaciones. Puede marcarlo como inactivo.";
                    return false;
                }
            }

            Estructuras.listaTiposViatico.Remove(encontrado);
            return true;
        }

        public List<TipoViatico> ObtenerTiposViatico()
        {
            return new List<TipoViatico>(Estructuras.listaTiposViatico);
        }

        private bool DatosValidos(TipoViatico miTipo)
        {
            if (miTipo == null || miTipo.id <= 0 ||
                string.IsNullOrWhiteSpace(miTipo.nombre) || miTipo.monto <= 0)
            {
                ultimoError = "Complete el nombre, use un identificador positivo y un monto mayor que cero.";
                return false;
            }
            return true;
        }
    }
}
