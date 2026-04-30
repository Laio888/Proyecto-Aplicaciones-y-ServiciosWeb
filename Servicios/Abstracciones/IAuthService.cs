using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Dtos;

namespace ApiKnowledgeMap.Servicios.Abstracciones
{
    public interface IAuthService
    {
        Task<string> RegistrarAsync(RegistroDto dto);

        Task<string?> LoginAsync(LoginDto dto);
    }
}