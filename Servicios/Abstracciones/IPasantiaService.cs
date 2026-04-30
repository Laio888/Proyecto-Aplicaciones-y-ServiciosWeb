using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Servicios.Abstracciones
{
    public interface IPasantiaService
    {
        Task<IEnumerable<Pasantia>> ObtenerTodosAsync();
        Task<Pasantia?> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(Pasantia pasantia);
        Task<bool> ActualizarAsync(Pasantia pasantia);
        Task<bool> EliminarAsync(int id);
    }
}