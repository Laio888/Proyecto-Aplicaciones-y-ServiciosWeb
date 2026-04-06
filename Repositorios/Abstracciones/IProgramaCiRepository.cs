using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Repositorios.Abstracciones
{
    public interface IProgramaCiRepository
    {
        Task<IEnumerable<ProgramaCi>> ObtenerTodosAsync();
        Task<IEnumerable<ProgramaCi>> ObtenerPorProgramaAsync(int programaId);
        Task<IEnumerable<ProgramaCi>> ObtenerPorCarInnovacionAsync(int carInnovacionId);
        Task<ProgramaCi?> ObtenerPorIdAsync(int programaId, int carInnovacionId);
        Task<bool> InsertarAsync(ProgramaCi p);
        Task<bool> EliminarAsync(int programaId, int carInnovacionId);
    }
}