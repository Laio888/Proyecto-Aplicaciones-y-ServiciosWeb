using Microsoft.AspNetCore.Mvc;
using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacultadController : ControllerBase
    {
        private readonly IFacultadService _servicio;

        public FacultadController(IFacultadService servicio)
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
        public async Task<IActionResult> Crear([FromBody] Facultad Facultad)
        {
            var nuevoId = await _servicio.CrearAsync(Facultad);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevoId }, Facultad);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] Facultad Facultad)
        {
            Facultad.Id = id;
            var actualizado = await _servicio.ActualizarAsync(Facultad);
            return actualizado ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var eliminado = await _servicio.EliminarAsync(id);
            return eliminado ? NoContent() : NotFound();
        }
    }
}