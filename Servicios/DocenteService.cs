using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Servicios
{
    public class DocenteService : IDocenteService
    {
        private readonly IDocenteRepository _repo;

        public DocenteService(IDocenteRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Docente>> ObtenerTodosAsync()
            => _repo.ObtenerTodosAsync();

        public Task<Docente?> ObtenerPorIdAsync(int cedula)
            => _repo.ObtenerPorIdAsync(cedula);

        public Task<bool> CrearAsync(Docente docente)
            => _repo.InsertarAsync(docente);

        public Task<bool> ActualizarAsync(int cedula, Docente docente)
            => _repo.ActualizarAsync(cedula, docente);

        public Task<bool> EliminarAsync(int cedula)
            => _repo.EliminarAsync(cedula);
    }
}