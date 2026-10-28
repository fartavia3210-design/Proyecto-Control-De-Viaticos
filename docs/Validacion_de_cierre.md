# Validación de cierre

Fecha: 28 de octubre de 2026. Rama: `release/cierre-final-viaticos`.

## Resultado comprobado

La aplicación compiló localmente en Windows en configuración Release y completó **197 comprobaciones automáticas** sobre la base `9459dab` y las correcciones locales posteriores:

| Suite | Comprobaciones |
|---|---:|
| LiquidacionesRegression | 36 |
| CierreRegression | 95 |
| AccesoUsuariosRegression | 26 |
| EstadosLiquidacionesRegression | 40 |

La ejecución local utiliza `tests/Run-Regression.ps1`. El registro generado se conserva en `screenshots/validacion-local.log` (excluido de Git). Estos resultados locales no son una ejecución nueva de GitHub Actions.

Ejecución histórica de referencia, anterior a estas correcciones (`2cc26bd217d49d6c81437de8a91f32cb5d6fd017`, 157 comprobaciones):
https://github.com/fartavia3210-design/Proyecto-Control-De-Viaticos/actions/runs/40643749513

La compilación se realizó desde `ControlViaticos.sln`, con el proyecto dirigido a .NET Framework 4.7.2. No se añadieron PackageReference ni paquetes NuGet. Las pruebas adicionales se compilan aparte y no se incluyen en la aplicación.

También compiló Debug con `/p:OutputPath=bin\VerificacionDebug\`. Se utilizó esa carpeta porque otra instancia tenía abierto `bin/Debug/ControlViaticos.exe`; se conservó la sesión de esa instancia. El bloqueo de copia no era un error del código.

Se comprobaron CRUD, duplicados, permisos por rol, protección de relaciones, rechazo sin cambios parciales, conservación de montos históricos, límites de comidas y fecha de consumo, carga idempotente, IDs externos máximos, bitácora con dos autores y tres reportes. Los casos de navegación comprueban que cancelar el descarte conserva la preparación y la sesión, y que aceptarlo libera el formulario anterior.

El caso de montos altos suma dos liquidaciones de ₡1.500.000.000 con consumos anteriores, obteniendo totales superiores a `int.MaxValue` correctos. El ejecutor utiliza procesos separados, registra salida UTF-8 y limita cada suite a dos minutos para obtener un diagnóstico ante un bloqueo.

## Correcciones reproducidas y verificadas localmente

La nueva suite reprodujo primero el guardado de pagos adicionales a un empleado inactivo. Después de la corrección comprueba el rechazo desde lógica y formulario, la conservación de cabecera/detalles/monto, ausencia de bitácora de éxito y recuperación al reactivar. También cubre el traslado indebido de un concepto inactivo a otro empleado o fecha, la conservación del precio histórico y el rechazo de IDs históricos en una cabecera nueva.

Una segunda reproducción detectó segundos ocultos del selector de fecha/hora: una salida mostrada como 07:00 podía tener internamente 07:00:37 y rechazar el desayuno. El formulario ahora valida y guarda con la misma precisión de minutos. Se comprueba este recorrido completo para desayuno, almuerzo y cena.

La validación se separó en cabecera, conjunto de detalles, detalles nuevos e historial. El guardado permanece separado y solo escribe después de validar. La carga del formulario distingue tablas, catálogos y conexión de eventos.

## Revisión visual

La ejecución local de `tests/Capture-Comparison.ps1` generó **50 capturas de Windows**: login, primer administrador y nueve opciones del menú; tamaños normal y pequeño; páginas desplazadas; escalado simulado a 125 % y 150 %. Se guardan en `screenshots/`; el registro está en `capturas-locales.log`. En las ejecuciones históricas de GitHub, el artefacto se llama `revision-visual-final`.

Se corrigió el capturador para usar `DrawToBitmap` con el tamaño completo del formulario. La copia directa del escritorio mezclaba píxeles físicos y tamaños virtualizados por Windows, recortando imágenes en esta computadora. Las capturas nuevas representan el formulario renderizado y no incluyen otras aplicaciones que lo cubran.

Se revisaron capturas de los módulos, controles de búsqueda, tablas y campos, los totales de reportes y las acciones de liquidaciones después de desplazar la página. La ventana inicial se ajusta al área útil del monitor. En el runner se comprobó una zona de trabajo de 1024 × 720 y una captura normal de 992 × 656, dentro del escritorio.

El escalado de capturas se produce mediante `Scale` de WinForms. No representa una prueba de DPI real del sistema o de varios monitores. La guía de aceptación explica la comprobación en las computadoras del equipo.

También se ejecutaron pruebas y revisión de capturas bajo Mono 6.12 / Xvfb como comprobación complementaria. La evidencia de Windows es la referencia de la plataforma de entrega.

## Cómo revisar la versión

1. Seleccionar esta rama y abrir `ControlViaticos.sln` en Visual Studio Community.
2. Seguir `Guia_de_aceptacion.md` con datos ficticios.
3. Consultar la última ejecución de la rama en Actions. Incluye capturas y el artefacto `aplicacion-windows` con el ejecutable Release y su configuración, para revisarlo en Windows con el framework instalado.
4. Revisar el informe editable con los tres integrantes y cotejarlo con los originales del curso.

## Alcance y pendientes externos

Después de corregir las reproducciones anteriores, no se observaron fallos pendientes en los casos automatizados y las capturas revisadas. Esto no garantiza ausencia absoluta de errores ni sustituye la comprobación con DPI real. Main no recibe automáticamente estos cambios.

Los datos viven en memoria y se pierden al finalizar el programa, conforme al contrato académico. Se cotejaron el enunciado, la rúbrica y la transcripción de la carpeta superior; el resultado está en `Cotejo_de_requisitos.md`. Se mantiene Cena según el documento escrito, dejando identificada su contradicción con la transcripción. La pertenencia a un grupo, la cita de exposición y las técnicas enseñadas en todas las clases requieren confirmación del equipo. El informe y la guía quedan preparados para su revisión conjunta.
