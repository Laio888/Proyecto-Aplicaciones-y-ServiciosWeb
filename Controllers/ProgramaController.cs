using Microsoft.AspNetCore.Mvc;
using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Servicios.Abstracciones;
using Microsoft.AspNetCore.Authorization;

namespace ApiKnowledgeMap.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProgramaController : ControllerBase
    {
        private readonly IProgramaService _service;

        public ProgramaController(IProgramaService service)
        {
            _service = service;
        }

        // 🔹 GET: api/programa
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var lista = await _service.ObtenerTodosAsync();
            return Ok(lista);
        }

        // 🔹 GET: api/programa/5
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var item = await _service.ObtenerPorIdAsync(id);
            return item is null ? NotFound() : Ok(item);
        }

        // 🔹 POST: api/programa
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Crear([FromBody] Programa programa)
        {
            var id = await _service.CrearAsync(programa);

            return CreatedAtAction(
                nameof(ObtenerPorId),
                new { id },
                programa
            );
        }

        // 🔹 PUT: api/programa/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] Programa programa)
        {
            programa.Id = id;

            var actualizado = await _service.ActualizarAsync(programa);

            return actualizado ? NoContent() : NotFound();
        }

        // 🔹 DELETE: api/programa/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var eliminado = await _service.EliminarAsync(id);

            return eliminado ? NoContent() : NotFound();
        }
    }
}