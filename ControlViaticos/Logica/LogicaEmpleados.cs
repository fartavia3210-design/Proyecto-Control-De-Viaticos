using System;
using System.Collections.Generic;
using ControlViaticos.Objetos;

namespace ControlViaticos.Logica
{
    internal class LogicaEmpleados
    {
        public string ultimoError = "";

        // Devuelve false si no hay sesión, los datos no son válidos o ya existe el empleado.
        public bool AgregarEmpleado(Empleado miEmpleado)
        {
            ultimoError = "";
            if (!SesionActual.HaySesionActiva())
            {
                ultimoError = "Debe iniciar sesión para agregar empleados.";
                return false;
            }

            if (!DatosValidos(miEmpleado))
            {
                return false;
            }

            foreach (Empleado empleado in Estructuras.listaEmpleados)
            {
                if (empleado.id == miEmpleado.id)
                {
                    ultimoError = "Ya existe un empleado con esa identificación.";
                    return false;
                }
                if (string.Equals(empleado.cedula.Trim(), miEmpleado.cedula.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    ultimoError = "Ya existe un empleado con esa cédula.";
                    return false;
                }
            }

            LimpiarEspacios(miEmpleado);
            Estructuras.listaEmpleados.Add(miEmpleado);
            return true;
        }

        public Empleado BuscarEmpleado(int id)
        {
            foreach (Empleado empleado in Estructuras.listaEmpleados)
            {
                if (empleado.id == id)
                {
                    return empleado;
                }
            }
            return null;
        }

        public bool ModificarEmpleado(Empleado miEmpleado)
        {
            ultimoError = "";
            if (!SesionActual.HaySesionActiva())
            {
                ultimoError = "Debe iniciar sesión para modificar empleados.";
                return false;
            }

            if (!DatosValidos(miEmpleado))
            {
                return false;
            }

            Empleado encontrado = BuscarEmpleado(miEmpleado.id);
            if (encontrado == null)
            {
                ultimoError = "No existe un empleado con esa identificación.";
                return false;
            }

            // Al comprobar la cédula se excluye el propio empleado.
            foreach (Empleado empleado in Estructuras.listaEmpleados)
            {
                if (empleado.id != miEmpleado.id && string.Equals(empleado.cedula.Trim(), miEmpleado.cedula.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    ultimoError = "Ya existe otro empleado con esa cédula.";
                    return false;
                }
            }

            LimpiarEspacios(miEmpleado);
            encontrado.cedula = miEmpleado.cedula;
            encontrado.nombre = miEmpleado.nombre;
            encontrado.apellido1 = miEmpleado.apellido1;
            encontrado.apellido2 = miEmpleado.apellido2;
            encontrado.telefono = miEmpleado.telefono;
            encontrado.direccion = miEmpleado.direccion;
            encontrado.activo = miEmpleado.activo;
            return true;
        }

        public bool EliminarEmpleado(int id)
        {
            ultimoError = "";
            if (!SesionActual.HaySesionActiva())
            {
                ultimoError = "Debe iniciar sesión para eliminar empleados.";
                return false;
            }

            Empleado encontrado = BuscarEmpleado(id);
            if (encontrado == null)
            {
                ultimoError = "No existe un empleado con esa identificación.";
                return false;
            }

            // Conserva el empleado si tiene liquidaciones registradas.
            foreach (Liquidacion liquidacion in Estructuras.listaLiquidaciones)
            {
                if (liquidacion.idEmpleado == id)
                {
                    ultimoError = "El empleado tiene liquidaciones. Puede marcarlo como inactivo.";
                    return false;
                }
            }

            Estructuras.listaEmpleados.Remove(encontrado);
            return true;
        }

        public List<Empleado> ObtenerEmpleados()
        {
            return new List<Empleado>(Estructuras.listaEmpleados);
        }

        private bool DatosValidos(Empleado miEmpleado)
        {
            if (miEmpleado == null || miEmpleado.id <= 0 || string.IsNullOrWhiteSpace(miEmpleado.cedula) || string.IsNullOrWhiteSpace(miEmpleado.nombre) || string.IsNullOrWhiteSpace(miEmpleado.apellido1) || string.IsNullOrWhiteSpace(miEmpleado.apellido2) || string.IsNullOrWhiteSpace(miEmpleado.telefono) || string.IsNullOrWhiteSpace(miEmpleado.direccion))
            {
                ultimoError = "Complete todos los datos y use una identificación positiva.";
                return false;
            }
            return true;
        }

        private void LimpiarEspacios(Empleado miEmpleado)
        {
            miEmpleado.cedula = miEmpleado.cedula.Trim();
            miEmpleado.nombre = miEmpleado.nombre.Trim();
            miEmpleado.apellido1 = miEmpleado.apellido1.Trim();
            miEmpleado.apellido2 = miEmpleado.apellido2.Trim();
            miEmpleado.telefono = miEmpleado.telefono.Trim();
            miEmpleado.direccion = miEmpleado.direccion.Trim();
        }
    }
}
