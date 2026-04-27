using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Servicios.Abstracciones;
using Microsoft.AspNetCore.Mvc;

namespace ApiKnowledgeMap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AliadoController : ControllerBase
    {
        private readonly IAliadoService _service;

        public AliadoController(IAliadoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var datos = await _service.ObtenerTodosAsync();
            return Ok(datos);
        }

        [HttpGet("{nit}")]
        public async Task<IActionResult> ObtenerPorId(int nit)
        {
            var dato = await _service.ObtenerPorIdAsync(nit);
            if (dato is null) return NotFound();

            return Ok(dato);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Aliado aliado)
        {
            var resultado = await _service.CrearAsync(aliado);
            if (!resultado) return BadRequest("No se pudo insertar el registro.");

            return CreatedAtAction(nameof(ObtenerPorId), new { nit = aliado.Nit }, aliado);
        }

        [HttpPut("{nit}")]
        public async Task<IActionResult> Actualizar(int nit, [FromBody] Aliado aliado)
        {
            var resultado = await _service.ActualizarAsync(nit, aliado);
            if (!resultado) return NotFound();

            return NoContent();
        }

        [HttpDelete("{nit}")]
        public async Task<IActionResult> Eliminar(int nit)
        {
            var resultado = await _service.EliminarAsync(nit);
            if (!resultado) return NotFound();

            return NoContent();
        }
    }
}