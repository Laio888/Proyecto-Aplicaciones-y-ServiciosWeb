using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Repositorios.Abstracciones
{
    public interface IAreaConocimientoRepository
    {
        Task<IEnumerable<AreaConocimiento>> ObtenerTodosAsync();
        Task<AreaConocimiento?> ObtenerPorIdAsync(int id);
        Task<int> InsertarAsync(AreaConocimiento a);
        Task<bool> ActualizarAsync(AreaConocimiento a);
        Task<bool> EliminarAsync(int id);
    }
}