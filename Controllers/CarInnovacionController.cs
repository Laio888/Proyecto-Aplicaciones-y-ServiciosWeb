using Microsoft.AspNetCore.Mvc;
using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Servicios.Abstracciones;
using Microsoft.AspNetCore.Authorization;

namespace ApiKnowledgeMap.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CarInnovacionController : ControllerBase
    {
        private readonly ICarInnovacionService _servicio;

        public CarInnovacionController(ICarInnovacionService servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
            => Ok(await _servicio.ListarAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var item = await _servicio.ObtenerPorIdAsync(id);
            return item is null ? NotFound() : Ok(item);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Crear([FromBody] CarInnovacion carInnovacion)
        {
            var nuevoId = await _servicio.CrearAsync(carInnovacion);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevoId }, carInnovacion);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] CarInnovacion carInnovacion)
        {
            carInnovacion.Id = id;
            var actualizado = await _servicio.ActualizarAsync(carInnovacion);
            return actualizado ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Eliminar(int id)
        {
            var eliminado = await _servicio.EliminarAsync(id);
            return eliminado ? NoContent() : NotFound();
        }
    }
}