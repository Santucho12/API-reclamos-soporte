using System;
using System.Collections.Generic;

namespace SupportApi.Models
{
    public class Usuario
    {
        public Guid Id { get; set; }  // Identificador único

        public string Nombre { get; set; } = null!;  // Nombre completo del usuario

        public string CorreoElectronico { get; set; } = null!;  // Email

        public string HashContrasena { get; set; } = null!;  // Contraseña hasheada

        public string Rol { get; set; } = "Cliente";  // Roles posibles: Cliente, Soporte, Admin

        // Relación: un usuario puede tener muchos reclamos
        public ICollection<Reclamo> Reclamos { get; set; } = new List<Reclamo>();

        // Relación: un usuario puede tener muchas respuestas (agregado para corregir el error)
        public ICollection<Respuesta> Respuestas { get; set; } = new List<Respuesta>();
    }
}
