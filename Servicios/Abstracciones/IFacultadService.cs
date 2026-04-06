using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Servicios.Abstracciones
{
    public interface IFacultadService
    {
        Task<IEnumerable<Facultad>> ListarAsync();
        Task<Facultad?> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(Facultad Facultad);
        Task<bool> ActualizarAsync(Facultad Facultad);
        Task<bool> EliminarAsync(int id);
    }
}