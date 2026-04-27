using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Servicios.Abstracciones;
using Microsoft.AspNetCore.Mvc;

namespace ApiKnowledgeMap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocenteDepartamentoController : ControllerBase
    {
        private readonly IDocenteDepartamentoService _service;

        public DocenteDepartamentoController(IDocenteDepartamentoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var datos = await _service.ObtenerTodosAsync();
            return Ok(datos);
        }

        [HttpGet("{docente}/{departamento}")]
        public async Task<IActionResult> ObtenerPorId(int docente, int departamento)
        {
            var dato = await _service.ObtenerPorIdAsync(docente, departamento);
            if (dato is null) return NotFound();

            return Ok(dato);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] DocenteDepartamento item)
        {
            var resultado = await _service.CrearAsync(item);
            if (!resultado) return BadRequest("No se pudo insertar el registro.");

            return Ok(item);
        }

        [HttpDelete("{docente}/{departamento}")]
        public async Task<IActionResult> Eliminar(int docente, int departamento)
        {
            var resultado = await _service.EliminarAsync(docente, departamento);
            if (!resultado) return NotFound();

            return NoContent();
        }
    }
}