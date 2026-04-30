using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiKnowledgeMap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class RolController : ControllerBase
    {
        private readonly IRolRepository _repo;

        public RolController(IRolRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await _repo.ObtenerTodosAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
            => Ok(await _repo.ObtenerPorIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Post(Rol rol)
            => Ok(await _repo.CrearAsync(rol));

        [HttpPut]
        public async Task<IActionResult> Put(Rol rol)
            => Ok(await _repo.ActualizarAsync(rol));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            => Ok(await _repo.EliminarAsync(id));
    }
}