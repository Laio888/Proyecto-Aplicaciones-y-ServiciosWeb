using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Servicios.Abstracciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiKnowledgeMap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrador")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var datos = await _service.ObtenerTodosAsync();
            return Ok(datos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var dato = await _service.ObtenerPorIdAsync(id);
            if (dato is null) return NotFound();
            return Ok(dato);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] UsuarioCreateRequest request)
        {
            var id = await _service.CrearAsync(request.Usuario, request.Roles);
            return Ok(new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id,
            [FromBody] UsuarioCreateRequest request)
        {
            request.Usuario.Id = id;
            var resultado = await _service.ActualizarAsync(request.Usuario, request.Roles);
            if (!resultado) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var resultado = await _service.EliminarAsync(id);
            if (!resultado) return NotFound();
            return NoContent();
        }
    }

    public class UsuarioCreateRequest
    {
        public Usuario Usuario { get; set; } = new();
        public List<int> Roles { get; set; } = new();
    }
}