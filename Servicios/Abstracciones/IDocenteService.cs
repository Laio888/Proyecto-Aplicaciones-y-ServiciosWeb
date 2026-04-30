using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Servicios.Abstracciones
{
    public interface IDocenteService
    {
        Task<IEnumerable<Docente>> ObtenerTodosAsync();
        Task<Docente?> ObtenerPorIdAsync(int cedula);
        Task<bool> CrearAsync(Docente docente);
        Task<bool> ActualizarAsync(int cedula, Docente docente);
        Task<bool> EliminarAsync(int cedula);
    }
}