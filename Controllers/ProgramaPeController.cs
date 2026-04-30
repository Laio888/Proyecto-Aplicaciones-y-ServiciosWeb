using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Servicios.Abstracciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiKnowledgeMap.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProgramaPeController : ControllerBase
    {
        private readonly IProgramaPeService _service;

        public ProgramaPeController(IProgramaPeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var datos = await _service.ObtenerTodosAsync();
            return Ok(datos);
        }

        [HttpGet("programa/{programaId}")]
        public async Task<IActionResult> ObtenerPorPrograma(int programaId)
        {
            var datos = await _service.ObtenerPorProgramaAsync(programaId);
            return Ok(datos);
        }

        [HttpGet("practicaestrategia/{practicaEstrategiaId}")]
        public async Task<IActionResult> ObtenerPorPracticaEstrategia(int practicaEstrategiaId)
        {
            var datos = await _service.ObtenerPorPracticaEstrategiaAsync(practicaEstrategiaId);
            return Ok(datos);
        }

        [HttpGet("{programaId}/{practicaEstrategiaId}")]
        public async Task<IActionResult> ObtenerPorId(int programaId, int practicaEstrategiaId)
        {
            var dato = await _service.ObtenerPorIdAsync(programaId, practicaEstrategiaId);
            if (dato is null) return NotFound();
            return Ok(dato);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Crear([FromBody] ProgramaPe programaPe)
        {
            var resultado = await _service.CrearAsync(programaPe);
            if (!resultado) return BadRequest("No se pudo insertar el registro.");
            return CreatedAtAction(
                nameof(ObtenerPorId),
                new { programaId = programaPe.Programa, practicaEstrategiaId = programaPe.PracticaEstrategia },
                programaPe);
        }

        [HttpDelete("{programaId}/{practicaEstrategiaId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Eliminar(int programaId, int practicaEstrategiaId)
        {
            var resultado = await _service.EliminarAsync(programaId, practicaEstrategiaId);
            if (!resultado) return NotFound();
            return NoContent();
        }
    }
}