using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupportApi.Data;
using SupportApi.Models;

namespace SupportApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RespuestasController : ControllerBase
    {
        private readonly SupportDbContext _context;

        public RespuestasController(SupportDbContext context)
        {
            _context = context;
        }

        // GET: api/respuestas
        // Permite opcionalmente filtrar por ReclamoId: /api/respuestas?reclamoId=...
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Respuesta>>> GetRespuestas([FromQuery] Guid? reclamoId)
        {
            var query = _context.Respuestas
                .Include(r => r.Usuario)
                .Include(r => r.Reclamo)
                .AsQueryable();

            if (reclamoId.HasValue)
                query = query.Where(r => r.ReclamoId == reclamoId.Value);

            return await query.AsNoTracking().ToListAsync();
        }

        // GET: api/respuestas/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Respuesta>> GetRespuesta(Guid id)
        {
            var respuesta = await _context.Respuestas
                .Include(r => r.Usuario)
                .Include(r => r.Reclamo)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (respuesta == null) return NotFound();

            return respuesta;
        }

        // POST: api/respuestas
        [HttpPost]
        public async Task<ActionResult<Respuesta>> CreateRespuesta(Respuesta respuesta)
        {
            // Validaciones mínimas para evitar FK inválidas
            var reclamoExists = await _context.Reclamos.AnyAsync(r => r.Id == respuesta.ReclamoId);
            if (!reclamoExists) return BadRequest("El ReclamoId no existe.");

            var usuarioExists = await _context.Usuarios.AnyAsync(u => u.Id == respuesta.UsuarioId);
            if (!usuarioExists) return BadRequest("El UsuarioId no existe.");

            respuesta.Id = Guid.NewGuid();
            respuesta.FechaRespuesta = DateTime.UtcNow;

            _context.Respuestas.Add(respuesta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRespuesta), new { id = respuesta.Id }, respuesta);
        }

        // PUT: api/respuestas/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateRespuesta(Guid id, Respuesta respuesta)
        {
            if (id != respuesta.Id) return BadRequest("El id del path no coincide con el del cuerpo.");

            var exists = await _context.Respuestas.AnyAsync(r => r.Id == id);
            if (!exists) return NotFound();

            // Opcional: bloquear cambios de ReclamoId/UsuarioId si no querés permitirlos
            _context.Entry(respuesta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Respuestas.AnyAsync(r => r.Id == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/respuestas/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteRespuesta(Guid id)
        {
            var respuesta = await _context.Respuestas.FindAsync(id);
            if (respuesta == null) return NotFound();

            _context.Respuestas.Remove(respuesta);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
