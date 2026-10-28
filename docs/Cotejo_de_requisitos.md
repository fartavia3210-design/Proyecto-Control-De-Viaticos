# Cotejo de requisitos y versión de cierre

Revisión del 28 de octubre de 2026 sobre `release/cierre-final-viaticos`, base `9459dab` y correcciones locales posteriores. Fuentes: `ITI 321 - Proyecto I.docx`, `Rubrica I Proyecto.xlsx` y `Transcripcion_video_explicacion_proyecto.txt`, ubicados en la carpeta superior al repositorio. No se sustituyen por el plan histórico.

## Funciones de la aplicación

| Requisito | Implementación y comprobación |
|---|---|
| C#, Visual Studio Community, WinForms, .NET Framework 4.7.2 o superior | Solución `.sln`, proyecto dirigido a 4.7.2 y compilación Release con MSBuild de Visual Studio. |
| Sin NuGet adicionales | Referencias del framework; las pruebas se compilan aparte. |
| Navegación dentro del menú principal | Formularios hijos, cierre con liberación de recursos y confirmación del borrador de liquidaciones. |
| Login, roles y estado de usuario | Administrador y Normal; Normal no gestiona Usuarios ni Carga inicial. Cuenta inactiva rechazada. |
| CRUD de Usuarios | ID, nombre completo, usuario, clave, tipo y estado. Tabla visual sin mostrar la clave. |
| CRUD de Empleados | ID, cédula, nombre, dos apellidos, teléfono, dirección y estado. Objeto y tabla distintos del usuario de acceso. |
| CRUD de tipos de viático | ID, nombre/categoría, monto y estado. Restricciones de duplicados y relaciones. |
| Liquidaciones maestro-detalle | Cabecera y detalles en objetos/listas distintos; empleado, consecutivo, inicio/fin, justificación y varios conceptos con montos y fechas. |
| Operaciones dobles | Crear, recuperar, modificar y eliminar cabecera/detalles. La sustitución de una fila se prepara quitándola y agregando su reemplazo; el conjunto se valida antes de guardarse. |
| Viajes de varios días | Fecha por consumo; salida limita el primer día, llegada limita el último y días intermedios permiten comidas. Incluye hospedaje sin regla de comida. |
| Restricciones por estado | Nuevas asignaciones requieren empleado y tipo activos. Editar historial no permite agregar pagos al empleado inactivo ni trasladar consumos inactivos a otro día o beneficiario. |
| Carga manual en tres listas | Solo Administrador; usuario, empleado, desayuno 3000, almuerzo 5000 y cena 5000. No se ejecuta al abrir ni impide altas posteriores. Repetirla conserva datos existentes. |
| Horarios | Desayuno ≤07:00/≥10:00, almuerzo ≤10:00/≥14:00 y cena ≤16:00/≥20:00 según el escrito. Se prueban límites y un minuto fuera. La interfaz guarda la precisión de minutos que muestra. |
| Bitácora independiente | Usuario, fecha/hora, sección y movimiento; una entrada por escritura completada desde la interfaz, sin éxitos por rechazo o cancelación. |
| Reporte por fechas | Empleado, fecha, viático, monto y total; filtro inclusivo de ambos días. |
| Reporte por empleado | Selección por ID real, fecha, viático, monto y total; incluye historial inactivo. |
| Reporte de operaciones | Usuario y fechas como filtros; usuario, fecha/hora, sección y movimiento. La explicación corrige la palabra empleado por usuario. |
| Objetos y listas | Seis listas compartidas durante la ejecución. El curso no exige persistencia al cerrar. |
| Documentación interna | Comentarios sobre validación, historial, IDs, presentación y almacenamiento. Responsabilidades separadas en Objetos, Logica y Presentacion. |
| Informe | DOCX y PDF con diseño, problemas/soluciones y lecciones. Se verificaron dos columnas, 12 puntos, márgenes estándar y PDF de dos páginas. |

## Responsabilidades del código corregido

- `ReglasViaticos` define únicamente categorías y ventanas horarias.
- `LogicaLiquidaciones.ValidarCabecera` comprueba viaje y beneficiario; `ValidarDetalles` comprueba conjunto, relaciones y total.
- `ValidarDetalleNuevo` exige estados activos y precio vigente. `ValidarDetalleHistorico` protege identidad, monto y conservación de registros inactivos.
- `GuardarConjunto` prepara copias e identificadores y después escribe las dos listas. Las validaciones no modifican el almacenamiento.
- `frmLiquidaciones` lee controles, normaliza la hora visible, prepara detalles, muestra mensajes y registra el éxito una vez completado. Su carga separa configuración de tablas, catálogos y eventos.

## Discrepancias y condiciones de entrega

La transcripción (49:34) indica salida para Cena a las 16:00 o después; el documento original indica a las 16:00 o antes. Se mantiene el documento escrito y se deja constancia de la contradicción, sin inventar una confirmación del profesor.

Las pruebas técnicas no certifican qué técnicas se enseñaron en todas las clases. El equipo debe explicar los métodos, estructuras, genéricos y eventos que utiliza. También corresponde a los integrantes asistir a la defensa, revisar el informe y cumplir su cita y plazo de entrega.

Las capturas a 125 % y 150 % simulan escalado de WinForms. No sustituyen la revisión con DPI real ni en otras computadoras. Los resultados concretos de la ejecución local se registran en `Validacion_de_cierre.md`.
