using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Repositorios.Abstracciones
{
    public interface IRolRepository
    {
        Task<IEnumerable<Rol>> ObtenerTodosAsync();
        Task<Rol?> ObtenerPorIdAsync(int id);
        Task<bool> InsertarAsync(Rol r);
        Task<bool> ActualizarAsync(Rol r);
        Task<bool> EliminarAsync(int id);
    }
}