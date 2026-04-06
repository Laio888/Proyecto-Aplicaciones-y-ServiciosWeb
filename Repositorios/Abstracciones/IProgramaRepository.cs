using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Repositorios.Abstracciones
{
    public interface IProgramaRepository
    {
        Task<IEnumerable<Programa>> ObtenerTodosAsync();
        Task<Programa?> ObtenerPorIdAsync(int id);
        Task<int> InsertarAsync(Programa Programa);
        Task<bool> ActualizarAsync(Programa Programa);
        Task<bool> EliminarAsync(int id);
    }
}