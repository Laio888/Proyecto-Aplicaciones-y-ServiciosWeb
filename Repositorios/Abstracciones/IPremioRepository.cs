using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Repositorios.Abstracciones
{
    public interface IPremioRepository
    {
        Task<IEnumerable<Premio>> ObtenerTodosAsync();
        Task<Premio?> ObtenerPorIdAsync(int id);
        Task<int> InsertarAsync(Premio p);
        Task<bool> ActualizarAsync(Premio p);
        Task<bool> EliminarAsync(int id);
    }
}