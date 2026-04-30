using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiKnowledgeMap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _repo;

        public UsuarioController(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await _repo.ObtenerTodosAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
            => Ok(await _repo.ObtenerPorIdAsync(id));

        [HttpPut]
        public async Task<IActionResult> Put(Usuario usuario)
            => Ok(await _repo.ActualizarAsync(usuario));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            => Ok(await _repo.EliminarAsync(id));
    }
}