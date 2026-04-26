using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Repositorios.Abstracciones
{
    public interface IAcreditacionRepository
    {
        Task<IEnumerable<Acreditacion>> ObtenerTodosAsync();
        Task<Acreditacion?> ObtenerPorIdAsync(int id);
        Task<int> InsertarAsync(Acreditacion acreditacion);
        Task<bool> ActualizarAsync(Acreditacion acreditacion);
        Task<bool> EliminarAsync(int id);
    }
}