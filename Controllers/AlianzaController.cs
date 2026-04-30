using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Servicios.Abstracciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiKnowledgeMap.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AlianzaController : ControllerBase
    {
        private readonly IAlianzaService _service;

        public AlianzaController(IAlianzaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var datos = await _service.ObtenerTodosAsync();
            return Ok(datos);
        }

        [HttpGet("{aliado}/{departamento}/{docente}")]
        public async Task<IActionResult> ObtenerPorId(int aliado, int departamento, int docente)
        {
            var dato = await _service.ObtenerPorIdAsync(aliado, departamento, docente);
            if (dato is null) return NotFound();

            return Ok(dato);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Crear([FromBody] Alianza alianza)
        {
            var resultado = await _service.CrearAsync(alianza);
            if (!resultado) return BadRequest("No se pudo insertar el registro.");

            return Ok(alianza);
        }

        [HttpDelete("{aliado}/{departamento}/{docente}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Eliminar(int aliado, int departamento, int docente)
        {
            var resultado = await _service.EliminarAsync(aliado, departamento, docente);
            if (!resultado) return NotFound();

            return NoContent();
        }
    }
}