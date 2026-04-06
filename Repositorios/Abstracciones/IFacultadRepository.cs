using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Repositorios.Abstracciones
{
    public interface IFacultadRepository
    {
        Task<IEnumerable<Facultad>> ObtenerTodosAsync();
        Task<Facultad?> ObtenerPorIdAsync(int id);
        Task<int> InsertarAsync(Facultad facultad);
        Task<bool> ActualizarAsync(Facultad facultad);
        Task<bool> EliminarAsync(int id);
    }
}