using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Repositorios.Abstracciones
{
    public interface IDocenteDepartamentoRepository
    {
        Task<IEnumerable<DocenteDepartamento>> ObtenerTodosAsync();
        Task<DocenteDepartamento?> ObtenerPorIdAsync(int docente, int departamento);
        Task<bool> InsertarAsync(DocenteDepartamento item);
        Task<bool> EliminarAsync(int docente, int departamento);
    }
}