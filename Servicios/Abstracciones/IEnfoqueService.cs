using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Servicios.Abstracciones
{
    public interface IEnfoqueService
    {
        Task<IEnumerable<Enfoque>> ListarAsync();
        Task<Enfoque?> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(Enfoque enfoque);
        Task<bool> ActualizarAsync(Enfoque enfoque);
        Task<bool> EliminarAsync(int id);
    }
}