using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Servicios.Abstracciones;
using Microsoft.AspNetCore.Mvc;

namespace ApiKnowledgeMap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PremioController : ControllerBase
    {
        private readonly IPremioService _service;

        public PremioController(IPremioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var datos = await _service.ObtenerTodosAsync();
            return Ok(datos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var dato = await _service.ObtenerPorIdAsync(id);
            if (dato is null) return NotFound();
            return Ok(dato);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Premio premio)
        {
            var id = await _service.CrearAsync(premio);
            return CreatedAtAction(nameof(ObtenerPorId), new { id }, premio);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] Premio premio)
        {
            if (id != premio.Id) return BadRequest("El id no coincide.");
            var resultado = await _service.ActualizarAsync(premio);
            if (!resultado) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var resultado = await _service.EliminarAsync(id);
            if (!resultado) return NotFound();
            return NoContent();
        }
    }
}