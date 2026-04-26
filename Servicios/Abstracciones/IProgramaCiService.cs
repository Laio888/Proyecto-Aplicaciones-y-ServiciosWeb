using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Servicios.Abstracciones
{
    public interface IProgramaCiService
    {
        Task<IEnumerable<ProgramaCi>> ObtenerTodosAsync();
        Task<IEnumerable<ProgramaCi>> ObtenerPorProgramaAsync(int programaId);
        Task<IEnumerable<ProgramaCi>> ObtenerPorCarInnovacionAsync(int carInnovacionId);
        Task<ProgramaCi?> ObtenerPorIdAsync(int programaId, int carInnovacionId);
        Task<bool> CrearAsync(ProgramaCi programaCi);
        Task<bool> EliminarAsync(int programaId, int carInnovacionId);
    }
}