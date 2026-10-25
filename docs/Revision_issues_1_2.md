# Revisión de los issues 1 y 2

Rama: `fix/issues-1-2-liquidaciones`. Cambios preparados para revisión, sin integrar en `main`.

## Cambios

- Cabecera y detalles se preparan localmente. Guardar valida el conjunto antes de reemplazar registros; un rechazo conserva la versión anterior.
- Buscar recupera copias de cabecera y detalles. Quitar afecta únicamente a la fila seleccionada en la preparación, incluso cuando hay tipos repetidos. Nuevo / Cancelar descarta la preparación; cambiar de módulo o cerrar no guarda cambios.
- El consecutivo queda bloqueado al buscar o agregar detalles. Para trabajar con otro consecutivo se usa Nuevo / Cancelar. Una liquidación existente debe buscarse antes de modificarla.
- Crear, modificar y eliminar registran una operación de bitácora desde el formulario. Preparar detalles, consultar y cancelar no generan movimientos exitosos.
- El listado se carga al abrir el formulario. Los combos usan objetos con IDs reales; las tablas conservan sus objetos enlazados y muestran nombres legibles. Se conserva el monto aplicado en los detalles históricos.
- Inicio y Fin tienen 240 píxeles de ancho base. La distribución utiliza filas adaptables y desplazamiento cuando falta espacio. Tipo de usuario se amplía a 170 píxeles sin cambiar la lógica ni las columnas protegidas de Usuarios.

## Cómo comprobarlo en Windows

1. Compilar `ControlViaticos/ControlViaticos.csproj` con Visual Studio o MSBuild (.NET Framework 4.7.2).
2. Ejecutar `powershell -File tests/Run-Regression.ps1`. El script compila un ejecutable temporal con las mismas fuentes y verifica lógica y apertura de formularios. El workflow `Validar liquidaciones` automatiza estos pasos en Windows.
3. Iniciar la aplicación, registrar un administrador y ejecutar Carga inicial para disponer de empleados y viáticos reales.
4. Preparar consecutivo, empleado, fechas válidas y justificación. Agregar un desayuno antes de guardar. Guardar y comprobar una única entrada de creación en Bitácora.
5. Buscar la liquidación y quitar el único detalle. Intentar guardar: debe rechazarse. Cancelar, buscar de nuevo y comprobar detalle y total originales. El rechazo y la cancelación no deben aparecer como éxitos en Bitácora.
6. Buscar, agregar otro detalle y guardar. Comprobar una única entrada de modificación. Eliminar con confirmación debe borrar cabecera y detalles y registrar una única eliminación; cancelar la confirmación no debe cambiarlos.
7. Guardar dos consecutivos, cambiar a Bitácora y regresar: ambos deben aparecer, sin duplicarse. Buscar debe recuperar empleado, fechas, detalles y total.
8. En tamaño normal, maximizado y restaurado, revisar Inicio y Fin, encabezados y nombres de ambas tablas. Si no hay espacio, desplazarse hasta los botones y la tabla inferior. Repetir a 100 %, 125 % y 150 % de escala de Windows.
9. Abrir Usuarios, seleccionar Administrador y comprobar que se lee completo; la clave debe seguir ausente de su tabla.

## Alcance y pendientes de revisión

El entorno Linux de edición no tiene compilador .NET/Mono ni escritorio Windows. La comprobación visual de texto, escala de pantalla, maximizar/restaurar y los diálogos de confirmación debe realizarse en Windows. Consultar el resultado del workflow para distinguir las pruebas automáticas ejecutadas de esta lista manual.

Se reutilizan exclusivamente los catálogos de `Estructuras`, consultados mediante adaptadores en `LogicaLiquidaciones`, porque `LogicaEmpleados` y `LogicaTiposViatico` aún no existen en esta base. Cuando se entreguen sus clases CRUD, esos adaptadores pueden delegar en sus métodos Obtener, sin cambiar el flujo del formulario.

Las reglas pendientes de Cena y viajes de varios días del contrato no se redefinen en esta corrección. Se conserva la regla horaria existente. La fecha e identidad adicional del detalle y los reportes de consumo pertenecen a las siguientes entregas del contrato; no se amplía ese alcance aquí.
