using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Servicios.Abstracciones;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace ApiKnowledgeMap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticacionController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ConfiguracionJwt _jwt;

        public AutenticacionController(
            IUsuarioService usuarioService,
            IOptions<ConfiguracionJwt> jwt)
        {
            _usuarioService = usuarioService;
            _jwt = jwt.Value;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Contrasena))
                return BadRequest("Email y contraseña son requeridos.");

            var (valido, usuario) = await _usuarioService
                .ValidarCredencialesAsync(request.Email, request.Contrasena);

            if (!valido || usuario == null)
                return Unauthorized("Credenciales incorrectas.");

            var token = GenerarToken(usuario);

            return Ok(new
            {
                token,
                usuario = usuario.Nombre,
                email = usuario.Email,
                roles = usuario.Roles
            });
        }

        private string GenerarToken(UsuarioConRoles usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Email, usuario.Email)
            };

            // Agregar un claim por cada rol
            foreach (var rol in usuario.Roles)
                claims.Add(new Claim(ClaimTypes.Role, rol));

            var clave = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwt.Key));
            var credenciales = new SigningCredentials(
                clave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DuracionMinutos),
                signingCredentials: credenciales
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = "";
        public string Contrasena { get; set; } = "";
    }
}