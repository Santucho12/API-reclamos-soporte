# Sistema de Reclamos y Soporte

Este proyecto es una API RESTful desarrollada en ASP.NET Core para la gestión de reclamos y soporte de usuarios. Permite registrar usuarios, crear y gestionar reclamos, y administrar respuestas de soporte, facilitando la comunicación entre clientes y el área de soporte técnico.

## Características principales
- **Gestión de usuarios:** Alta, consulta, actualización y eliminación de usuarios.
- **Gestión de reclamos:** Los usuarios pueden crear reclamos, ver el estado, modificarlos y eliminarlos.
- **Respuestas a reclamos:** El personal de soporte puede responder a los reclamos y los usuarios pueden ver el historial de respuestas.
- **Relaciones:**
  - Un usuario puede tener varios reclamos y respuestas.
  - Un reclamo pertenece a un usuario y puede tener varias respuestas.
  - Una respuesta pertenece a un reclamo y a un usuario (soporte o cliente).
- **Swagger UI:** Documentación y pruebas interactivas disponibles en `/` (por defecto `http://localhost:5138/`).


## Estructura del proyecto
```
SupportApi/
├── Controllers/         # Controladores de API REST
├── Models/              # Modelos de datos (Usuario, Reclamo, Respuesta)
├── DTOs/                # Data Transfer Objects para validaciones y respuestas
├── Data/                # DbContext y configuración de EF Core
├── Migrations/          # Migraciones de base de datos
├── Services/            # Servicios (TokenService, lógica de negocio)
├── Validators/          # Validaciones con FluentValidation
├── Mappings/            # Perfiles de AutoMapper
├── Properties/          # launchSettings.json y configuración de inicio
SupportApi.Tests/        # Pruebas unitarias con xUnit
```

## Seguridad y autenticación
- Autenticación JWT para login y autorización de endpoints protegidos.
- Los tokens se generan en `/api/auth/login` y se deben enviar en el header `Authorization: Bearer {token}`.

## Validaciones
- Validaciones automáticas con FluentValidation en DTOs (ejemplo: formato de correo, contraseña requerida, etc).
- Manejo de errores y respuestas claras para datos inválidos.

## Pruebas unitarias
- Pruebas con xUnit en la carpeta `SupportApi.Tests`.
- Ejemplo: validación de email, registro de usuario, lógica de reclamos.

## Dependencias principales
- ASP.NET Core 8
- Entity Framework Core
- AutoMapper
- FluentValidation
- Swashbuckle (Swagger)
- xUnit

## Ejemplo de uso
### Registro de usuario
```json
POST /api/auth/register
{
  "nombre": "Juan",
  "correoElectronico": "juan@mail.com",
  "password": "123456",
  "rol": "Cliente"
}
```
### Login y obtención de token
```json
POST /api/auth/login
{
  "email": "juan@mail.com",
  "password": "123456"
}
```
### Crear reclamo (requiere token)
```json
POST /api/reclamos
Authorization: Bearer {token}
{
  "titulo": "Problema con el producto",
  "descripcion": "No funciona correctamente"
}
```

## Endpoints principales
- `/api/usuarios` - CRUD de usuarios
- `/api/reclamos` - CRUD de reclamos
- `/api/respuestas` - CRUD de respuestas
- `/api/auth/register` - Registro de usuario
- `/api/auth/login` - Login y obtención de token JWT

## Recomendaciones
- Configura la cadena de conexión en `appsettings.json` antes de ejecutar migraciones.
- Usa Swagger UI para probar todos los endpoints y ver la documentación interactiva.
- Ejecuta las pruebas unitarias con:
  ```
  dotnet test
  ```

---

Desarrollado por Santucho12.

## Tecnologías utilizadas
- ASP.NET Core 8
- Entity Framework Core
- SQL Server
- Swagger/OpenAPI

## Ejecución local
1. Clona el repositorio.
2. Configura la cadena de conexión a tu SQL Server en `appsettings.json`.
3. Ejecuta las migraciones con:
   ```
   dotnet ef database update
   ```
4. Inicia la API:
   ```
   dotnet run
   ```
5. Accede a Swagger UI en `http://localhost:5138/` para probar los endpoints.

## Notas
- El sistema maneja relaciones y validaciones de claves foráneas.
- La serialización JSON está configurada para evitar ciclos de referencia.
- Los archivos `appsettings.*.json` están ignorados en el repositorio por seguridad.

---

Desarrollado por Santucho12.
