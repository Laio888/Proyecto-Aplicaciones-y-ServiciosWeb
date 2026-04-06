using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Repositorios.Abstracciones
{
    public interface IProgramaAcRepository
    {
        Task<IEnumerable<ProgramaAc>> ObtenerTodosAsync();
        Task<IEnumerable<ProgramaAc>> ObtenerPorProgramaAsync(int programaId);
        Task<IEnumerable<ProgramaAc>> ObtenerPorAreaConocimientoAsync(int areaConocimientoId);
        Task<ProgramaAc?> ObtenerPorIdAsync(int programaId, int areaConocimientoId);
        Task<bool> InsertarAsync(ProgramaAc p);
        Task<bool> EliminarAsync(int programaId, int areaConocimientoId);
    }
}