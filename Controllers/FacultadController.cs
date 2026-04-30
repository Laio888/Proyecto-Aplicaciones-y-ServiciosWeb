using Microsoft.AspNetCore.Mvc;
using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Servicios.Abstracciones;
using Microsoft.AspNetCore.Authorization;

namespace ApiKnowledgeMap.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FacultadController : ControllerBase
    {
        private readonly IFacultadService _service;

        public FacultadController(IFacultadService service)
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
        public async Task<IActionResult> Crear([FromBody] Facultad facultad)
        {
            var id = await _service.CrearAsync(facultad);
            return CreatedAtAction(nameof(ObtenerPorId), new { id }, facultad);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] Facultad facultad)
        {
            facultad.Id = id;
            var ok = await _service.ActualizarAsync(facultad);
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