using SupportApi.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SupportApi.Services;
using SupportApi.Models;
using SupportApi.Data;

namespace SupportApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SupportDbContext _context;
        private readonly TokenService _tokenService;
        private readonly PasswordHasher<Usuario> _passwordHasher;

        public AuthController(SupportDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
            _passwordHasher = new PasswordHasher<Usuario>();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UsuarioCreateDto dto)
        {
            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Nombre = dto.Nombre,
                CorreoElectronico = dto.CorreoElectronico,
                Rol = dto.Rol,
                HashContrasena = _passwordHasher.HashPassword(null, dto.Password)
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Usuario registrado con éxito" });
        }

        [HttpPost("login")]
        public IActionResult Login(UsuarioLoginDto dto)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.CorreoElectronico == dto.Email);
            if (usuario == null)
                return Unauthorized("Usuario o contraseña incorrectos");

            var result = _passwordHasher.VerifyHashedPassword(usuario, usuario.HashContrasena, dto.Password);
            if (result == PasswordVerificationResult.Failed)
                return Unauthorized("Usuario o contraseña incorrectos");

            var token = _tokenService.GenerateToken(usuario);
            return Ok(new { Token = token });
        }
    }
}
