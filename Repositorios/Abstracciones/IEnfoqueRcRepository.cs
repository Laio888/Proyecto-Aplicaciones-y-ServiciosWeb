using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Repositorios.Abstracciones
{
    public interface IEnfoqueRcRepository
    {
        Task<IEnumerable<EnfoqueRc>> ObtenerTodosAsync();
        Task<EnfoqueRc?> ObtenerPorIdAsync(int enfoque, int registroCalificado);
        Task<bool> InsertarAsync(EnfoqueRc item);
        Task<bool> ActualizarAsync(EnfoqueRc item);
        Task<bool> EliminarAsync(int enfoque, int registroCalificado);
    }
}