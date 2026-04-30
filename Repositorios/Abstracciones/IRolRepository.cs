using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Repositorios.Abstracciones
{
    public interface IRolRepository
    {
        Task<IEnumerable<Rol>> ObtenerTodosAsync();
        Task<Rol?> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(Rol rol);
        Task<bool> ActualizarAsync(Rol rol);
        Task<bool> EliminarAsync(int id);
    }
}