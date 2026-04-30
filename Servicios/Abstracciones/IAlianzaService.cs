using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Servicios.Abstracciones
{
    public interface IAlianzaService
    {
        Task<IEnumerable<Alianza>> ObtenerTodosAsync();
        Task<Alianza?> ObtenerPorIdAsync(int aliado, int departamento, int docente);
        Task<bool> CrearAsync(Alianza alianza);
        Task<bool> EliminarAsync(int aliado, int departamento, int docente);
    }
}