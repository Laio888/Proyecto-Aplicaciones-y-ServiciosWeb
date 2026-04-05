using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Servicios
{
    public class PracticaEstrategiaService : IPracticaEstrategiaService
    {
        private readonly IPracticaEstrategiaRepository _repo;

        public PracticaEstrategiaService(IPracticaEstrategiaRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<PracticaEstrategia>> ListarAsync()
            => await _repo.ObtenerTodosAsync();

        public async Task<PracticaEstrategia?> ObtenerPorIdAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("El ID debe ser mayor a 0.");
            return await _repo.ObtenerPorIdAsync(id);
        }

        public async Task<int> CrearAsync(PracticaEstrategia practica)
        {
            if (string.IsNullOrWhiteSpace(practica.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");
            if (string.IsNullOrWhiteSpace(practica.Tipo))
                throw new ArgumentException("El tipo es obligatorio.");

            var todos = await _repo.ObtenerTodosAsync();
            practica.Id = todos.Any() ? todos.Max(x => x.Id) + 1 : 1;

            practica.Nombre = practica.Nombre.Trim();
            practica.Tipo = practica.Tipo.Trim();
            return await _repo.InsertarAsync(practica);
        }

        public async Task<bool> ActualizarAsync(PracticaEstrategia practica)
        {
            if (practica.Id <= 0) throw new ArgumentException("ID inválido.");
            if (string.IsNullOrWhiteSpace(practica.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");
            return await _repo.ActualizarAsync(practica);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("El ID debe ser mayor a 0.");
            return await _repo.EliminarAsync(id);
        }
    }
}