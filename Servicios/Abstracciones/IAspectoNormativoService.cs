using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Servicios.Abstracciones
{
    public interface IAspectoNormativoService
    {
        Task<IEnumerable<AspectoNormativo>> ListarAsync();
        Task<AspectoNormativo?> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(AspectoNormativo AspectoNormativo);
        Task<bool> ActualizarAsync(AspectoNormativo AspectoNormativo);
        Task<bool> EliminarAsync(int id);
    }
}