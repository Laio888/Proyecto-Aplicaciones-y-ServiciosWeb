using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Servicios.Abstracciones
{
    public interface IAreaConocimientoService
    {
        Task<IEnumerable<AreaConocimiento>> ObtenerTodosAsync();
        Task<AreaConocimiento?> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(AreaConocimiento areaConocimiento);
        Task<bool> ActualizarAsync(AreaConocimiento areaConocimiento);
        Task<bool> EliminarAsync(int id);
    }
}