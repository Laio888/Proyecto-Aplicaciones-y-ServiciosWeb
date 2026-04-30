using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Repositorios.Abstracciones
{
    public interface IAliadoRepository
    {
        Task<IEnumerable<Aliado>> ObtenerTodosAsync();
        Task<Aliado?> ObtenerPorIdAsync(int nit);
        Task<bool> InsertarAsync(Aliado aliado);
        Task<bool> ActualizarAsync(int nit, Aliado aliado);
        Task<bool> EliminarAsync(int nit);
    }
}