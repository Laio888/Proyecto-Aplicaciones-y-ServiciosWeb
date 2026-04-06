using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Servicios.Abstracciones
{
    public interface IFacultadService
    {
        Task<IEnumerable<Facultad>> ObtenerTodosAsync();
        Task<Facultad?> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(Facultad facultad);
        Task<bool> ActualizarAsync(Facultad facultad);
        Task<bool> EliminarAsync(int id);
    }
}