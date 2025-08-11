## Sistema Solicitud de Permisos 

Aplicación web para gestionar solicitudes de permisos de usuarios, con panel de administración, carga de documentos y notificaciones por correo electrónico.  
Frontend desarrollado en **Angular**, backend en **ASP.NET Core Web API** y base de datos en **SQL Server**.

---

## Tecnologías utilizadas
- **Frontend**: Angular, HTML5, Sass, TypeScript
- **Backend**: ASP.NET Core 6 Web API, C#
- **Base de datos**: SQL Server, Entity Framework Core
- **Autenticación**: JWT (JSON Web Tokens)
- **Notificaciones**: SMTP para envío de correos
- **Herramientas**: Git, GitHub, Visual Studio Code, Visual Studio 2022


## Estructura del proyecto
solicitud-permisos/
├── backend/ # API en .NET
│ ├── Controllers/
│ ├── Models/
│ ├── Services/
│ ├── appsettings.json
├── frontend/ # Angular
│ ├── src/
│ ├── angular.json
├── database/ # Scripts SQL
│ ├── PermisosApp.sql
└── README.md

## Requisitos previos
Antes de comenzar, asegúrate de tener instalado:
- [Node.js](https://nodejs.org/) v16 o superior
- [Angular CLI](https://angular.io/cli)
- SQL Server Management Studio (SSMS)
- Git

## Instalación y configuración
1️. Clonar el repositorio
git clone https://github.com/leisy95/Project-Solicitud-Permisos.git
cd Project-Solicitud-Permisos

2️. Configurar base de datos

Opción A: Ejecutar script SQL
Abrir database/PermisosApp.sql en SSMS y ejecutarlo.

Opción B: Usar migraciones de Entity Framework
cd backend
- dotnet restore
- dotnet ef database update
  
3. Configurar backend (.NET)
Editar backend/appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=TU_SERVIDOR;Database=PermisosApp;Trusted_Connection=True;"
}
Si usas usuario y contraseña de SQL Server:

"DefaultConnection": "Server=TU_SERVIDOR;Database=PermisosApp;User Id=USUARIO;Password=CONTRASEÑA;"

## Levantar API:

cd backend
dotnet run
Backend disponible en: https://localhost:5204

4. Configurar frontend (Angular)

cd frontend
npm install
ng serve
Frontend disponible en: http://localhost:4200

## Funcionalidades principales

- Registro y login de usuarios con JWT

- Solicitud de permisos con adjunto PDF

- Panel de administración para aprobar/rechazar solicitudes

- Exportación de datos a Excel y PDF

- Notificación automática por correo al usuario

## Mantenimiento
Despliegue local: usando ng serve y dotnet run.


# Autor
Leisy Ladino
GitHub: leisy95

