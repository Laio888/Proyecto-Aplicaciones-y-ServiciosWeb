using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Repositorios.Abstracciones
{
    public interface IPracticaEstrategiaRepository
    {
        Task<IEnumerable<PracticaEstrategia>> ObtenerTodosAsync();
        Task<PracticaEstrategia?> ObtenerPorIdAsync(int id);
        Task<int> InsertarAsync(PracticaEstrategia practica);
        Task<bool> ActualizarAsync(PracticaEstrategia practica);
        Task<bool> EliminarAsync(int id);
    }
}