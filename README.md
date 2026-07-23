Nombre del proyecto
===================

ExpenseTrackerApi_04

Descripción
-----------

API para gestión de gastos personales. Proyecto en C# usando ASP.NET Core Web API y orientado a la separación por capas (Application, Domain, Infrastructure). Diseñado para ejecutarse con .NET 10.

Estado actual
------------

- Código en el branch `master` del repositorio remoto origin.
- La solución está en: ExpenseTrackerApi_04.slnx (raíz del repositorio).
- Estructura parcial conocida: carpetas como `Application` (casos de uso), `Domain` (modelos/entidades) y probablemente proyectos de API e infraestructura.

Tecnologías
-----------

- .NET 10
- C#
- ASP.NET Core Web API
- Visual Studio Community 2026 compatible con la solución

Requisitos
----------

- .NET 10 SDK instalado (dotnet --version debería devolver una versión 10.x)
- Visual Studio 2022/2026 o VS Code (opcional) para desarrollo

Cómo ejecutar
--------------

1. Clonar el repositorio:

   git clone https://github.com/jose-angell/ExpenseTrackerApi_04.git
   cd ExpenseTrackerApi_04

2. Restaurar dependencias y compilar la solución:

   dotnet restore ExpenseTrackerApi_04.slnx
   dotnet build ExpenseTrackerApi_04.slnx

3. Ejecutar desde la solución en Visual Studio:

   - Abrir `ExpenseTrackerApi_04.slnx` con Visual Studio Community 2026 y ejecutar el proyecto de tipo Web API como proyecto de inicio.

4. Ejecutar desde la CLI:

   - Si conoce el proyecto API (por ejemplo `src/ExpenseTrackerApi.Api/ExpenseTrackerApi.Api.csproj`), ejecutar:

	 dotnet run --project <ruta-al-proyecto-api>.csproj

   - Si no está seguro del nombre del proyecto API, listar los archivos `.csproj` en la solución y seleccionar el que tenga plantilla Web/API.

Notas sobre configuración
------------------------

- Variables de entorno o cadenas de conexión necesarias pueden estar definidas en archivos de configuración dentro del proyecto (appsettings.json) o en secretos de usuario. Revisar la carpeta del proyecto API para detalles.

Pruebas
------

- Ejecutar tests (si existen) con:

  dotnet test ExpenseTrackerApi_04.slnx

Contribuir
----------

- Abrir un issue o crear un pull request contra la rama `master` en el repositorio remoto.
- Seguir las buenas prácticas: crear branch por feature/bugfix, escribir mensajes claros y mantener commits atómicos.

Contacto
-------

Repositorio remoto: https://github.com/jose-angell/ExpenseTrackerApi_04

Licencia
--------

No se ha añadido archivo de licencia en esta copia. Añadir LICENSE si se requiere una licencia explícita.

