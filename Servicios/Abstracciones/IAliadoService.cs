using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Servicios.Abstracciones
{
    public interface IAliadoService
    {
        Task<IEnumerable<Aliado>> ObtenerTodosAsync();
        Task<Aliado?> ObtenerPorIdAsync(int nit);
        Task<bool> CrearAsync(Aliado aliado);
        Task<bool> ActualizarAsync(int nit, Aliado aliado);
        Task<bool> EliminarAsync(int nit);
    }
}