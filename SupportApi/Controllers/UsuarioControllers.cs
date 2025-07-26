using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupportApi.Data;
using SupportApi.Models;

namespace SupportApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly SupportDbContext _context;

        public UsuariosController(SupportDbContext context)
        {
            _context = context;
        }

        // GET: api/usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios
                .AsNoTracking()
                .ToListAsync();
        }

        // GET: api/usuarios/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Usuario>> GetUsuario(Guid id)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Reclamos)
                .Include(u => u.Respuestas)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null) return NotFound();

            return usuario;
        }

        // POST: api/usuarios
        [HttpPost]
        public async Task<ActionResult<Usuario>> CreateUsuario(Usuario usuario)
        {
            // TODO: cuando implementemos Auth, NO aceptaremos HashContrasena directamente,
            //       sino que recibiremos una contraseña en texto plano y la hashearemos.
            usuario.Id = Guid.NewGuid();

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
        }

        // PUT: api/usuarios/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUsuario(Guid id, Usuario usuario)
        {
            if (id != usuario.Id) return BadRequest("El id del path no coincide con el del cuerpo.");

            var exists = await _context.Usuarios.AnyAsync(u => u.Id == id);
            if (!exists) return NotFound();

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Usuarios.AnyAsync(u => u.Id == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/usuarios/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUsuario(Guid id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
