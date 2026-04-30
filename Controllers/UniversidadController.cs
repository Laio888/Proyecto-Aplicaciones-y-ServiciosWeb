using Microsoft.AspNetCore.Mvc;
using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Servicios.Abstracciones;
using Microsoft.AspNetCore.Authorization;

namespace ApiKnowledgeMap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UniversidadController : ControllerBase
    {
        private readonly IUniversidadService _servicio;

        public UniversidadController(IUniversidadService servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Listar()
            => Ok(await _servicio.ListarAsync());

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var item = await _servicio.ObtenerPorIdAsync(id);
            return item is null ? NotFound() : Ok(item);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Crear([FromBody] Universidad Universidad)
        {
            var nuevoId = await _servicio.CrearAsync(Universidad);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevoId }, Universidad);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] Universidad Universidad)
        {
            Universidad.Id = id;
            var actualizado = await _servicio.ActualizarAsync(Universidad);
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