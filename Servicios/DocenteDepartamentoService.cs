using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Servicios
{
    public class DocenteDepartamentoService : IDocenteDepartamentoService
    {
        private readonly IDocenteDepartamentoRepository _repo;

        public DocenteDepartamentoService(IDocenteDepartamentoRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<DocenteDepartamento>> ObtenerTodosAsync()
            => _repo.ObtenerTodosAsync();

        public Task<DocenteDepartamento?> ObtenerPorIdAsync(int docente, int departamento)
            => _repo.ObtenerPorIdAsync(docente, departamento);

        public Task<bool> CrearAsync(DocenteDepartamento item)
            => _repo.InsertarAsync(item);

        public Task<bool> EliminarAsync(int docente, int departamento)
            => _repo.EliminarAsync(docente, departamento);
    }
}