using System;
using System.Collections.Generic;

namespace SupportApi.Models
{
    public class Reclamo
    {
        public Guid Id { get; set; }  // Identificador único

        public string Titulo { get; set; } = null!;  // Título del reclamo

        public string Descripcion { get; set; } = null!;  // Descripción detallada

        public string Estado { get; set; } = "Abierto";  // Estado: Abierto, EnProceso, Cerrado

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;  // Fecha de creación

        // FK al usuario que creó el reclamo
        public Guid UsuarioId { get; set; }

        // Navegación hacia el usuario creador
        public Usuario? Usuario { get; set; }

        // Un reclamo puede tener muchas respuestas
        public ICollection<Respuesta> Respuestas { get; set; } = new List<Respuesta>();
    }
}
