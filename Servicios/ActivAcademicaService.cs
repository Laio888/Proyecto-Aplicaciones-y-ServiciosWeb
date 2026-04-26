using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Servicios
{
    public class ActivAcademicaService : IActivAcademicaService
    {
        private readonly IActivAcademicaRepository _repo;

        public ActivAcademicaService(IActivAcademicaRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<ActivAcademica>> ObtenerTodosAsync()
            => _repo.ObtenerTodosAsync();

        public Task<ActivAcademica?> ObtenerPorIdAsync(int id)
            => _repo.ObtenerPorIdAsync(id);

        public Task<int> CrearAsync(ActivAcademica activAcademica)
            => _repo.InsertarAsync(activAcademica);

        public Task<bool> ActualizarAsync(ActivAcademica activAcademica)
            => _repo.ActualizarAsync(activAcademica);

        public Task<bool> EliminarAsync(int id)
            => _repo.EliminarAsync(id);
    }
}