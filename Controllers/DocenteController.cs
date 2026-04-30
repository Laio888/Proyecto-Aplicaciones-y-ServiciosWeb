using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Servicios.Abstracciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiKnowledgeMap.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DocenteController : ControllerBase
    {
        private readonly IDocenteService _service;

        public DocenteController(IDocenteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var datos = await _service.ObtenerTodosAsync();
            return Ok(datos);
        }

        [HttpGet("{cedula}")]
        public async Task<IActionResult> ObtenerPorId(int cedula)
        {
            var dato = await _service.ObtenerPorIdAsync(cedula);
            if (dato is null) return NotFound();

            return Ok(dato);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Crear([FromBody] Docente docente)
        {
            var resultado = await _service.CrearAsync(docente);
            if (!resultado) return BadRequest("No se pudo insertar el registro.");

            return CreatedAtAction(nameof(ObtenerPorId), new { cedula = docente.Cedula }, docente);
        }

        [HttpPut("{cedula}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Actualizar(int cedula, [FromBody] Docente docente)
        {
            var resultado = await _service.ActualizarAsync(cedula, docente);
            if (!resultado) return NotFound();

            return NoContent();
        }

        [HttpDelete("{cedula}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Eliminar(int cedula)
        {
            var resultado = await _service.EliminarAsync(cedula);
            if (!resultado) return NotFound();

            return NoContent();
        }
    }
}