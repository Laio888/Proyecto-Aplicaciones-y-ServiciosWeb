using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Repositorios.Abstracciones
{
    public interface IAlianzaRepository
    {
        Task<IEnumerable<Alianza>> ObtenerTodosAsync();
        Task<Alianza?> ObtenerPorIdAsync(int aliado, int departamento, int docente);
        Task<bool> InsertarAsync(Alianza alianza);
        Task<bool> EliminarAsync(int aliado, int departamento, int docente);
    }
}