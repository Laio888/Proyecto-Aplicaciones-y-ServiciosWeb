using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Servicios.Abstracciones
{
    public interface IProgramaPeService
    {
        Task<IEnumerable<ProgramaPe>> ObtenerTodosAsync();
        Task<IEnumerable<ProgramaPe>> ObtenerPorProgramaAsync(int programaId);
        Task<IEnumerable<ProgramaPe>> ObtenerPorPracticaEstrategiaAsync(int practicaEstrategiaId);
        Task<ProgramaPe?> ObtenerPorIdAsync(int programaId, int practicaEstrategiaId);
        Task<bool> CrearAsync(ProgramaPe programaPe);
        Task<bool> EliminarAsync(int programaId, int practicaEstrategiaId);
    }
}