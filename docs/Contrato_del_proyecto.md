# Contrato vigente — Control de viáticos

Actualización de cierre: 28 de octubre de 2026, rama `release/cierre-final-viaticos`. Este documento describe el comportamiento implementado. El contrato base del 21 de octubre se conserva en [historial](historial/Contrato_base_21_octubre.md).

## Estructura y almacenamiento

Aplicación de escritorio C# / WinForms / .NET Framework 4.7.2. No requiere NuGet ni base de datos. `Objetos` contiene registros, `Logica` las operaciones y validaciones, y `Presentacion` los formularios. `Program` configura el primer administrador y abre el login.

`Estructuras` conserva seis listas compartidas durante la ejecución: usuarios, empleados, tipos de viático, liquidaciones, detalles y bitácora. No se reinician al navegar ni al cambiar de sesión. Se pierden al terminar el proceso.

| Objeto | Datos guardados |
|---|---|
| Usuario | ID, nombre completo, usuario, clave, tipo y activo. |
| Empleado | ID, cédula, nombre, dos apellidos, teléfono, dirección y activo. |
| TipoViatico | ID, nombre, monto entero en colones y activo. |
| Liquidacion | Consecutivo, ID de empleado, inicio, fin y justificación. |
| DetalleLiquidacion | ID de detalle, ID de liquidación, ID de tipo, monto aplicado y fecha de consumo. |
| RegistroBitacora | ID, ID y nombre de autor, fecha/hora, sección y movimiento. |
| ConsumoReporte | Resultado calculado: liquidación, empleado, nombre, fecha, viático y monto. No necesita una lista global. |

Empleado también expone `nombreCompleto` y `descripcion`, calculados a partir de sus datos para mostrar nombre, ID y estado en los selectores. No son registros nuevos.

## Acceso y permisos

La primera cuenta se crea explícitamente como Administrador activo antes del login; no ejecuta la carga de ejemplos. Cancelar ese registro termina el programa. El nombre de usuario se compara sin distinguir mayúsculas ni espacios externos; la clave se compara exactamente. Un usuario inactivo no entra.

Administrador utiliza todas las opciones. Normal utiliza empleados, tipos, liquidaciones, bitácora y reportes; no gestiona usuarios ni ejecuta carga inicial. El menú y las pantallas restringidas validan el permiso, y la lógica vuelve a comprobarlo antes de escribir. Empleados y Tipos comprueban sesión al abrir y antes de sus operaciones.

Salir o cerrar el menú regresa al login y limpia la clave. Si se cancela el descarte de una liquidación preparada, se conserva el menú y la sesión.

## CRUD y relaciones

Los identificadores ingresados son enteros positivos y únicos entre los registros vigentes de cada lista. Cédula y teléfono son texto para conservar ceros iniciales. Los campos del empleado son obligatorios. La cédula es única sin espacios externos; el nombre del tipo y el nombre de usuario son únicos sin distinguir mayúsculas y espacios externos. Los montos individuales son enteros positivos.

Guardar agrega cuando el ID no existe y modifica cuando ya existe, según el flujo de cada catálogo. Los formularios construyen objetos nuevos antes de validar para evitar alterar un registro al rechazar la operación. Eliminar solicita confirmación. Las tablas se actualizan después de una escritura correcta y vuelven a cargarse al abrir.

- Un empleado con liquidaciones no puede borrarse; puede marcarse inactivo.
- Un tipo utilizado en detalles no puede borrarse; puede marcarse inactivo.
- Un usuario autor de bitácora no puede borrarse; puede desactivarse si no vulnera las protecciones de administradores.
- No se elimina ni desactiva la cuenta de la sesión ni se le quita su rol administrador. Debe quedar al menos un administrador activo.
- Cambiar el precio de un tipo afecta asignaciones nuevas; los detalles guardados conservan su monto.
- Si un tipo ya tiene consumos, no puede cambiarse a otra categoría de horario por renombrado. Para eso se crea otro tipo. Se permite editar monto/estado y nombres dentro de la misma categoría de horario.

## Política de identificadores

Los IDs relacionan objetos; la posición de un combo nunca funciona como identidad. La política es unicidad entre registros vigentes, no una secuencia histórica irreversible. Un ID libre puede reutilizarse después de una eliminación permitida, porque no quedan relaciones que apunten a ese registro. La bitácora conserva el nombre del autor y la descripción de la operación tal como ocurrieron; no reconstruye el pasado desde un ID reutilizado.

`Identificadores.Siguiente` busca el primer entero positivo libre. Se usa para detalles, bitácora y ejemplos; no suma uno a un ID externo y por eso no desborda si existe `int.MaxValue`. Los IDs nuevos de un conjunto se preparan antes de escribir; si no quedan disponibles, la operación se rechaza. En una misma liquidación no se admiten IDs históricos repetidos o ajenos.

## Liquidaciones y borradores

Se prepara la cabecera junto con los detalles y se valida todo antes de aplicar el cambio. Se exige consecutivo positivo, empleado válido, justificación, fin posterior al inicio y al menos un detalle. Las nuevas asignaciones requieren empleado y tipos activos. Al editar se pueden conservar los registros inactivos ya relacionados.

Conservar historial no permite nuevos pagos: si el empleado está inactivo, no se agregan detalles ni se cambian las fechas de sus consumos. Se puede corregir la justificación o retirar un detalle, siempre conservando al menos uno. Un tipo inactivo solo se conserva con su beneficiario, fecha y monto históricos; no se traslada a otra persona ni a otro día. La lógica comprueba estas restricciones aunque se invoque sin usar el formulario.

Cada detalle guarda su fecha, comprendida dentro del viaje, y una copia del monto aplicado. Buscar devuelve copias de cabecera y detalles; editar la preparación no modifica lo guardado. Eliminar una liquidación elimina sus detalles en la misma operación. Quitar una fila de la preparación identifica esa fila, aunque otro día use el mismo tipo.

Al cambiar de módulo, cancelar, buscar otro conjunto o cerrar una preparación modificada se confirma su descarte. Rechazar conserva los controles y detalles; aceptar no guarda datos parciales. La navegación cierra y libera el formulario anterior solo cuando su cierre se acepta.

El total individual se limita a `int.MaxValue`, según la validación existente. Los reportes y la preparación acumulan en `long`, por lo que varias liquidaciones válidas no producen un total negativo.

## Horarios por fecha de consumo

`ReglasViaticos` es la única definición de estas reglas. Los nombres Desayuno, Almuerzo y Cena se reconocen sin distinguir mayúsculas ni espacios externos. Hospedaje y otros nombres no reciben restricciones de comida. Toda fecha debe pertenecer al viaje.

| Comida | Salida del primer día | Llegada del último día |
|---|---|---|
| Desayuno | ≤ 07:00 | ≥ 10:00 |
| Almuerzo | ≤ 10:00 | ≥ 14:00 |
| Cena | ≤ 16:00 | ≥ 20:00 |

En un viaje de un día se comprueban ambas condiciones. En varios días, la salida solo limita el primero y la llegada solo limita el último. Los días intermedios permiten las tres comidas. Los límites incluyen la hora exacta.

Inicio y Fin se muestran con precisión de minutos. El formulario elimina los segundos ocultos del selector antes de validar o guardar, de modo que una salida mostrada como 07:00 se evalúa como 07:00:00. La misma conversión se usa al agregar detalles y al guardar el conjunto.

Ejemplo: salida el 25/10 a las 08:00 y regreso el 27/10 a las 15:00. El 25 no corresponde desayuno y sí almuerzo/cena. El 26 permite las tres comidas. El 27 permite desayuno/almuerzo y no cena. Se puede agregar hospedaje. Si se asignan un desayuno (₡3.000), un almuerzo (₡5.000), una cena (₡5.000) y hospedaje de ₡10.000 en días válidos, el conjunto suma ₡23.000. El precio de hospedaje lo define el catálogo; no forma parte de los tres ejemplos iniciales.

Cena usa el límite del enunciado escrito aplicado por el PR #5. La indicación contraria del PDF del 15 de octubre es histórica y queda sustituida por esta tabla.

La transcripción de la explicación (aproximadamente 49:34) dice salida a las 16:00 o después, mientras que el enunciado original dice a las 16:00 o antes. La aplicación conserva el criterio escrito. Esta discrepancia de las fuentes debe distinguirse de un fallo del cálculo; el equipo debe confirmar el criterio académico antes de sustituir la regla.

## Bitácora y reportes

Se registra una entrada después de cada creación, modificación o eliminación completada desde la interfaz, con el autor de `SesionActual`. Una liquidación se registra como conjunto, no por cada cambio de borrador. No se registran como éxitos búsquedas, reportes, rechazos o cancelaciones. La carga inicial genera una entrada si agregó ejemplos.

Los reportes de consumo relacionan IDs de listas existentes, muestran una fila por detalle y usan `fechaConsumo.Date`, no la fecha de creación. Incluyen historial de registros inactivos y suman el monto del detalle, no el precio actual. El rango incluye ambos días completos. El reporte por empleado filtra por ID real y su selector distingue homónimos.

El reporte de operaciones filtra por autor y fechas, también con ambos días completos. Los filtros inválidos limpian los resultados anteriores. Los consumos sin resultados muestran total cero. Las claves no aparecen en la tabla de Usuarios ni se escriben en bitácora.

## Carga inicial

Es manual y solo para Administrador. Agrega los ejemplos que falten por nombre de usuario, cédula y nombre de tipo. Conserva cambios en precio, clave o estado de ejemplos existentes. Los nuevos registros se preparan con IDs libres antes de modificar las listas.

Devuelve cantidad agregada, `-1` por falta de permiso o `-2` si no pudo preparar/aplicar la carga, con explicación en `ultimoError`. La carga completa inicial agrega cinco registros: usuario Normal, empleado ficticio y tres tipos (₡3.000 / ₡5.000 / ₡5.000).

## Presentación y verificación

`DisenoFormularios` organiza campos en filas, mide el contenido con el ancho disponible y mantiene páginas desplazables. Los formularios conservan sus eventos y validaciones. El menú lateral también permite desplazarse cuando falta altura.

El contrato se verifica con las suites de `tests/` y el recorrido de [aceptación](Guia_de_aceptacion.md). El almacenamiento en memoria y las reglas anteriores son el alcance académico; no se añadieron servicios, bases de datos ni dependencias de aplicación.
