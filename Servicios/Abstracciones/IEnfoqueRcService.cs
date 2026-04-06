using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Servicios.Abstracciones
{
    public interface IEnfoqueRcService
    {
        Task<IEnumerable<EnfoqueRc>> ListarAsync();
        Task<EnfoqueRc?> ObtenerPorIdAsync(int enfoque, int registroCalificado);
        Task<bool> CrearAsync(EnfoqueRc item);
        Task<bool> ActualizarAsync(EnfoqueRc item);
        Task<bool> EliminarAsync(int enfoque, int registroCalificado);
    }
}