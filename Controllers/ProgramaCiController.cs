using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Servicios.Abstracciones;
using Microsoft.AspNetCore.Mvc;

namespace ApiKnowledgeMap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgramaCiController : ControllerBase
    {
        private readonly IProgramaCiService _service;

        public ProgramaCiController(IProgramaCiService service)
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

        [HttpGet("carinnovacion/{carInnovacionId}")]
        public async Task<IActionResult> ObtenerPorCarInnovacion(int carInnovacionId)
        {
            var datos = await _service.ObtenerPorCarInnovacionAsync(carInnovacionId);
            return Ok(datos);
        }

        [HttpGet("{programaId}/{carInnovacionId}")]
        public async Task<IActionResult> ObtenerPorId(int programaId, int carInnovacionId)
        {
            var dato = await _service.ObtenerPorIdAsync(programaId, carInnovacionId);
            if (dato is null) return NotFound();
            return Ok(dato);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ProgramaCi programaCi)
        {
            var resultado = await _service.CrearAsync(programaCi);
            if (!resultado) return BadRequest("No se pudo insertar el registro.");
            return CreatedAtAction(
                nameof(ObtenerPorId),
                new { programaId = programaCi.Programa, carInnovacionId = programaCi.CarInnovacion },
                programaCi);
        }

        [HttpDelete("{programaId}/{carInnovacionId}")]
        public async Task<IActionResult> Eliminar(int programaId, int carInnovacionId)
        {
            var resultado = await _service.EliminarAsync(programaId, carInnovacionId);
            if (!resultado) return NotFound();
            return NoContent();
        }
    }
}