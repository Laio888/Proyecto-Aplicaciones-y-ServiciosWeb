using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Repositorios.Abstracciones
{
    public interface IPasantiaRepository
    {
        Task<IEnumerable<Pasantia>> ObtenerTodosAsync();
        Task<Pasantia?> ObtenerPorIdAsync(int id);
        Task<int> InsertarAsync(Pasantia pasantia);
        Task<bool> ActualizarAsync(Pasantia pasantia);
        Task<bool> EliminarAsync(int id);
    }
}