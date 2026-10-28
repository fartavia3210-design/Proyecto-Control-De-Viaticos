# Contrato del proyecto Control de viáticos

Fecha: 21 de octubre de 2026. Responsable de esta etapa: Fabián.

Este documento describe la base compartida implementada por Fabián e integrada en `main`. Incluye los nombres reales del código, el funcionamiento implementado, las pruebas manuales realizadas y el trabajo pendiente de Mango y Duván. Es la referencia común para continuar el desarrollo sobre esa base.

## 1. Organización y almacenamiento

- Aplicación de escritorio en C#, WinForms y .NET Framework 4.7.2, sin paquetes NuGet adicionales.
- `ControlViaticos/Objetos`: clases con propiedades `get` y `set` que representan los registros.
- `ControlViaticos/Logica`: listas compartidas, operaciones y validaciones.
- `ControlViaticos/Presentacion`: formularios, controles, mensajes y llamadas a la lógica.
- `ControlViaticos/Program.cs`: decide si debe abrir el registro inicial antes del login.
- Los datos viven en memoria. Se conservan al navegar o cambiar de sesión durante una misma ejecución; se pierden al cerrar el programa.

`Estructuras.cs` contiene únicamente las seis listas siguientes. Son campos `internal static`: se comparten entre las clases del mismo proyecto, sin crear listas independientes por formulario.

| Campo | Tipo |
|---|---|
| `listaUsuarios` | `List<Usuario>` |
| `listaEmpleados` | `List<Empleado>` |
| `listaTiposViatico` | `List<TipoViatico>` |
| `listaLiquidaciones` | `List<Liquidacion>` |
| `listaDetalles` | `List<DetalleLiquidacion>` |
| `listaBitacora` | `List<RegistroBitacora>` |

Los formularios deben llamar a las clases de lógica para realizar operaciones. No deben reiniciar estas listas al abrirse. Los métodos `Obtener...` devuelven una copia de la lista, pero sus objetos siguen siendo compartidos: para modificar un usuario, se debe construir un objeto nuevo y pasarlo a `ModificarUsuario`, como hace `frmUsuarios`.

## 2. Objetos y nombres de propiedades

Todos se encuentran en el namespace `ControlViaticos.Objetos`. Los nombres distinguen mayúsculas y minúsculas. No se escribieron constructores explícitos.

| Clase | Propiedades |
|---|---|
| `Usuario` | `int id`, `string nombreCompleto`, `string nombreUsuario`, `string clave`, `string tipo`, `bool activo` |
| `Empleado` | `int id`, `string cedula`, `string nombre`, `string apellido1`, `string apellido2`, `string telefono`, `string direccion`, `bool activo` |
| `TipoViatico` | `int id`, `string nombre`, `int monto`, `bool activo` |
| `RegistroBitacora` | `int id`, `int idUsuario`, `string nombreUsuario`, `DateTime fechaHora`, `string seccion`, `string movimiento` |
| `Liquidacion` | `int id`, `int idEmpleado`, `DateTime inicio`, `DateTime fin`, `string justificacion` |
| `DetalleLiquidacion` | `int idLiquidacion`, `int idViatico`, `int monto` |

`Liquidacion` y `DetalleLiquidacion` ya existían en la entrega de Duván. Fabián agregó los otros cuatro objetos y las cuatro listas correspondientes.

Un usuario representa una cuenta de acceso; un empleado representa a quien recibe viáticos. Administrador no es otra clase: es un `Usuario` con `tipo = "Administrador"`. El otro tipo aceptado es `"Normal"`. `activo` vale `true` o `false`.

Los montos actuales se representan como enteros en colones, siguiendo la estructura existente de los detalles. Teléfono y cédula son texto.

### Identificadores y relaciones

- En Usuarios, `id` es la identificación entera positiva ingresada en la pantalla; debe ser única.
- El nombre de usuario también debe ser único. Se eliminan espacios de sus extremos y se compara sin distinguir mayúsculas de minúsculas.
- El vínculo previsto es `Liquidacion.idEmpleado` con `Empleado.id`; `DetalleLiquidacion.idLiquidacion` con `Liquidacion.id`; `DetalleLiquidacion.idViatico` con `TipoViatico.id`.
- La conexión real con los catálogos aún está pendiente: `frmLiquidaciones` todavía utiliza posiciones de los combos (`SelectedIndex`) como identificadores. No se debe copiar ese comportamiento a los nuevos módulos.
- `RegistroBitacora.idUsuario` identifica al autor de la operación. Su `nombreUsuario` conserva el nombre que tenía al registrar el movimiento.
- La carga inicial calcula identificadores nuevos a partir del mayor existente. Actualmente no existe una política global para impedir reutilizar identificadores borrados; debe resolverse antes de depender de ellos para más relaciones históricas.

## 3. Primer administrador, login y sesión

Flujo implementado:

```text
Programa sin usuarios
    -> frmRegistroAdministrador
    -> creación explícita de administrador activo
    -> frmLogin
    -> credenciales válidas
    -> frmPrincipal
    -> Salir o cerrar el menú con X
    -> cerrar sesión y regresar al login
```

Si se cancela o cierra el registro inicial, el programa termina sin abrir el login. La primera cuenta no tiene credenciales hardcodeadas: se ingresan identificación, nombre completo, usuario y clave. Este registro no ejecuta la carga de ejemplos. Al iniciar una ejecución nueva, las listas vuelven a estar vacías y se solicita otra vez el primer administrador.

Esta forma de crear la primera cuenta es una decisión de implementación del equipo: el enunciado exige login, roles y carga manual, pero no especifica el mecanismo de creación de la primera cuenta.

### Métodos de acceso

| Clase y método | Resultado |
|---|---|
| `LogicaAcceso.CrearPrimerAdministrador(Usuario administrador)` | `bool`: acepta datos válidos de un administrador activo únicamente si la lista de usuarios está vacía. |
| `LogicaAcceso.ValidarAcceso(string nombreUsuario, string clave)` | `Usuario` autorizado o `null` si los datos son incorrectos o la cuenta está inactiva. |
| `SesionActual.IniciarSesion(string nombreUsuario, string clave)` | `bool`: valida las credenciales y guarda el usuario autorizado. Un intento rechazado limpia la sesión anterior. |
| `SesionActual.HaySesionActiva()` | `bool`: comprueba que el usuario siga registrado y activo. |
| `SesionActual.EsAdministrador()` | `bool`: exige sesión válida y tipo Administrador. |
| `SesionActual.CerrarSesion()` | `void`: asigna `null` a `usuarioActual`. |

`SesionActual.usuarioActual` es un campo `internal static Usuario`. Al integrar operaciones se debe utilizar este autor, no una cuenta elegida manualmente en un combo.

La contraseña se compara exactamente: distingue mayúsculas y minúsculas y no elimina espacios. El campo visual oculta sus caracteres. Las claves no se incluyen en las columnas de la tabla de Usuarios.

### Permisos

- Administrador: acceso a todas las opciones actuales del menú.
- Normal: puede usar empleados, tipos de viático, liquidaciones, bitácora y reportes; no administra usuarios ni ejecuta la carga inicial.
- `frmPrincipal` deshabilita Usuarios y Carga inicial para cuentas normales, comprueba la sesión al abrir/navegar y vuelve a validar el permiso en las opciones restringidas.
- `frmUsuarios` y `frmCargaInicial` también validan el permiso al abrir y antes de sus botones.
- La lógica de escritura de Usuarios y la carga inicial comprueban el permiso aunque se llamen sin usar esos botones.
- La carga inicial exige que el administrador recibido coincida con la sesión actual.

## 4. Usuarios

`LogicaUsuarios` contiene:

| Método | Resultado |
|---|---|
| `AgregarUsuario(Usuario miUsuario)` | `bool`: agrega si la sesión es administradora, los campos son válidos y no hay duplicados. |
| `BuscarUsuario(int id)` | `Usuario` encontrado o `null`. También se utiliza internamente para validar sesión y autor de bitácora. |
| `ModificarUsuario(Usuario miUsuario)` | `bool`: modifica un registro existente si supera las validaciones y protecciones. |
| `EliminarUsuario(int id)` | `bool`: elimina si existe y no está protegido. |
| `ObtenerUsuarios()` | `List<Usuario>` para listados y selectores. |

El campo `ultimoError` explica el rechazo de una operación de escritura. `AgregarPrimerAdministrador` es la excepción interna para el registro inicial; los demás formularios deben utilizar `AgregarUsuario` después del login.

`frmUsuarios` carga la tabla al abrir. Buscar valida la identificación y llena los campos; Guardar agrega cuando el ID no existe y modifica cuando ya existe. Eliminar solicita confirmación. La tabla se actualiza y los campos se limpian después de guardar o eliminar correctamente; ante un error se conservan los datos para corregirlos.

Protecciones implementadas:

- Identificación positiva; nombre, usuario y clave obligatorios; tipo y estado seleccionados en la pantalla.
- Rechazo de nombres de usuario duplicados, también al modificar.
- No eliminar ni desactivar la cuenta que está en uso, ni quitarle su rol administrador durante esa sesión.
- Mantener al menos un administrador activo.
- No eliminar usuarios que sean autores de registros en bitácora. Se puede modificar su estado si las otras protecciones lo permiten; el rechazo no los desactiva automáticamente.

## 5. Bitácora y consulta de operaciones

La bitácora es una lista separada. No se almacena el historial dentro de los empleados ni de los tipos de viático.

| Método de `LogicaBitacora` | Resultado |
|---|---|
| `RegistrarOperacion(Usuario usuario, string seccion, string movimiento)` | `bool`: registra al autor existente y activo, fecha/hora actual y textos no vacíos; asigna el siguiente ID disponible según el mayor existente. |
| `ObtenerBitacora()` | `List<RegistroBitacora>` con los registros existentes. |
| `ConsultarOperaciones(int idUsuario, DateTime desde, DateTime hasta)` | `List<RegistroBitacora>` filtrada por autor y fechas. Incluye todas las horas de ambos días. Un ID inválido o rango invertido devuelve una lista vacía. |

Actualmente `frmUsuarios` llama a RegistrarOperacion después de crear, modificar o eliminar correctamente. Las operaciones rechazadas o canceladas no generan movimientos exitosos. No se registra la clave. La creación inicial del administrador no tiene una entrada automática de bitácora.

`frmBitacora` muestra Usuario, Fecha y hora, Sección y Movimiento, sin edición directa. El reporte `frmReporteOperaciones` ya está conectado por Fabián: permite seleccionar usuarios activos o inactivos, valida el rango y limpia resultados anteriores cuando los filtros son inválidos. El nombre mostrado en cada movimiento es el histórico, aunque la cuenta después cambie de nombre.

### Ejemplo para Mango y Duván

Después de comprobar que la operación de su módulo terminó correctamente:

```csharp
LogicaBitacora bitacora = new LogicaBitacora();
bitacora.RegistrarOperacion(
    SesionActual.usuarioActual,
    "Empleados",
    "Creó empleado con identificación " + empleado.id
);
```

Cambiar la sección y el movimiento según corresponda. Duván utilizará, por ejemplo, `"Liquidaciones"` y el consecutivo del registro. No llamar a este método antes de validar, al cancelar una confirmación ni después de una operación rechazada. La llamada pertenece a un único punto del flujo para evitar registros dobles.

## 6. Carga inicial

`LogicaCargaInicial.EjecutarCargaInicial(Usuario administrador)` devuelve un `int`: `-1` si no hay permiso, `0` si no agregó nada o la cantidad de registros agregados.

La primera carga completa agrega cinco registros de ejemplo:

| Lista | Datos |
|---|---|
| Usuarios | Cuenta Normal y Activa: `usuarioEjemplo`, clave de demostración `ejemplo123`. |
| Empleados | Empleado ficticio con cédula `000000001`, nombre Empleado De Ejemplo, teléfono `00000000` y dirección de ejemplo. |
| Tipos de viático | Desayuno: 3000; Almuerzo: 5000; Cena: 5000. Los tres activos. |

No reemplaza al administrador inicial. Identifica ejemplos existentes por nombre de usuario, cédula y nombre de viático, respectivamente. Conserva sus montos, claves, estados y demás datos; agrega únicamente los ejemplos que faltan. Registra una entrada de bitácora si agregó datos.

`frmCargaInicial` ejecuta esta lógica únicamente desde el botón. Abrir el programa o la pantalla no ejecuta la carga. El formulario muestra el resultado y permite repetirla sin duplicar. La cantidad puede ser menor que cinco si ya existen algunos ejemplos.

## 7. Liquidaciones y contratos pendientes de completar

Fabián movió la lógica existente de `Estructuras.cs` a `LogicaLiquidaciones.cs` y cambió la referencia del formulario. Las dos listas existentes permanecieron en Estructuras. No se completó la funcionalidad pendiente de Duván mediante ese traslado.

Los métodos existentes son `AgregarLiquidacion(Liquidacion)`, `buscarLiquidacion(int)`, `modificarLiquidacion(Liquidacion)`, `eliminarLiquidacion(int)`, `ObtenerLiquidaciones()`, `AgregarDetalle(DetalleLiquidacion)`, `ObtenerDetalles(int idLiquidacion)`, `eliminarDetalle(int idLiquidacion, int idViatico)`, `calcularTotal(int idLiquidacion)` y `cumpleHorario(string viatico, DateTime salida, DateTime llegada)`.

Estado y decisiones pendientes:

- Conectar empleados y tipos reales mediante sus IDs; actualmente hay opciones y precios de prueba en el formulario, incluidos almuerzo 7000 y cena 10000.
- Conservar el monto aplicado en cada detalle aunque cambie el precio del catálogo. El campo `DetalleLiquidacion.monto` ya existe para guardar esa copia.
- Definir cómo identificar cada detalle si se permiten varias filas del mismo tipo; la eliminación actual busca por liquidación y tipo, y elimina la primera coincidencia.
- Definir el flujo para completar cabecera y detalles sin dejar una liquidación final vacía. El formulario actual exige guardar la cabecera antes de agregar detalles.
- Resolver fecha de consumo por detalle, viajes de varios días y fecha utilizada por los dos reportes de consumo. El objeto DetalleLiquidacion actual no tiene fecha de consumo.
- Acordar la eliminación o desactivación de empleados y tipos ya utilizados para conservar los reportes; esa protección todavía no está implementada en sus CRUD.
- Confirmar la regla de Cena: el código actual y el plan revisado usan salida a las 16:00 o después; la tabla del enunciado escrito indica salida a las 16:00 o antes. No se consideran verificados los límites ni los viajes de varios días en esta etapa.

Las correcciones auditadas y comunicadas a Duván son evitar liquidaciones completadas sin detalles y recargar el listado al volver al módulo. Se confirmó el listado vacío, no pérdida de datos. Mango tiene los ajustes visuales de campos cortados, distribución de Liquidaciones y presentación de tablas, coordinados con Duván cuando afectan datos.

## 8. Cambios de Fabián y estado actual

Durante esta etapa se crearon los cuatro objetos compartidos que faltaban, se agregaron sus listas y se separó la lógica de liquidaciones. Después se implementaron LogicaUsuarios, LogicaAcceso, LogicaBitacora, LogicaCargaInicial y SesionActual. Se registraron los archivos nuevos en el proyecto para que compilen.

Se creó el formulario independiente de registro inicial y se retiró el GroupBox experimental del login. Se conectaron login, sesión y permisos; CRUD de usuarios; bitácora; botón de carga inicial; y reporte de operaciones por usuario y fechas. También se incorporaron los ajustes visuales realizados por Fabián en Usuarios.

Referencias del historial:

| Commit | Trabajo |
|---|---|
| `9ea8ff0` | Lógica de usuarios. |
| `60c3101` | Lógica de acceso y bitácora. |
| `8569174` | Lógica de carga inicial y sesión actual. |
| `367f5ac` | Registro independiente del primer administrador, login y permisos. |
| `09be591` | CRUD conectado, bitácora, carga inicial, reporte de operaciones y ajustes de Usuarios. |

Estos commits identifican los cambios de Fabián incluidos en la base compartida de `main`. El contrato acompaña esa base y establece los nombres y reglas que deben conservar las siguientes entregas.

### Pruebas realizadas

La compilación de las entregas terminó sin errores ni advertencias. Además de las comprobaciones de lógica en memoria, Fabián realizó y confirmó manualmente el siguiente recorrido desde las pantallas el 21 de octubre de 2026:

- Registro inicial: rechazo de identificación inválida y campos incompletos; creación correcta y paso al login.
- Login: rechazo de campos vacíos, contraseña incorrecta y usuario inactivo; acceso con credenciales correctas.
- Roles: opciones restringidas habilitadas para administrador y deshabilitadas para Normal; los demás módulos abren.
- Usuarios: creación, consulta, modificación sin duplicar, rechazo de duplicados y entradas inválidas, eliminación confirmada y cancelación sin borrar.
- Protecciones: impedir eliminar o desactivar la cuenta en uso y quitarle su rol; conservar sus datos después del rechazo; impedir eliminar usuarios con historial.
- Bitácora: movimientos exitosos con autor correcto, conservación de autores anteriores y registro de carga inicial.
- Carga: ejecución con cantidad agregada, repetición sin duplicados y una sola cuenta de ejemplo.
- Sesión: regreso al login mediante Salir y X, contraseña vacía y cambio a otro usuario.
- Reporte: selección obligatoria, rechazo de rango invertido, filtro por autor y fecha y aviso sin resultados para fechas anteriores.
- Navegación: conservación de los datos durante la misma ejecución.

Los límites de todo el último día se comprobaron también en memoria, incluyendo un registro en la última fracción de segundo. La revisión visual todavía identifica contenidos cortados en algunas tablas, incluidos en el trabajo de presentación pendiente.

### Trabajo que sigue para el equipo

- Mango: implementar los CRUD de Empleados y Tipos de viático sobre estas listas; completar los ajustes visuales y conectar las pantallas de consumo con las consultas de Duván. El reporte de operaciones ya está conectado en la base compartida.
- Duván: integrar los catálogos reales, terminar validaciones y coherencia de liquidaciones, corregir sus hallazgos y entregar las consultas de consumo por fechas y empleado.
- Ambos: conectar sus operaciones exitosas con la bitácora central.
- Fabián: volver a auditar las entregas de los compañeros y coordinar su integración. Después, comprobar la rúbrica y preparar el informe y la exposición con el equipo.

El estado documentado corresponde a la base compartida de `main`. Los avances que existan únicamente en ramas separadas de Mango o Duván no se consideran entregados hasta incorporarlos y comprobarlos. La parte implementada y probada de Fabián no significa que el software completo esté terminado.

## 9. Guía específica de implementación para Mango y Duván

Esta sección indica el trabajo que sigue sobre la base de Fabián disponible en `main`. Las firmas y ampliaciones señaladas como nuevas son instrucciones para la próxima implementación: todavía están pendientes. No significan que esos cambios ya estén terminados o probados. Si un compañero ya tiene parte del trabajo en otra rama, debe adaptarlo a estos nombres y reutilizarlo.

### 9.1. Base que ambos deben conservar

1. Partir de la base de Fabián integrada en `main` y conservar los cambios propios al actualizar la rama de trabajo.
2. Reutilizar los seis objetos y las seis listas de los apartados 1 y 2. No crear otro objeto Administrador, otra sesión, otra bitácora ni listas globales duplicadas.
3. Mantener `Estructuras.cs` como almacenamiento compartido. Los métodos de cada módulo van en su clase de lógica y los eventos de botones en su formulario.
4. Utilizar propiedades, clases, listas y recorridos que el equipo pueda explicar con lo visto en clase. No agregar bases de datos, paquetes NuGet ni una arquitectura adicional.
5. Usar los identificadores guardados en los objetos, nunca `SelectedIndex` como identificador. Una posición del combo no representa el ID de un registro.
6. No reiniciar listas al entrar a una pantalla. Cargar las tablas desde la lógica en el evento de apertura y actualizarlas después de cada operación correcta.
7. Antes de operar desde una pantalla, comprobar `SesionActual.HaySesionActiva()`. Empleados, Tipos y Liquidaciones están disponibles tanto para Normal como para Administrador; conservar las restricciones actuales de Usuarios y Carga inicial.
8. Registrar una sola entrada de bitácora por operación completada, con `SesionActual.usuarioActual`. No registrar búsquedas, consultas, rechazos ni cancelaciones como modificaciones exitosas.
9. Registrar los archivos `.cs` nuevos en `ControlViaticos.csproj`, siguiendo el formato existente; este proyecto enumera explícitamente sus archivos de compilación.

### 9.2. Mango: CRUD de Empleados

**Archivos:** crear `Logica/LogicaEmpleados.cs` y completar `Presentacion/frmEmpleados.cs`. Reutilizar `Objetos/Empleado.cs` y `Estructuras.listaEmpleados`; no hacen falta un objeto Empleado nuevo ni una lista compartida nueva.

Crear `internal class LogicaEmpleados` con estos miembros públicos de instancia, siguiendo la forma de `LogicaUsuarios`:

```csharp
public string ultimoError = "";
public bool AgregarEmpleado(Empleado empleado)
public Empleado BuscarEmpleado(int id)
public bool ModificarEmpleado(Empleado empleado)
public bool EliminarEmpleado(int id)
public List<Empleado> ObtenerEmpleados()
```

Los métodos de escritura devuelven `false` y explican el rechazo en `ultimoError`; Buscar devuelve `null` si no existe. Obtener devuelve una copia de la lista para mostrarla. Los métodos de escritura también deben rechazar la operación si no hay sesión activa.

- Validar ID positivo y único; cédula obligatoria y única, ignorando espacios de los extremos; nombre, ambos apellidos, teléfono y dirección obligatorios. Conservar cédula y teléfono como texto. No inventar restricciones de longitud o formato que el enunciado no especifique.
- Al modificar, excluir el propio ID al comprobar cédulas duplicadas. Construir un objeto nuevo desde los controles y validar antes de cambiar el objeto guardado.
- Si alguna `Liquidacion.idEmpleado` coincide con el empleado, impedir su eliminación y explicar que puede marcarse Inactivo. Permitir cambiar su estado sin borrar liquidaciones anteriores. Si no tiene relaciones, permitir eliminarlo con confirmación.
- En Buscar, validar identificación y llenar todos los controles. En Guardar, agregar si el ID no existe y modificar si existe. En Eliminar, confirmar antes de llamar a la lógica.
- Mostrar en la tabla los campos del empleado, encabezados comprensibles y estado Activo/Inactivo. Recargar y limpiar campos tras una operación correcta.
- Después de guardar o eliminar correctamente, registrar la sección `"Empleados"` y un movimiento con la acción y el ID. Usar el mismo punto de registro que `frmUsuarios`, sin duplicarlo dentro de la lógica.

**Entrega comprobable:** crear, buscar, modificar y borrar; rechazar ID/cédula duplicados; conservar datos tras un rechazo; impedir borrar empleados relacionados; conservar el listado al navegar; comprobar autor y acción en Bitácora.

### 9.3. Mango: CRUD de Tipos de viático

**Archivos:** crear `Logica/LogicaTiposViatico.cs` y completar `Presentacion/frmTiposViatico.cs`. Reutilizar `TipoViatico` y `Estructuras.listaTiposViatico`; no agregar objetos ni listas compartidas para este CRUD.

Crear `internal class LogicaTiposViatico` con miembros públicos de instancia:

```csharp
public string ultimoError = "";
public bool AgregarTipoViatico(TipoViatico tipoViatico)
public TipoViatico BuscarTipoViatico(int id)
public bool ModificarTipoViatico(TipoViatico tipoViatico)
public bool EliminarTipoViatico(int id)
public List<TipoViatico> ObtenerTiposViatico()
```

Aplicar la misma convención de resultados, errores y sesión que en Empleados.

- Validar ID positivo y único, nombre obligatorio y único sin distinguir mayúsculas y espacios de los extremos, monto entero positivo y estado seleccionado.
- Buscar, Guardar, Eliminar, tabla y limpieza deben funcionar como en Empleados.
- Impedir eliminar un tipo si algún `DetalleLiquidacion.idViatico` lo referencia. Ofrecer marcarlo Inactivo mediante edición; no desactivarlo automáticamente al rechazar la eliminación.
- Modificar el monto del catálogo debe afectar asignaciones nuevas. No recorrer detalles históricos para reemplazar sus montos.
- Mostrar Identificación, Tipo de viático, Monto y Estado. Registrar escrituras correctas en la sección `"Tipos de viático"` con acción e ID.
- Mostrar los ejemplos que agrega la carga inicial sobre esta misma lista. No mantener otro catálogo de desayuno/almuerzo/cena en el formulario.

**Entrega comprobable:** CRUD completo, duplicados y montos inválidos rechazados, protección de tipos relacionados, estado respetado y montos anteriores conservados en detalles ya guardados.

### 9.4. Duván: completar Liquidaciones sobre los catálogos reales

**Archivos:** completar `Logica/LogicaLiquidaciones.cs` y `Presentacion/frmLiquidaciones.cs`. Reutilizar `Liquidacion`, `DetalleLiquidacion`, `listaLiquidaciones` y `listaDetalles`. No crear una segunda lógica CRUD ni listas globales paralelas.

1. Cargar empleados desde `LogicaEmpleados.ObtenerEmpleados()` y tipos desde `LogicaTiposViatico.ObtenerTiposViatico()`, cuando Mango entregue esos métodos. Para nuevas asignaciones, ofrecer solo registros activos; volver a comprobar existencia y estado en la lógica al guardar.
2. Eliminar empleados, tipos y precios de prueba del formulario. Obtener el ID del objeto seleccionado y tomar el monto de `TipoViatico.monto`. Guardar ese monto en el detalle; al consultar una liquidación, sumar los montos de sus detalles guardados.
3. Validar consecutivo positivo y único al crear, empleado existente, justificación no vacía e inicio anterior a fin. Al modificar, conservar el consecutivo y validar todos los datos antes de escribir.
4. Corregir el flujo auditado de liquidaciones sin detalles: preparar cabecera y detalles antes de guardar definitivamente. Puede usarse una lista temporal privada del formulario; no agregar otra lista a Estructuras. Cancelar o cerrar sin guardar no debe insertar una cabecera vacía en la lista compartida.
5. Al guardar definitivamente, exigir al menos un detalle válido. En una modificación, impedir que el estado final guardado quede sin detalles. El botón Quitar puede dejar vacía la preparación temporal, pero Guardar debe rechazarla sin alterar lo que ya estaba almacenado.
6. Validar primero toda la cabecera y todos los detalles; después aplicar el cambio completo. No insertar parcialmente ni borrar los detalles anteriores antes de terminar las validaciones.
7. Cargar `ObtenerLiquidaciones()` al abrir el formulario y después de guardar o eliminar. Buscar debe recuperar cabecera, detalles y total. Esta corrección resuelve el listado vacío al volver; no debe presentarse como reparación de una pérdida de datos que no se confirmó.
8. Conservar la confirmación al eliminar y borrar cabecera junto con sus detalles. Una cancelación no cambia ninguna lista.
9. Registrar una entrada en `"Liquidaciones"` después de crear, modificar o eliminar la liquidación completa, con su consecutivo. Los cambios de detalles preparados se registran al guardar el conjunto, evitando registrar como definitivos cambios que después se cancelan.

**Ajuste de firmas:** conservar los nombres existentes para evitar romper llamadas. Convertir `AgregarLiquidacion` y `modificarLiquidacion` en operaciones que reciban también `List<DetalleLiquidacion> detalles` y devuelvan `bool`; agregar `public string ultimoError = "";`. Actualizar conjuntamente las llamadas del formulario. Los métodos auxiliares de detalles no deben permitir guardar un conjunto inválido por una vía alternativa.

### 9.5. Duván: identificar detalles y preparar los consumos por día

La estructura actual no distingue dos filas del mismo viático ni guarda su día de consumo. Para soportar el viaje de varios días y los reportes, **ampliar el objeto existente** `DetalleLiquidacion` con estas propiedades nuevas, además de las actuales:

```csharp
public int id { get; set; }
public DateTime fechaConsumo { get; set; }
```

- `id` identifica una fila de detalle y debe ser único entre los detalles guardados. Al guardar detalles nuevos, asignarlo después de validar. No usar el tipo de viático como identidad de la fila.
- `fechaConsumo` guarda el día al que corresponde el concepto, usando `.Date`. Incorporar un selector de fecha en la preparación del detalle y cargarlo al consultar.
- Validar que la fecha esté comprendida entre `Liquidacion.inicio.Date` y `Liquidacion.fin.Date`. Los dos reportes utilizarán esta fecha, no la fecha de creación del registro ni la fecha actual.
- Cambiar la eliminación de una fila para identificarla por liquidación e ID de detalle; actualizar la selección del formulario. Poder quitar un desayuno sin borrar otro desayuno de distinto día.
- No crear listas globales adicionales para estas propiedades. Esta ampliación pertenece a Duván; Mango solo deberá mostrar los datos resultantes en sus reportes.

**Reglas que aún necesitan definición antes de implementarlas:** la contradicción de Cena y las ventanas de alimentación de los días intermedios siguen abiertas en el apartado 7. Duván debe presentar a Fabián un ejemplo de dos días con horas, conceptos y resultado esperado; Fabián define el criterio común o consulta al profesor. No asumir que la validación actual del primer día resuelve todos los días del viaje. Hospedaje u otros conceptos no deben recibir por error las restricciones de una comida.

**Entrega comprobable de Liquidaciones:** varios detalles, dos filas del mismo tipo en días distintos, rechazo de guardado vacío, cancelación sin cambios parciales, total correcto, precio histórico conservado, estados respetados y recuperación al volver al módulo. Probar límites exactos y un minuto fuera cuando las reglas pendientes estén resueltas.

### 9.6. Duván: entregar consultas de consumo; Mango: conectarlas

**Duván crea** `Objetos/ConsumoReporte.cs`, un objeto únicamente para presentar resultados. No representa un registro nuevo ni requiere una lista en Estructuras. Usar propiedades públicas con `get` y `set`:

| Propiedad | Tipo | Origen |
|---|---|---|
| `idLiquidacion` | `int` | Cabecera relacionada. |
| `idEmpleado` | `int` | Empleado de la cabecera. |
| `nombreEmpleado` | `string` | Nombre y apellidos del empleado. |
| `fechaConsumo` | `DateTime` | Fecha del detalle. |
| `nombreViatico` | `string` | Nombre del tipo relacionado. |
| `monto` | `int` | Monto guardado en el detalle. |

**Duván crea** `Logica/LogicaReportesConsumo.cs`, clase `internal`, con estos métodos públicos de instancia:

```csharp
public List<ConsumoReporte> ConsultarConsumosPorFechas(DateTime desde, DateTime hasta)
public List<ConsumoReporte> ConsultarConsumosPorEmpleado(int idEmpleado)
```

Recorrer las listas existentes y relacionar los IDs para construir una lista local de resultados. Por fechas, incluir ambos días completos comparando `.Date`; un rango invertido devuelve una lista vacía. Por empleado, filtrar por ID; uno inexistente devuelve una lista vacía. Mostrar una fila por detalle, sin multiplicar filas al relacionar datos. Incluir consumos anteriores de empleados o tipos actualmente inactivos. El monto sale del detalle, nunca del precio actual del catálogo. Entregar estos métodos y un ejemplo de resultado a Mango.

**Mango completa** `frmReporteConsumosFechas.cs` y `frmReporteConsumosEmpleado.cs`:

- Fechas: validar Desde menor o igual a Hasta, llamar a `ConsultarConsumosPorFechas` y mostrar Empleado, Fecha, Tipo de viático y Monto.
- Empleado: cargar el selector desde `ObtenerEmpleados()`, incluyendo inactivos para consultar historial; exigir selección, obtener el ID real y llamar a `ConsultarConsumosPorEmpleado`. Mostrar Fecha, Tipo de viático y Monto.
- Ambos: sumar `monto` de las filas devueltas, mostrar el total, limpiar resultados anteriores al rechazar filtros y mostrar tabla vacía y total cero cuando no haya resultados. No modificar registros ni generar entradas de bitácora por consultar.
- `frmReporteOperaciones` ya está conectado por Fabián. Mango conserva esa lógica y ajusta su presentación si hace falta; no recrea `ConsultarOperaciones` ni cambia su firma.

**Entrega comprobable:** dos empleados y varias fechas producen filtros distintos; el último día se incluye completo; un rango sin consumos muestra cero; un cambio posterior del precio del catálogo no cambia los totales históricos.

### 9.7. Mango: correcciones visuales confirmadas

- Ampliar los controles Inicio y Fin de Liquidaciones para mostrar fecha y hora completas; ampliar Tipo de usuario en Usuarios para mostrar Administrador completo.
- Reorganizar Liquidaciones para que la tabla inferior y los botones sean accesibles en el tamaño normal de la ventana, sin exigir maximizarla. Ajustar tamaño mínimo, distribución y anclajes según corresponda.
- Usar encabezados comprensibles, anchos legibles y estado escrito en las tablas; mostrar nombres de empleado y viático, conservando sus IDs para las relaciones internas.
- Coordinar cambios de `frmLiquidaciones.Designer.cs` con Duván, que modifica su comportamiento. El formato visual no debe reemplazar las relaciones por índices ni alterar los montos.
- Revisar las pantallas de registro inicial y login conservando el flujo y las validaciones de Fabián. Separar los ajustes visuales de cualquier cambio de acceso.

**Entrega comprobable:** valores completos y controles accesibles sin maximizar; nombres en lugar de IDs sin perder el vínculo al registro; pantallas que conservan sus funciones después de reorganizarlas.

### 9.8. Orden de entrega y resumen de archivos nuevos

1. Ambos compañeros actualizan sus ramas con la base de Fabián que ya está integrada en `main`.
2. Mango entrega primero Empleados y después Tipos de viático, usando las listas ya creadas.
3. Duván integra esos catálogos, termina el guardado completo de Liquidaciones y amplía los detalles. Resuelve con Fabián las reglas abiertas antes de dar por probados los horarios de varios días.
4. Duván entrega las consultas de consumo y su objeto de resultado; Mango conecta las dos pantallas de consumo.
5. Mango coordina los ajustes visuales con el dueño de cada formulario. Fabián audita las entregas y las correcciones anteriores.

| Responsable | Clases nuevas | Objetos existentes que amplía | Listas nuevas en Estructuras |
|---|---|---|---|
| Mango | `LogicaEmpleados`, `LogicaTiposViatico` | Ninguno para completar los dos CRUD. | Ninguna. |
| Duván | `LogicaReportesConsumo`, `ConsumoReporte` | `DetalleLiquidacion`: agregar `id` y `fechaConsumo`. | Ninguna. |

Cada compañero debe entregar los archivos registrados en el proyecto, compilación correcta y resultados de las pruebas de su apartado. Las listas temporales de preparación y de resultados son locales al trabajo que realizan; no sustituyen ni duplican el almacenamiento compartido.
