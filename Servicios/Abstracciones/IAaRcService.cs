using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Servicios.Abstracciones
{
    public interface IAaRcService
    {
        Task<IEnumerable<AaRc>> ListarAsync();
        Task<AaRc?> ObtenerPorIdAsync(int activAcademicasIdcurso, int registroCalificadoCodigo);
        Task<bool> CrearAsync(AaRc item);
        Task<bool> ActualizarAsync(AaRc item);
        Task<bool> EliminarAsync(int activAcademicasIdcurso, int registroCalificadoCodigo);
    }
}