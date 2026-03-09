
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiKnowledgeMap.Modelos;

namespace ApiKnowledgeMap.Repositorios.Abstracciones
{

    public interface IRepositorioEnfoque
    {
        
    Task<IEnumerable<Enfoque>> GetAll();
    Task<Enfoque> GetById(int id);
    Task Add(Enfoque nombre);
    Task Update(Enfoque nombre);
    Task Delete(int id);

    }
}
