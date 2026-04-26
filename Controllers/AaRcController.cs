using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Servicios.Abstracciones;
using Microsoft.AspNetCore.Mvc;

namespace ApiKnowledgeMap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AaRcController : ControllerBase
    {
        private readonly IAaRcService _servicio;

        public AaRcController(IAaRcService servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
            => Ok(await _servicio.ListarAsync());

        [HttpGet("{activAcademicasIdcurso:int}/{registroCalificadoCodigo:int}")]
        public async Task<IActionResult> ObtenerPorId(int activAcademicasIdcurso, int registroCalificadoCodigo)
        {
            var item = await _servicio.ObtenerPorIdAsync(activAcademicasIdcurso, registroCalificadoCodigo);
            return item is null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] AaRc item)
        {
            var creado = await _servicio.CrearAsync(item);

            if (!creado)
                return BadRequest("No se pudo crear el registro.");

            return CreatedAtAction(
                nameof(ObtenerPorId),
                new
                {
                    activAcademicasIdcurso = item.ActivAcademicasIdcurso,
                    registroCalificadoCodigo = item.RegistroCalificadoCodigo
                },
                item
            );
        }

        [HttpPut("{activAcademicasIdcurso:int}/{registroCalificadoCodigo:int}")]
        public async Task<IActionResult> Actualizar(
            int activAcademicasIdcurso,
            int registroCalificadoCodigo,
            [FromBody] AaRc item)
        {
            item.ActivAcademicasIdcurso = activAcademicasIdcurso;
            item.RegistroCalificadoCodigo = registroCalificadoCodigo;

            var actualizado = await _servicio.ActualizarAsync(item);
            return actualizado ? NoContent() : NotFound();
        }

        [HttpDelete("{activAcademicasIdcurso:int}/{registroCalificadoCodigo:int}")]
        public async Task<IActionResult> Eliminar(int activAcademicasIdcurso, int registroCalificadoCodigo)
        {
            var eliminado = await _servicio.EliminarAsync(activAcademicasIdcurso, registroCalificadoCodigo);
            return eliminado ? NoContent() : NotFound();
        }

    }
}