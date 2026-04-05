using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Servicios.Abstracciones
{
    public interface IPracticaEstrategiaService
    {
        Task<IEnumerable<PracticaEstrategia>> ListarAsync();
        Task<PracticaEstrategia?> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(PracticaEstrategia practica);
        Task<bool> ActualizarAsync(PracticaEstrategia practica);
        Task<bool> EliminarAsync(int id);
    }
}