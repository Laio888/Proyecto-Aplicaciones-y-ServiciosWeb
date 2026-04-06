using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Servicios.Abstracciones
{
    public interface IActivAcademicaService
    {
        Task<IEnumerable<ActivAcademica>> ObtenerTodosAsync();
        Task<ActivAcademica?> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(ActivAcademica activAcademica);
        Task<bool> ActualizarAsync(ActivAcademica activAcademica);
        Task<bool> EliminarAsync(int id);
    }
}