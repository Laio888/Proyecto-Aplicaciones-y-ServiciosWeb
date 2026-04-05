using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Repositorios.Abstracciones
{
    public interface IEnfoqueRepository
    {
        Task<IEnumerable<Enfoque>> ObtenerTodosAsync();
        Task<Enfoque?> ObtenerPorIdAsync(int id);
        Task<int> InsertarAsync(Enfoque enfoque);
        Task<bool> ActualizarAsync(Enfoque enfoque);
        Task<bool> EliminarAsync(int id);
    }
}