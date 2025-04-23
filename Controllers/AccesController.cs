using MicroService_NaceTuIdea.Context;
using MicroService_NaceTuIdea.Dtos;
using MicroService_NaceTuIdea.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BCrypt.Net;
using System.Text;

namespace MicroService_NaceTuIdea.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccesController : ControllerBase
    {
        private readonly AppDbcontext _context;
        private readonly IConfiguration _config;

        public AccesController(AppDbcontext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            // Verificar si el usuario ya existe
            if (await _context.Users.AnyAsync(u => u.email == request.Email))
            {
                return BadRequest("El usuario ya existe.");
            }

            // Crear un nuevo usuario
            var user = new Users
            {
                id = Guid.NewGuid(),
                name = request.Name,
                email = request.Email,
                password = BCrypt.Net.BCrypt.HashPassword(request.Password), // Asegúrate de hashear la contraseña
                phone = request.Phone,
                registro = DateTime.Now,
                propietario = request.HasProperty
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Usuario registrado exitosamente." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            // Buscar el usuario por email
            var user = await _context.Users.SingleOrDefaultAsync(u => u.email == request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.password))
            {
                return Unauthorized("Credenciales inválidas.");
            }

            // Generar el token JWT
            var token = GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(Users user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
            new Claim(ClaimTypes.Name, user.name),
            new Claim(ClaimTypes.Email, user.email),
            new Claim("IsAdmin", user.propietario.ToString()) // Si necesitas verificar si es propietario
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
