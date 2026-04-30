using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Servicios.Abstracciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiKnowledgeMap.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RegistroCalificadoController : ControllerBase
    {
        private readonly IRegistroCalificadoService _servicio;

        public RegistroCalificadoController(IRegistroCalificadoService servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
            => Ok(await _servicio.ListarAsync());

        [HttpGet("{codigo:int}")]
        public async Task<IActionResult> ObtenerPorId(int codigo)
        {
            var item = await _servicio.ObtenerPorIdAsync(codigo);
            return item is null ? NotFound() : Ok(item);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Crear([FromBody] RegistroCalificado item)
        {
            var nuevoCodigo = await _servicio.CrearAsync(item);

            return CreatedAtAction(
                nameof(ObtenerPorId),
                new { codigo = nuevoCodigo },
                item
            );
        }

        [HttpPut("{codigo:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Actualizar(int codigo, [FromBody] RegistroCalificado item)
        {
            item.Codigo = codigo;
            var actualizado = await _servicio.ActualizarAsync(item);
            return actualizado ? NoContent() : NotFound();
        }

        [HttpDelete("{codigo:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Eliminar(int codigo)
        {
            var eliminado = await _servicio.EliminarAsync(codigo);
            return eliminado ? NoContent() : NotFound();
        }
    }
}