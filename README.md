# Control de viáticos — Los 3 Patitos S.A.

Aplicación de escritorio de Programación II, desarrollada por Fabián, Mango y Duván en C#, WinForms y .NET Framework 4.7.2. Usa objetos y listas en memoria, sin base de datos ni paquetes NuGet adicionales.

## Versión para revisión

La rama `release/cierre-final-viaticos` reúne la entrega de Mango y las correcciones de cierre. Se conserva separada de `main` hasta que Fabián la revise. Incluye:

- Registro explícito del primer administrador, login, sesión y permisos por rol.
- Crear, buscar, modificar y eliminar usuarios, empleados y tipos de viático.
- Liquidaciones con cabecera y varios detalles, fecha de consumo, validaciones de horarios y montos históricos.
- Confirmación de descarte de borradores al cambiar de módulo o salir.
- Bitácora, carga inicial manual y tres reportes con filtros funcionales.
- Formularios organizados en filas y páginas desplazables para mantener accesibles los controles.
- Pruebas automáticas de lógica, formularios y capturas de los módulos.

## Abrir y ejecutar en Windows

1. Descargar o seleccionar la rama que se quiere revisar.
2. Instalar Visual Studio Community con **Desarrollo de escritorio de .NET** y el paquete de destino de **.NET Framework 4.7.2**.
3. Abrir [ControlViaticos.sln](ControlViaticos.sln). También se puede abrir el `.csproj` directamente. Se conserva `.slnx` para versiones que lo admitan.
4. Presionar **F5**. Crear el primer administrador con datos propios e iniciar sesión.
5. Si se necesitan ejemplos, abrir **Carga inicial** como Administrador y pulsar **Ejecutar carga inicial**. Los ejemplos no se cargan al abrir el programa.

Para probar el ejecutable sin abrir Visual Studio, la última ejecución correcta de esta rama en **Actions** ofrece el artefacto `aplicacion-windows`. Descargarlo y extraer sus dos archivos en la misma carpeta; ejecutar `ControlViaticos.exe` en Windows con .NET Framework 4.7.2 o superior.

Los datos se conservan al navegar y cambiar de sesión durante una ejecución. Al cerrar el programa se pierden y se solicita de nuevo el primer administrador: es el almacenamiento en memoria acordado para el curso.

La carga agrega un empleado ficticio, desayuno ₡3.000, almuerzo ₡5.000, cena ₡5.000 y un usuario Normal de demostración (`usuarioEjemplo`, clave `ejemplo123`). Repetirla no reemplaza los datos existentes.

## Pruebas

Desde PowerShell en Windows, con las herramientas anteriores:

```powershell
msbuild ControlViaticos.sln /p:Configuration=Release /verbosity:minimal
./tests/Run-Regression.ps1
./tests/Capture-Comparison.ps1
```

El ejecutor prueba liquidaciones, el cierre integral y acceso/usuarios. Las capturas quedan en `screenshots/`, carpeta excluida de Git. GitHub Actions ejecuta estas comprobaciones en Windows para los PR y las ramas `main` y `release/**`.

Las capturas de 125 % y 150 % simulan escalado mediante WinForms. La revisión con DPI real en los equipos del equipo se describe en la guía de aceptación.

## Documentación de entrega

- [Contrato vigente](docs/Contrato_del_proyecto.md): estructuras, permisos, reglas e identificadores.
- [Guía de aceptación y demostración](docs/Guia_de_aceptacion.md): recorrido completo para revisar la rama.
- [Actualización del plan](docs/Actualizacion_del_plan.md): cierre y corrección de la regla de Cena del PDF histórico.
- [Informe editable](docs/Informe_del_proyecto.docx) y [versión PDF](docs/Informe_del_proyecto.pdf).
- [Validación de cierre](docs/Validacion_de_cierre.md): resultados y límites de las pruebas.

El contrato inicial se conserva en `docs/historial/`. El enunciado, la rúbrica y el video originales del curso permanecen fuera del repositorio.
