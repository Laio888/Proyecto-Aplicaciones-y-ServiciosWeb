using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Servicios.Abstracciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiKnowledgeMap.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AreaConocimientoController : ControllerBase
    {
        private readonly IAreaConocimientoService _service;

        public AreaConocimientoController(IAreaConocimientoService service)
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Crear([FromBody] AreaConocimiento areaConocimiento)
        {
            var id = await _service.CrearAsync(areaConocimiento);
            return CreatedAtAction(nameof(ObtenerPorId), new { id }, areaConocimiento);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] AreaConocimiento areaConocimiento)
        {
            if (id != areaConocimiento.Id) return BadRequest("El id no coincide.");
            var resultado = await _service.ActualizarAsync(areaConocimiento);
            if (!resultado) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var resultado = await _service.EliminarAsync(id);
            if (!resultado) return NotFound();
            return NoContent();
        }
    }
}