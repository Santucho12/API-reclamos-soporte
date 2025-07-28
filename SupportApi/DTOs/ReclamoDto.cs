using System;
using System.Collections.Generic;

namespace SupportApi.DTOs
{
    public class ReclamoDto
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Estado { get; set; } = "Abierto";
        public DateTime FechaCreacion { get; set; }
        public Guid UsuarioId { get; set; }
        public string? UsuarioNombre { get; set; } // Opcional, para mostrar el nombre del usuario
        public List<RespuestaDto>? Respuestas { get; set; } // Opcional, para mostrar respuestas
    }

    public class RespuestaDto
    {
        public Guid Id { get; set; }
        public string Contenido { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public Guid UsuarioId { get; set; }
        public string? UsuarioNombre { get; set; }
    }
}
