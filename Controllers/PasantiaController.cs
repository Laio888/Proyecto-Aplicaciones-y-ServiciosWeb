using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Servicios.Abstracciones;
using Microsoft.AspNetCore.Mvc;

namespace ApiKnowledgeMap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PasantiaController : ControllerBase
    {
        private readonly IPasantiaService _service;

        public PasantiaController(IPasantiaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.ObtenerTodosAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _service.ObtenerPorIdAsync(id);
            if (data == null) return NotFound();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Pasantia pasantia)
        {
            var id = await _service.CrearAsync(pasantia);
            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Pasantia pasantia)
        {
            var ok = await _service.ActualizarAsync(pasantia);
            return ok ? Ok() : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.EliminarAsync(id);
            return ok ? Ok() : NotFound();
        }
    }
}