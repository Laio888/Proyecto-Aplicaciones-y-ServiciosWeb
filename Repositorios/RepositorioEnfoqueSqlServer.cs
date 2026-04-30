using System.Collections.Generic;
using System.Threading.Tasks;
using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;

public class RepositorioEnfoqueSqlServer : IRepositorioEnfoque
{
    public Task<IEnumerable<Enfoque>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Enfoque> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task Add(Enfoque enfoque)
    {
        throw new NotImplementedException();
    }

    public Task Update(Enfoque enfoque)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }
}