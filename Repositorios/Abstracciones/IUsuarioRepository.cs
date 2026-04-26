using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Repositorios.Abstracciones
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<UsuarioConRoles>> ObtenerTodosAsync();
        Task<UsuarioConRoles?> ObtenerPorIdAsync(int id);
        Task<Usuario?> ObtenerPorEmailAsync(string email);
        Task<int> InsertarAsync(Usuario u);
        Task<bool> ActualizarAsync(Usuario u);
        Task<bool> EliminarAsync(int id);
    }
}