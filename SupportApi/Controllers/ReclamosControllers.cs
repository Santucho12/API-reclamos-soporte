using SupportApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using SupportApi.Models;
using SupportApi.Data;
using Microsoft.EntityFrameworkCore;

using FluentValidation;
using SupportApi.Validators;
namespace SupportApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReclamosController : ControllerBase
    {
        private readonly SupportDbContext _context;
        private readonly IValidator<ReclamoDto> _validator;
        public ReclamosController(SupportDbContext context, IValidator<ReclamoDto> validator)
        {
            _context = context;
            _validator = validator;
        }

        // GET: api/reclamos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reclamo>>> ObtenerTodos()
        {
            var reclamos = await _context.Reclamos.Include(r => r.Respuestas).ToListAsync();
            return Ok(reclamos);
        }

        // GET: api/reclamos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Reclamo>> ObtenerPorId(Guid id)
        {
            var reclamo = await _context.Reclamos.Include(r => r.Respuestas)
                                .FirstOrDefaultAsync(r => r.Id == id);

            if (reclamo == null)
                return NotFound();
            return Ok(reclamo);
        }
        // POST: api/reclamos
        [HttpPost]
        public async Task<ActionResult<Reclamo>> Crear(Reclamo reclamo)
        {
            reclamo.Id = Guid.NewGuid();
            reclamo.FechaCreacion = DateTime.UtcNow;
            reclamo.Estado = "Abierto";

            _context.Reclamos.Add(reclamo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerPorId), new { id = reclamo.Id }, reclamo);
        }

        // PUT: api/reclamos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(Guid id, Reclamo reclamoActualizado)
        {
            if (id != reclamoActualizado.Id)
                return BadRequest("El ID del reclamo no coincide.");

            var reclamoExistente = await _context.Reclamos.FindAsync(id);
            if (reclamoExistente == null)
                return NotFound();

            // Actualizar campos permitidos
            reclamoExistente.Titulo = reclamoActualizado.Titulo;
            reclamoExistente.Descripcion = reclamoActualizado.Descripcion;
            reclamoExistente.Estado = reclamoActualizado.Estado;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/reclamos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var reclamo = await _context.Reclamos.FindAsync(id);
            if (reclamo == null)
                return NotFound();

            _context.Reclamos.Remove(reclamo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
