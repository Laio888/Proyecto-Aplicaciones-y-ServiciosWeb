using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Servicios
{
    public class AlianzaService : IAlianzaService
    {
        private readonly IAlianzaRepository _repo;

        public AlianzaService(IAlianzaRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Alianza>> ObtenerTodosAsync()
            => _repo.ObtenerTodosAsync();

        public Task<Alianza?> ObtenerPorIdAsync(int aliado, int departamento, int docente)
            => _repo.ObtenerPorIdAsync(aliado, departamento, docente);

        public Task<bool> CrearAsync(Alianza alianza)
            => _repo.InsertarAsync(alianza);

        public Task<bool> EliminarAsync(int aliado, int departamento, int docente)
            => _repo.EliminarAsync(aliado, departamento, docente);
    }
}