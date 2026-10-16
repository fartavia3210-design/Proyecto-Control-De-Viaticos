# Control de viáticos — Los 3 Patitos S.A.

Repositorio de trabajo de Mango, Fabián y Dubán para el proyecto de Programación II.

## Estado actual

El repositorio contiene el proyecto inicial de Windows Forms en C# con .NET Framework 4.7.2. Por ahora muestra el formulario inicial `Form1`; las funciones del sistema están pendientes de desarrollar. El plan de construcción y el reparto de tareas están en [docs/Plan_de_construccion_control_de_viaticos.pdf](docs/Plan_de_construccion_control_de_viaticos.pdf).

## Abrir y ejecutar

1. Instalar la carga de trabajo **Desarrollo de escritorio de .NET** en Visual Studio Community y el paquete de destino de .NET Framework 4.7.2.
2. Abrir [ControlViaticos.slnx](ControlViaticos.slnx). Si la versión de Visual Studio no reconoce ese formato de solución, abrir directamente [ControlViaticos.csproj](ControlViaticos/ControlViaticos.csproj).
3. Presionar **F5** para compilar y ejecutar el formulario inicial.

La solución está en la raíz del repositorio y el código fuente está en `ControlViaticos/`. Las carpetas `.vs/`, `bin/` y `obj/` son locales y están excluidas de Git.

La entrega será una aplicación de escritorio en C# con WinForms, abierta desde Visual Studio Community y con .NET Framework 4.7.2 o superior. Los datos del proyecto se manejan con objetos y listas. No se usarán paquetes NuGet adicionales.

## Reparto inicial

| Integrante | Área principal |
|---|---|
| Fabián | Estructura, usuarios, acceso, permisos, carga inicial, bitácora e integración. |
| Mango | Navegación y diseño visual, empleados, tipos de viático y vistas de reportes. |
| Dubán | Liquidaciones, validaciones, cálculos y consultas de consumo. |

## Forma de trabajo

1. Abrir una tarea concreta antes de programarla y asignarle un responsable.
2. Crear una rama corta para esa tarea, por ejemplo `feature/empleados-crud`.
3. Hacer commits pequeños y describir lo que cambió.
4. Abrir una solicitud de cambio (*pull request*) hacia `main`. Otro integrante la revisa y prueba antes de integrarla.
5. Mantener `main` en un estado que abra y ejecute. Las decisiones sobre objetos y listas compartidos se acuerdan antes de construir funciones que dependan de ellas.

El video de la explicación y los materiales originales del curso permanecen fuera de este repositorio.
