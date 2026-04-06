using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Repositorios.Abstracciones
{
    public interface IProgramaRepository
    {
        Task<IEnumerable<Programa>> ObtenerTodosAsync();
        Task<Programa?> ObtenerPorIdAsync(int id);
        Task<int> InsertarAsync(Programa programa);
        Task<bool> ActualizarAsync(Programa programa);
        Task<bool> EliminarAsync(int id);
    }
}