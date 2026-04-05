using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Servicios
{
    public class EnfoqueService : IEnfoqueService
    {
        private readonly IEnfoqueRepository _repo;

        public EnfoqueService(IEnfoqueRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Enfoque>> ListarAsync()
            => await _repo.ObtenerTodosAsync();

        public async Task<Enfoque?> ObtenerPorIdAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("El ID debe ser mayor a 0.");
            return await _repo.ObtenerPorIdAsync(id);
        }

        public async Task<int> CrearAsync(Enfoque enfoque)
        {
            if (string.IsNullOrWhiteSpace(enfoque.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            // ID automático
            var todos = await _repo.ObtenerTodosAsync();
            enfoque.Id = todos.Any() ? todos.Max(x => x.Id) + 1 : 1;

            enfoque.Nombre = enfoque.Nombre.Trim();
            return await _repo.InsertarAsync(enfoque);
        }

        public async Task<bool> ActualizarAsync(Enfoque enfoque)
        {
            if (enfoque.Id <= 0) throw new ArgumentException("ID inválido.");
            if (string.IsNullOrWhiteSpace(enfoque.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");
            return await _repo.ActualizarAsync(enfoque);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("El ID debe ser mayor a 0.");
            return await _repo.EliminarAsync(id);
        }
    }
}