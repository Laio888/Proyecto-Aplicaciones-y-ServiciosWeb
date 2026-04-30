using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Repositorios.Abstracciones
{
    public interface IRegistroCalificadoRepository
    {
        Task<IEnumerable<RegistroCalificado>> ObtenerTodosAsync();
        Task<RegistroCalificado?> ObtenerPorIdAsync(int codigo);
        Task<int> InsertarAsync(RegistroCalificado item);
        Task<bool> ActualizarAsync(RegistroCalificado item);
        Task<bool> EliminarAsync(int codigo);
    }
}