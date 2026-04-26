using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Servicios
{
    public class PasantiaService : IPasantiaService
    {
        private readonly IPasantiaRepository _repo;

        public PasantiaService(IPasantiaRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Pasantia>> ObtenerTodosAsync()
        {
            return await _repo.ObtenerTodosAsync();
        }

        public async Task<Pasantia?> ObtenerPorIdAsync(int id)
        {
            return await _repo.ObtenerPorIdAsync(id);
        }

        public async Task<int> CrearAsync(Pasantia pasantia)
        {
            return await _repo.InsertarAsync(pasantia);
        }

        public async Task<bool> ActualizarAsync(Pasantia pasantia)
        {
            return await _repo.ActualizarAsync(pasantia);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            return await _repo.EliminarAsync(id);
        }
    }
}