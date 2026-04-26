using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Servicios
{
    public class RolService : IRolService
    {
        private readonly IRolRepository _repo;

        public RolService(IRolRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Rol>> ObtenerTodosAsync()
            => _repo.ObtenerTodosAsync();

        public Task<Rol?> ObtenerPorIdAsync(int id)
            => _repo.ObtenerPorIdAsync(id);

        public Task<bool> CrearAsync(Rol rol)
            => _repo.InsertarAsync(rol);

        public Task<bool> ActualizarAsync(Rol rol)
            => _repo.ActualizarAsync(rol);

        public Task<bool> EliminarAsync(int id)
            => _repo.EliminarAsync(id);
    }
}