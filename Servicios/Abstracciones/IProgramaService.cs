using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Servicios.Abstracciones
{
    public interface IProgramaService
    {
        Task<IEnumerable<Programa>> ListarAsync();
        Task<Programa?> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(Programa Programa);
        Task<bool> ActualizarAsync(Programa Programa);
        Task<bool> EliminarAsync(int id);
    }
}