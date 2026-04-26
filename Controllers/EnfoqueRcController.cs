using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Servicios.Abstracciones;
using Microsoft.AspNetCore.Mvc;

namespace ApiKnowledgeMap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnfoqueRcController : ControllerBase
    {
        private readonly IEnfoqueRcService _servicio;

        public EnfoqueRcController(IEnfoqueRcService servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
            => Ok(await _servicio.ListarAsync());

        [HttpGet("{enfoque:int}/{registroCalificado:int}")]
        public async Task<IActionResult> ObtenerPorId(int enfoque, int registroCalificado)
        {
            var item = await _servicio.ObtenerPorIdAsync(enfoque, registroCalificado);
            return item is null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] EnfoqueRc item)
        {
            var creado = await _servicio.CrearAsync(item);

            if (!creado)
                return BadRequest("No se pudo crear el registro.");

            return CreatedAtAction(
                nameof(ObtenerPorId),
                new
                {
                    enfoque = item.Enfoque,
                    registroCalificado = item.RegistroCalificado
                },
                item
            );
        }

        [HttpPut("{enfoque:int}/{registroCalificado:int}")]
        public async Task<IActionResult> Actualizar(
            int enfoque,
            int registroCalificado,
            [FromBody] EnfoqueRc item)
        {
            item.Enfoque = enfoque;
            item.RegistroCalificado = registroCalificado;

            var actualizado = await _servicio.ActualizarAsync(item);
            return actualizado ? NoContent() : NotFound();
        }

        [HttpDelete("{enfoque:int}/{registroCalificado:int}")]
        public async Task<IActionResult> Eliminar(int enfoque, int registroCalificado)
        {
            var eliminado = await _servicio.EliminarAsync(enfoque, registroCalificado);
            return eliminado ? NoContent() : NotFound();
        }
    }
}