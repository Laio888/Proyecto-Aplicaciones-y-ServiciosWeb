using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Servicios.Abstracciones
{
    public interface IRolService
    {
        Task<IEnumerable<Rol>> ObtenerTodosAsync();
        Task<Rol?> ObtenerPorIdAsync(int id);
        Task<bool> CrearAsync(Rol rol);
        Task<bool> ActualizarAsync(Rol rol);
        Task<bool> EliminarAsync(int id);
    }
}