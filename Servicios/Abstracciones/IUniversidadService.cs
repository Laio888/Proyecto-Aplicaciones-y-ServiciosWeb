using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Servicios.Abstracciones
{
    public interface IUniversidadService
    {
        Task<IEnumerable<Universidad>> ListarAsync();
        Task<Universidad?> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(Universidad Universidad);
        Task<bool> ActualizarAsync(Universidad Universidad);
        Task<bool> EliminarAsync(int id);
    }
}