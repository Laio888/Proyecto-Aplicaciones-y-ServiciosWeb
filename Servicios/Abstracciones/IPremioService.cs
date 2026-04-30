using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Servicios.Abstracciones
{
    public interface IPremioService
    {
        Task<IEnumerable<Premio>> ObtenerTodosAsync();
        Task<Premio?> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(Premio premio);
        Task<bool> ActualizarAsync(Premio premio);
        Task<bool> EliminarAsync(int id);
    }
}