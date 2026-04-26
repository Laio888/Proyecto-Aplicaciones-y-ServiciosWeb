using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Repositorios.Abstracciones
{
    public interface IUsuarioRolRepository
    {
        Task<IEnumerable<UsuarioRol>> ObtenerPorUsuarioAsync(int usuarioId);
        Task<bool> InsertarAsync(UsuarioRol ur);
        Task<bool> EliminarAsync(int usuarioId, int rolId);
        Task<bool> EliminarTodosDeUsuarioAsync(int usuarioId);
    }
}