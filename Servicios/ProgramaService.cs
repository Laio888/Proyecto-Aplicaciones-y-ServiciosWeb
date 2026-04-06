using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Servicios
{
    public class ProgramaService : IProgramaService
    {
        private readonly IProgramaRepository _repo;

        public ProgramaService(IProgramaRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Programa>> ObtenerTodosAsync()
            => _repo.ObtenerTodosAsync();

        public Task<Programa?> ObtenerPorIdAsync(int id)
            => _repo.ObtenerPorIdAsync(id);

        public Task<int> CrearAsync(Programa programa)
            => _repo.InsertarAsync(programa);

        public Task<bool> ActualizarAsync(Programa programa)
            => _repo.ActualizarAsync(programa);

        public Task<bool> EliminarAsync(int id)
            => _repo.EliminarAsync(id);
    }
}