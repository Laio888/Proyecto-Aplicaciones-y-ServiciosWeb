using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Repositorios.Abstracciones
{
    public interface ICarInnovacionRepository
    {
        Task<IEnumerable<CarInnovacion>> ObtenerTodosAsync();
        Task<CarInnovacion?> ObtenerPorIdAsync(int id);
        Task<int> InsertarAsync(CarInnovacion carInnovacion);
        Task<bool> ActualizarAsync(CarInnovacion carInnovacion);
        Task<bool> EliminarAsync(int id);
    }
}