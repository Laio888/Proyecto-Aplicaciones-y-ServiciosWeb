using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Repositorios.Abstracciones
{
    public interface IAuthRepository
    {
        Task<Usuario?> ObtenerPorEmailAsync(string email);

        Task<int> CrearUsuarioAsync(Usuario usuario);
    }
}