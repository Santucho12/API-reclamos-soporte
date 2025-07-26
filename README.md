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

## Endpoints principales
- `/api/usuarios` - CRUD de usuarios
- `/api/reclamos` - CRUD de reclamos
- `/api/respuestas` - CRUD de respuestas

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
