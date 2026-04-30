using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Repositorios.Abstracciones
{
    public interface IAaRcRepository
    {
        Task<IEnumerable<AaRc>> ObtenerTodosAsync();
        Task<AaRc?> ObtenerPorIdAsync(int activAcademicasIdcurso, int registroCalificadoCodigo);
        Task<bool> InsertarAsync(AaRc item);
        Task<bool> ActualizarAsync(AaRc item);
        Task<bool> EliminarAsync(int activAcademicasIdcurso, int registroCalificadoCodigo);
    }
}