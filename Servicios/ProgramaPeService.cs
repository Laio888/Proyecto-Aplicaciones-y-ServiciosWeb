using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Servicios
{
    public class ProgramaPeService : IProgramaPeService
    {
        private readonly IProgramaPeRepository _repo;

        public ProgramaPeService(IProgramaPeRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<ProgramaPe>> ObtenerTodosAsync()
            => _repo.ObtenerTodosAsync();

        public Task<IEnumerable<ProgramaPe>> ObtenerPorProgramaAsync(int programaId)
            => _repo.ObtenerPorProgramaAsync(programaId);

        public Task<IEnumerable<ProgramaPe>> ObtenerPorPracticaEstrategiaAsync(int practicaEstrategiaId)
            => _repo.ObtenerPorPracticaEstrategiaAsync(practicaEstrategiaId);

        public Task<ProgramaPe?> ObtenerPorIdAsync(int programaId, int practicaEstrategiaId)
            => _repo.ObtenerPorIdAsync(programaId, practicaEstrategiaId);

        public Task<bool> CrearAsync(ProgramaPe programaPe)
            => _repo.InsertarAsync(programaPe);

        public Task<bool> EliminarAsync(int programaId, int practicaEstrategiaId)
            => _repo.EliminarAsync(programaId, practicaEstrategiaId);
    }
}