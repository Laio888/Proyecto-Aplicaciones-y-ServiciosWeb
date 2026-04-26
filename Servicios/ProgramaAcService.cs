using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Servicios
{
    public class ProgramaAcService : IProgramaAcService
    {
        private readonly IProgramaAcRepository _repo;

        public ProgramaAcService(IProgramaAcRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<ProgramaAc>> ObtenerTodosAsync()
            => _repo.ObtenerTodosAsync();

        public Task<IEnumerable<ProgramaAc>> ObtenerPorProgramaAsync(int programaId)
            => _repo.ObtenerPorProgramaAsync(programaId);

        public Task<IEnumerable<ProgramaAc>> ObtenerPorAreaConocimientoAsync(int areaConocimientoId)
            => _repo.ObtenerPorAreaConocimientoAsync(areaConocimientoId);

        public Task<ProgramaAc?> ObtenerPorIdAsync(int programaId, int areaConocimientoId)
            => _repo.ObtenerPorIdAsync(programaId, areaConocimientoId);

        public Task<bool> CrearAsync(ProgramaAc programaAc)
            => _repo.InsertarAsync(programaAc);

        public Task<bool> EliminarAsync(int programaId, int areaConocimientoId)
            => _repo.EliminarAsync(programaId, areaConocimientoId);
    }
}