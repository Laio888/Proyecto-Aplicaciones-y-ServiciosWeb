using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Servicios
{
    public class AreaConocimientoService : IAreaConocimientoService
    {
        private readonly IAreaConocimientoRepository _repo;

        public AreaConocimientoService(IAreaConocimientoRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<AreaConocimiento>> ObtenerTodosAsync()
            => _repo.ObtenerTodosAsync();

        public Task<AreaConocimiento?> ObtenerPorIdAsync(int id)
            => _repo.ObtenerPorIdAsync(id);

        public Task<int> CrearAsync(AreaConocimiento areaConocimiento)
            => _repo.InsertarAsync(areaConocimiento);

        public Task<bool> ActualizarAsync(AreaConocimiento areaConocimiento)
            => _repo.ActualizarAsync(areaConocimiento);

        public Task<bool> EliminarAsync(int id)
            => _repo.EliminarAsync(id);
    }
}