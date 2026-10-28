# Validación de cierre

Fecha: 28 de octubre de 2026. Rama: `release/cierre-final-viaticos`.

## Resultado comprobado

La aplicación de la rama compiló en Windows en configuración Release y completó **157 comprobaciones automáticas**:

| Suite | Comprobaciones |
|---|---:|
| LiquidacionesRegression | 36 |
| CierreRegression | 95 |
| AccesoUsuariosRegression | 26 |

Ejecución de referencia del código funcional final (`2cc26bd217d49d6c81437de8a91f32cb5d6fd017`):
https://github.com/fartavia3210-design/Proyecto-Control-De-Viaticos/actions/runs/40643749513

La compilación se realizó desde `ControlViaticos.sln`, con el proyecto dirigido a .NET Framework 4.7.2. No se añadieron PackageReference ni paquetes NuGet. Las pruebas adicionales se compilan aparte y no se incluyen en la aplicación.

Se comprobaron CRUD, duplicados, permisos por rol, protección de relaciones, rechazo sin cambios parciales, conservación de montos históricos, límites de comidas y fecha de consumo, carga idempotente, IDs externos máximos, bitácora con dos autores y tres reportes. Los casos de navegación comprueban que cancelar el descarte conserva la preparación y la sesión, y que aceptarlo libera el formulario anterior.

El caso de montos altos suma dos liquidaciones de ₡1.500.000.000 con consumos anteriores, obteniendo totales superiores a `int.MaxValue` correctos. El ejecutor utiliza procesos separados, registra salida UTF-8 y limita cada suite a dos minutos para obtener un diagnóstico ante un bloqueo.

## Revisión visual

La misma ejecución generó **50 capturas de Windows**: login, primer administrador y nueve opciones del menú; tamaños normal y pequeño; páginas desplazadas; escalado simulado a 125 % y 150 %. El artefacto se llama `revision-visual-final`.

Se revisaron capturas de los módulos, controles de búsqueda, tablas y campos, los totales de reportes y las acciones de liquidaciones después de desplazar la página. La ventana inicial se ajusta al área útil del monitor. En el runner se comprobó una zona de trabajo de 1024 × 720 y una captura normal de 992 × 656, dentro del escritorio.

El escalado de capturas se produce mediante `Scale` de WinForms. No representa una prueba de DPI real del sistema o de varios monitores. La guía de aceptación explica la comprobación en las computadoras del equipo.

También se ejecutaron pruebas y revisión de capturas bajo Mono 6.12 / Xvfb como comprobación complementaria. La evidencia de Windows es la referencia de la plataforma de entrega.

## Cómo revisar la versión

1. Seleccionar esta rama y abrir `ControlViaticos.sln` en Visual Studio Community.
2. Seguir `Guia_de_aceptacion.md` con datos ficticios.
3. Consultar la última ejecución de la rama en Actions. Incluye capturas y el artefacto `aplicacion-windows` con el ejecutable Release y su configuración, para revisarlo en Windows con el framework instalado.
4. Revisar el informe editable con los tres integrantes y cotejarlo con los originales del curso.

## Alcance y pendientes externos

En los casos automatizados y las capturas revisadas no se observaron fallos pendientes. Esto no sustituye la revisión del usuario antes del merge. Main no recibe automáticamente estos cambios.

Los datos viven en memoria y se pierden al finalizar el programa, conforme al contrato académico. El enunciado y la rúbrica originales, la pertenencia a un grupo y la cita de exposición están fuera del repositorio; no se han certificado ni inventado. El informe y la guía quedan preparados para su revisión por el equipo.
