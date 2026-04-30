using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Servicios.Abstracciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiKnowledgeMap.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProgramaAcController : ControllerBase
    {
        private readonly IProgramaAcService _service;

        public ProgramaAcController(IProgramaAcService service)
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

        [HttpGet("areaconocimiento/{areaConocimientoId}")]
        public async Task<IActionResult> ObtenerPorAreaConocimiento(int areaConocimientoId)
        {
            var datos = await _service.ObtenerPorAreaConocimientoAsync(areaConocimientoId);
            return Ok(datos);
        }

        [HttpGet("{programaId}/{areaConocimientoId}")]
        public async Task<IActionResult> ObtenerPorId(int programaId, int areaConocimientoId)
        {
            var dato = await _service.ObtenerPorIdAsync(programaId, areaConocimientoId);
            if (dato is null) return NotFound();
            return Ok(dato);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Crear([FromBody] ProgramaAc programaAc)
        {
            var resultado = await _service.CrearAsync(programaAc);
            if (!resultado) return BadRequest("No se pudo insertar el registro.");
            return CreatedAtAction(
                nameof(ObtenerPorId),
                new { programaId = programaAc.Programa, areaConocimientoId = programaAc.AreaConocimiento },
                programaAc);
        }

        [HttpDelete("{programaId}/{areaConocimientoId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Eliminar(int programaId, int areaConocimientoId)
        {
            var resultado = await _service.EliminarAsync(programaId, areaConocimientoId);
            if (!resultado) return NotFound();
            return NoContent();
        }
    }
}