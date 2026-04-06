using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Repositorios.Abstracciones
{
    public interface IFacultadRepository
    {
        Task<IEnumerable<Facultad>> ObtenerTodosAsync();
        Task<Facultad?> ObtenerPorIdAsync(int id);
        Task<int> InsertarAsync(Facultad Facultad);
        Task<bool> ActualizarAsync(Facultad Facultad);
        Task<bool> EliminarAsync(int id);
    }
}