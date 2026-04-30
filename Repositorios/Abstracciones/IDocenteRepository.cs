using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Repositorios.Abstracciones
{
    public interface IDocenteRepository
    {
        Task<IEnumerable<Docente>> ObtenerTodosAsync();
        Task<Docente?> ObtenerPorIdAsync(int cedula);
        Task<bool> InsertarAsync(Docente docente);
        Task<bool> ActualizarAsync(int cedula, Docente docente);
        Task<bool> EliminarAsync(int cedula);
    }
}