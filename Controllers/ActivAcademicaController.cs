using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivAcademicaController : ControllerBase
    {
        private readonly IActivAcademicaService _service;

        public ActivAcademicaController(IActivAcademicaService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ObtenerTodos()
            => Ok(await _service.ObtenerTodosAsync());

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var item = await _service.ObtenerPorIdAsync(id);
            return item is null ? NotFound() : Ok(item);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Crear([FromBody] ActivAcademica activAcademica)
        {
            var id = await _service.CrearAsync(activAcademica);
            return CreatedAtAction(nameof(ObtenerPorId), new { id }, activAcademica);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ActivAcademica activAcademica)
        {
            activAcademica.Id = id;
            var ok = await _service.ActualizarAsync(activAcademica);
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