using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Repositorios.Abstracciones
{
    public interface IAspectoNormativoRepository
    {
        Task<IEnumerable<AspectoNormativo>> ObtenerTodosAsync();
        Task<AspectoNormativo?> ObtenerPorIdAsync(int id);
        Task<int> InsertarAsync(AspectoNormativo AspectoNormativo);
        Task<bool> ActualizarAsync(AspectoNormativo AspectoNormativo);
        Task<bool> EliminarAsync(int id);
    }
}