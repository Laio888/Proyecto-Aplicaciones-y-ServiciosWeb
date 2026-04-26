using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Repositorios.Abstracciones
{
    public interface IProgramaPeRepository
    {
        Task<IEnumerable<ProgramaPe>> ObtenerTodosAsync();
        Task<IEnumerable<ProgramaPe>> ObtenerPorProgramaAsync(int programaId);
        Task<IEnumerable<ProgramaPe>> ObtenerPorPracticaEstrategiaAsync(int practicaEstrategiaId);
        Task<ProgramaPe?> ObtenerPorIdAsync(int programaId, int practicaEstrategiaId);
        Task<bool> InsertarAsync(ProgramaPe p);
        Task<bool> EliminarAsync(int programaId, int practicaEstrategiaId);
    }
}