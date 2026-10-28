# Guía de aceptación y demostración

Usar una copia de `release/cierre-final-viaticos`, Visual Studio Community y Windows con .NET Framework 4.7.2. No usar datos personales para los ejemplos.

## Recorrido desde cero

1. Abrir `ControlViaticos.sln` y ejecutar F5. Crear un Administrador. Comprobar que todavía no existen ejemplos.
2. Probar login vacío, clave incorrecta y acceso correcto. Crear un usuario Normal y otro inactivo; el inactivo no debe entrar.
3. Como Administrador, ejecutar Carga inicial. Verificar cinco ejemplos, precios 3000/5000/5000 y una entrada de bitácora. Repetir: cero agregados, sin duplicados.
4. En Usuarios, crear, buscar, modificar y borrar una cuenta sin historial. Probar duplicados. No permitir eliminar/desactivar la cuenta en uso ni dejar el sistema sin administrador activo.
5. En Empleados, crear dos personas con el mismo nombre y distinta cédula/ID. Buscar, modificar, probar cédula duplicada y borrar una sin relaciones. Un rechazo conserva datos y campos.
6. En Tipos, crear hospedaje con monto positivo. Probar monto cero, texto no entero y nombre duplicado. Crear/modificar/borrar un tipo sin relaciones.
7. Preparar una liquidación: empleado activo, justificación, fechas y varios detalles. Antes de guardar, ir a otro módulo y responder No: conservar la preparación. Repetir y responder Sí: descartarla sin insertar cabecera ni detalles. Repetir con X del menú.
8. Guardar una liquidación completa. Buscarla, modificarla y volver al módulo: deben recuperarse cabecera, detalles, fechas y total. Quitar el último detalle y tratar de guardar: rechazar sin alterar lo anterior.
9. Cambiar el precio del catálogo. El detalle anterior mantiene su monto. Desactivar empleado o tipo usados: el historial se consulta, pero no se ofrecen para una asignación nueva. No se pueden borrar mientras estén relacionados.
   - Con un empleado desactivado, buscar una liquidación anterior e intentar agregar un concepto activo: debe rechazarse sin cambiar total, detalles ni bitácora. Corregir la justificación y conservar los consumos originales sí está permitido.
   - Preparar un detalle mientras el empleado está activo y desactivarlo antes del guardado: Guardar también debe rechazarlo. Reactivar permite completar la operación una sola vez.
   - No trasladar un tipo inactivo a otro empleado o fecha. El historial mantiene fecha y monto originales; quitar una fila solo afecta la preparación hasta guardar.
10. Comprobar los límites de la tabla siguiente, tanto exactos como un minuto fuera. Probar un viaje de varios días según el ejemplo del contrato y un hospedaje sin regla de comida.
11. Consultar consumos por fechas y por cada empleado: filas distintas, ambos días incluidos, total correcto, inactivos disponibles y nombres iguales distinguibles por ID. Un rango vacío deja cero; un filtro inválido limpia lo anterior.
12. Cerrar sesión y entrar como Normal. Empleados, Tipos y reportes deben funcionar; Usuarios y Carga inicial deben estar restringidos.
13. Completar cambios desde ambos usuarios y consultar el reporte de operaciones por cada autor y por fechas. Confirmar una entrada por escritura correcta, sin contraseñas, ni éxitos por rechazo/cancelación.
14. Borrar la liquidación guardada, con confirmación; cabecera y detalles desaparecen juntos. Cancelar un borrado no cambia nada.
15. Ejecutar `tests/Run-Regression.ps1`. El caso de montos grandes prueba dos liquidaciones individuales válidas cuya suma supera `int.MaxValue`.

| Viático | Salida exacta | Salida rechazada | Llegada exacta | Llegada rechazada |
|---|---|---|---|---|
| Desayuno | 07:00 | 07:01 | 10:00 | 09:59 |
| Almuerzo | 10:00 | 10:01 | 14:00 | 13:59 |
| Cena | 16:00 | 16:01 | 20:00 | 19:59 |

Para cada fila usar un mismo día y mantener válido el otro extremo. En varios días, aplicar la salida al primero y la llegada al último.

El formulario evalúa los minutos que muestra, sin segundos ocultos. Para comprobarlo, editar Inicio hasta el límite exacto mostrado y guardar una liquidación válida. Las pruebas automáticas incluyen valores del selector con segundos internos para las tres comidas.

Para modificar un detalle, buscar la liquidación, seleccionar y quitar la fila, agregar su reemplazo y guardar el conjunto. Buscar o cancelar antes de guardar conserva el detalle anterior. Las nuevas asignaciones utilizan el precio vigente y requieren empleado y tipo activos.

## Presentación en Windows

Revisar las nueve opciones del menú, login y registro inicial. Probar ventana normal, ventana pequeña, maximizar y restaurar. Desplazarse hasta tablas, Guardar y totales; no exigir que todo quepa simultáneamente. Comprobar etiquetas largas, fechas completas, Administrador, acentos y colones.

Repetir con la configuración real de Windows a 100 %, 125 % y 150 %, cerrando y abriendo la aplicación después del cambio. Las capturas automáticas con `Scale` son simulaciones y no reemplazan esta comprobación de pantalla.

## Guion de exposición

- Fabián: explicar objetos/listas, primer acceso, roles, Usuarios, carga y bitácora; mostrar un rechazo de permiso y el reporte de operaciones.
- Mango: demostrar Empleados, Tipos y reportes; explicar IDs frente a posiciones del combo y las filas con desplazamiento.
- Duván: preparar un viaje de varios días, demostrar límites horarios, guardado completo y montos históricos.
- Cada integrante explica además una función ajena. Usar el mismo juego ficticio de datos y la tabla del contrato para mantener coherencia.

El informe editable debe revisarse por los tres antes de entregarlo. Confirmar la rúbrica original, el grupo y la cita exacta; los plazos históricos del plan no son una confirmación de matrícula o entrega.
