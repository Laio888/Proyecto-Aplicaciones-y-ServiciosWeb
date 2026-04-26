using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Servicios.Abstracciones
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioConRoles>> ObtenerTodosAsync();
        Task<UsuarioConRoles?> ObtenerPorIdAsync(int id);
        Task<int> CrearAsync(Usuario usuario, List<int> roles);
        Task<bool> ActualizarAsync(Usuario usuario, List<int> roles);
        Task<bool> EliminarAsync(int id);
        Task<(bool valido, UsuarioConRoles? usuario)> ValidarCredencialesAsync(string email, string password);
    }
}