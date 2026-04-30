using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Dtos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;
using Microsoft.IdentityModel.Tokens;

namespace ApiKnowledgeMap.Servicios
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;

        public AuthService(IAuthRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        public async Task<string> RegistrarAsync(RegistroDto dto)
        {
            var existente = await _repo.ObtenerPorEmailAsync(dto.Email);

            if (existente != null)
                throw new Exception("El correo ya existe");

            var hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var usuario = new Usuario
            {
                Id = new Random().Next(1000, 99999),
                Nombre = dto.Nombre.Trim(),
                Email = dto.Email.Trim(),
                PasswordHash = hash,
                RolId = 2
            };

            await _repo.CrearUsuarioAsync(usuario);

            return "Usuario registrado correctamente";
        }

        public async Task<string?> LoginAsync(LoginDto dto)
        {
            var usuario = await _repo.ObtenerPorEmailAsync(dto.Email);

            if (usuario == null)
                return null;

            bool passwordValido = BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash);

            if (!passwordValido)
                return null;

            return GenerarToken(usuario);
        }

        private string GenerarToken(Usuario usuario)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

            var credenciales = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.RolNombre)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credenciales);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}