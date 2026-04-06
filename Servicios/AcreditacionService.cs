using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Servicios
{
    public class AcreditacionService : IAcreditacionService
    {
        private readonly IAcreditacionRepository _repo;

        public AcreditacionService(IAcreditacionRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Acreditacion>> ObtenerTodosAsync()
            => _repo.ObtenerTodosAsync();

        public Task<Acreditacion?> ObtenerPorIdAsync(int id)
            => _repo.ObtenerPorIdAsync(id);

        public Task<int> CrearAsync(Acreditacion acreditacion)
            => _repo.InsertarAsync(acreditacion);

        public Task<bool> ActualizarAsync(Acreditacion acreditacion)
            => _repo.ActualizarAsync(acreditacion);

        public Task<bool> EliminarAsync(int id)
            => _repo.EliminarAsync(id);
    }
}