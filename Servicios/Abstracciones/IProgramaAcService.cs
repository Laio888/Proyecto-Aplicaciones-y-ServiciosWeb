using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Servicios.Abstracciones
{
    public interface IProgramaAcService
    {
        Task<IEnumerable<ProgramaAc>> ObtenerTodosAsync();
        Task<IEnumerable<ProgramaAc>> ObtenerPorProgramaAsync(int programaId);
        Task<IEnumerable<ProgramaAc>> ObtenerPorAreaConocimientoAsync(int areaConocimientoId);
        Task<ProgramaAc?> ObtenerPorIdAsync(int programaId, int areaConocimientoId);
        Task<bool> CrearAsync(ProgramaAc programaAc);
        Task<bool> EliminarAsync(int programaId, int areaConocimientoId);
    }
}