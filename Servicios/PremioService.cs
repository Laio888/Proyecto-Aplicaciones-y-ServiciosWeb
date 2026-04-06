using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Servicios
{
    public class PremioService : IPremioService
    {
        private readonly IPremioRepository _repo;

        public PremioService(IPremioRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Premio>> ObtenerTodosAsync()
            => _repo.ObtenerTodosAsync();

        public Task<Premio?> ObtenerPorIdAsync(int id)
            => _repo.ObtenerPorIdAsync(id);

        public Task<int> CrearAsync(Premio premio)
            => _repo.InsertarAsync(premio);

        public Task<bool> ActualizarAsync(Premio premio)
            => _repo.ActualizarAsync(premio);

        public Task<bool> EliminarAsync(int id)
            => _repo.EliminarAsync(id);
    }
}