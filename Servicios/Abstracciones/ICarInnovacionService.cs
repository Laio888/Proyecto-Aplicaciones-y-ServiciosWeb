using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Servicios.Abstracciones
{
    public interface ICarInnovacionService
    {
        Task<IEnumerable<CarInnovacion>> ListarAsync();
        Task<CarInnovacion?> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(CarInnovacion carInnovacion);
        Task<bool> ActualizarAsync(CarInnovacion carInnovacion);
        Task<bool> EliminarAsync(int id);
    }
}