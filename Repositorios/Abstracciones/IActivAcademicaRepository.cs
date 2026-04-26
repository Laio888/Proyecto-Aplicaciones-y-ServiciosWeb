using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Repositorios.Abstracciones
{
    public interface IActivAcademicaRepository
    {
        Task<IEnumerable<ActivAcademica>> ObtenerTodosAsync();
        Task<ActivAcademica?> ObtenerPorIdAsync(int id);
        Task<int> InsertarAsync(ActivAcademica acreditacion);
        Task<bool> ActualizarAsync(ActivAcademica acreditacion);
        Task<bool> EliminarAsync(int id);
    }
}