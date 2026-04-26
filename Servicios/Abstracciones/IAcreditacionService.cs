using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Servicios.Abstracciones
{
    public interface IAcreditacionService
    {
        Task<IEnumerable<Acreditacion>> ObtenerTodosAsync();
        Task<Acreditacion?> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(Acreditacion acreditacion);
        Task<bool> ActualizarAsync(Acreditacion acreditacion);
        Task<bool> EliminarAsync(int id);
    }
}