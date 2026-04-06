using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Servicios
{
    public class FacultadService : IFacultadService
    {
        private readonly IFacultadRepository _repo;

        public FacultadService(IFacultadRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Facultad>> ObtenerTodosAsync()
            => _repo.ObtenerTodosAsync();

        public Task<Facultad?> ObtenerPorIdAsync(int id)
            => _repo.ObtenerPorIdAsync(id);

        public Task<int> CrearAsync(Facultad facultad)
            => _repo.InsertarAsync(facultad);

        public Task<bool> ActualizarAsync(Facultad facultad)
            => _repo.ActualizarAsync(facultad);

        public Task<bool> EliminarAsync(int id)
            => _repo.EliminarAsync(id);
    }
}