using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Servicios
{
    public class AliadoService : IAliadoService
    {
        private readonly IAliadoRepository _repo;

        public AliadoService(IAliadoRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Aliado>> ObtenerTodosAsync()
            => _repo.ObtenerTodosAsync();

        public Task<Aliado?> ObtenerPorIdAsync(int nit)
            => _repo.ObtenerPorIdAsync(nit);

        public Task<bool> CrearAsync(Aliado aliado)
            => _repo.InsertarAsync(aliado);

        public Task<bool> ActualizarAsync(int nit, Aliado aliado)
            => _repo.ActualizarAsync(nit, aliado);

        public Task<bool> EliminarAsync(int nit)
            => _repo.EliminarAsync(nit);
    }
}