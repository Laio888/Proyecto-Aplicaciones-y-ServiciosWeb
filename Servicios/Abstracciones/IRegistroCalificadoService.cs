using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Servicios.Abstracciones
{
    public interface IRegistroCalificadoService
    {
        Task<IEnumerable<RegistroCalificado>> ListarAsync();
        Task<RegistroCalificado?> ObtenerPorIdAsync(int codigo);
        Task<int> CrearAsync(RegistroCalificado item);
        Task<bool> ActualizarAsync(RegistroCalificado item);
        Task<bool> EliminarAsync(int codigo);
    }
}