using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Servicios.Abstracciones
{
    public interface IProgramaService
    {
        Task<IEnumerable<Programa>> ObtenerTodosAsync();
        Task<Programa?> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(Programa programa);
        Task<bool> ActualizarAsync(Programa programa);
        Task<bool> EliminarAsync(int id);
    }
}