using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Repositorios.Abstracciones
{
    public interface IUniversidadRepository
    {
        Task<IEnumerable<Universidad>> ObtenerTodosAsync();
        Task<Universidad?> ObtenerPorIdAsync(int id);
        Task<int> InsertarAsync(Universidad Universidad);
        Task<bool> ActualizarAsync(Universidad Universidad);
        Task<bool> EliminarAsync(int id);
    }
}