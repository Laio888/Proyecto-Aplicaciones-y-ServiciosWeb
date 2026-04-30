using Microsoft.AspNetCore.Mvc;
using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Dtos;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _servicio;

        public AuthController(IAuthService servicio)
        {
            _servicio = servicio;
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registro([FromBody] RegistroDto dto)
        {
            var resultado = await _servicio.RegistrarAsync(dto);
            return Ok(new { mensaje = resultado });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var token = await _servicio.LoginAsync(dto);

            if (token == null)
                return Unauthorized(new { mensaje = "Credenciales inválidas" });

            return Ok(new { token });
        }
    }
}