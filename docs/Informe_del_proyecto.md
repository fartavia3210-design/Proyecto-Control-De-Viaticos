# Control de viáticos — informe del proyecto

Equipo: Fabián, Mango y Duván. Programación II.

## Diseño de la aplicación

El sistema de control de viáticos para Los 3 Patitos S.A. permite administrar usuarios, empleados y tipos de viático, registrar liquidaciones de viajes y consultar consumos y operaciones. Se desarrolló como una aplicación de escritorio en C# con Windows Forms y .NET Framework 4.7.2. Los registros se conservan en objetos y listas durante la ejecución, según el alcance del curso; no se incorporaron una base de datos ni paquetes NuGet adicionales.

La organización separa los objetos, la lógica y la presentación. Las seis listas compartidas almacenan usuarios, empleados, tipos, liquidaciones, detalles y bitácora. Los formularios reciben datos, muestran resultados y llaman a las validaciones. El acceso empieza con la creación explícita del primer administrador. La carga de ejemplos queda como una acción manual y restringida, mientras que el usuario Normal puede trabajar con los módulos operativos y los reportes.

Cada liquidación relaciona una cabecera con varios detalles. La cabecera identifica al empleado y al viaje; cada detalle conserva el tipo, la fecha de consumo y el monto aplicado. Esta copia del monto permite consultar el gasto original aunque después cambie el precio del catálogo. Los selectores utilizan identificadores reales, no posiciones de una lista.

## Problemas encontrados y soluciones

La integración mostró que guardar una cabecera antes de completar sus viáticos podía dejar registros incompletos. La solución fue preparar el conjunto en el formulario, validar todos sus datos y escribirlo solo cuando sea válido. Un rechazo conserva la versión anterior. Las consultas devuelven copias de los detalles para que modificar o cancelar una preparación no cambie accidentalmente lo almacenado.

También se detectó que cambiar de módulo retiraba el formulario sin preguntar por el borrador. Se unificó el cierre: la liquidación decide si puede descartarse y el menú respeta la cancelación. Solo después de aceptar se cierra y libera la pantalla anterior. Así se evita tanto perder la preparación sin aviso como mantener formularios retirados sin liberar.

Los reportes sumaban montos con un entero del mismo tamaño que el monto individual. Varias liquidaciones válidas podían producir un total negativo. La acumulación se trasladó a un entero de mayor capacidad y a una operación común de la lógica. La asignación de identificadores también busca valores positivos libres sin sumar a un identificador máximo externo.

En la presentación había campos recortados y nombres iguales difíciles de distinguir. Los selectores muestran el ID y el nombre completo. Los catálogos y reportes usan contenedores compartidos que miden las filas con el ancho disponible y permiten desplazarse. Esta solución trata la distribución del contenido desde su contenedor, sin corregir cada pantalla con posiciones independientes.

## Reglas y comprobación

Las reglas de desayuno, almuerzo y cena se concentraron en una sola clase. En viajes de varios días se aplica la salida al primero y la llegada al último; los días intermedios permiten las comidas. Se conservó para Cena la salida a las 16:00 o antes y llegada a las 20:00 o después, siguiendo la corrección integrada del enunciado escrito. Hospedaje no recibe restricciones de comida.

La validación incluye los cuatro CRUD, permisos, duplicados, cancelaciones, relaciones, bitácora, reportes y horas exactas o un minuto fuera. Las pruebas se ejecutan en Windows mediante GitHub Actions; además se generan capturas de los módulos y ventanas pequeñas. El escalado automático de capturas es simulado y se complementa con una guía para revisar DPI real en las computadoras del equipo.

## Lecciones aprendidas

Una integración no termina al fusionar archivos: también hay que comprobar los contratos compartidos, los casos límite y lo que ocurre al navegar. Separar preparación, validación y escritura facilita explicar el código y conservar datos ante errores. Registrar solo operaciones completadas hace que la bitácora sea útil para revisar el trabajo de cada usuario.

Los commits por tema y las pruebas de regresión permiten revisar cambios concretos sin mezclar responsabilidades. La documentación debe describir el comportamiento actual y distinguir los acuerdos históricos. La versión de cierre queda en una rama separada para revisión; antes de la entrega académica, el equipo debe contrastar la rúbrica original, revisar este informe y ensayar la demostración conjunta.