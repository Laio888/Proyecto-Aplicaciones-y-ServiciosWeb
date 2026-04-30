using Microsoft.AspNetCore.Mvc;
using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Servicios.Abstracciones;
using Microsoft.AspNetCore.Authorization;

namespace ApiKnowledgeMap.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AcreditacionController : ControllerBase
    {
        private readonly IAcreditacionService _service;

        public AcreditacionController(IAcreditacionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
            => Ok(await _service.ObtenerTodosAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var item = await _service.ObtenerPorIdAsync(id);
            return item is null ? NotFound() : Ok(item);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Crear([FromBody] Acreditacion acreditacion)
        {
            var id = await _service.CrearAsync(acreditacion);
            return CreatedAtAction(nameof(ObtenerPorId), new { id }, acreditacion);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] Acreditacion acreditacion)
        {
            acreditacion.Resolucion = id;
            var ok = await _service.ActualizarAsync(acreditacion);
            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var ok = await _service.EliminarAsync(id);
            return ok ? NoContent() : NotFound();
        }
    }
}