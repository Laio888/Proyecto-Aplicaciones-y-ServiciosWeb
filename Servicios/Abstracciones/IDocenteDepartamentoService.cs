using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Servicios.Abstracciones
{
    public interface IDocenteDepartamentoService
    {
        Task<IEnumerable<DocenteDepartamento>> ObtenerTodosAsync();
        Task<DocenteDepartamento?> ObtenerPorIdAsync(int docente, int departamento);
        Task<bool> CrearAsync(DocenteDepartamento item);
        Task<bool> EliminarAsync(int docente, int departamento);
    }
}