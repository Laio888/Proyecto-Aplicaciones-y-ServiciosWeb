using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Servicios
{
    public class ProgramaCiService : IProgramaCiService
    {
        private readonly IProgramaCiRepository _repo;

        public ProgramaCiService(IProgramaCiRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<ProgramaCi>> ObtenerTodosAsync()
            => _repo.ObtenerTodosAsync();

        public Task<IEnumerable<ProgramaCi>> ObtenerPorProgramaAsync(int programaId)
            => _repo.ObtenerPorProgramaAsync(programaId);

        public Task<IEnumerable<ProgramaCi>> ObtenerPorCarInnovacionAsync(int carInnovacionId)
            => _repo.ObtenerPorCarInnovacionAsync(carInnovacionId);

        public Task<ProgramaCi?> ObtenerPorIdAsync(int programaId, int carInnovacionId)
            => _repo.ObtenerPorIdAsync(programaId, carInnovacionId);

        public Task<bool> CrearAsync(ProgramaCi programaCi)
            => _repo.InsertarAsync(programaCi);

        public Task<bool> EliminarAsync(int programaId, int carInnovacionId)
            => _repo.EliminarAsync(programaId, carInnovacionId);
    }
}