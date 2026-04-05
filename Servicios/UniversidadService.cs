using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Servicios
{
    public class UniversidadService : IUniversidadService
    {
        private readonly IUniversidadRepository _repo;

        public UniversidadService(IUniversidadRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Universidad>> ListarAsync()
            => await _repo.ObtenerTodosAsync();

        public async Task<Universidad?> ObtenerPorIdAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("El ID debe ser mayor a 0.");
            return await _repo.ObtenerPorIdAsync(id);
        }

        public async Task<int> CrearAsync(Universidad Universidad)
        {
            if (string.IsNullOrWhiteSpace(Universidad.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");
            if (string.IsNullOrWhiteSpace(Universidad.Tipo))
                throw new ArgumentException("El tipo es obligatorio.");
            if (string.IsNullOrWhiteSpace(Universidad.Ciudad))
                throw new ArgumentException("La Ciudad es obligatoria");

            // Calcular el siguiente ID automáticamente
            var todos = await _repo.ObtenerTodosAsync();
            Universidad.Id = todos.Any() ? todos.Max(x => x.Id) + 1 : 1;

            Universidad.Nombre = Universidad.Nombre.Trim();
            Universidad.Tipo = Universidad.Tipo.Trim();
            Universidad.Ciudad = Universidad.Ciudad.Trim();
            return await _repo.InsertarAsync(Universidad);
        }

        public async Task<bool> ActualizarAsync(Universidad Universidad)
        {
            if (Universidad.Id <= 0) throw new ArgumentException("ID inválido.");
            if (string.IsNullOrWhiteSpace(Universidad.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");
            if (string.IsNullOrWhiteSpace(Universidad.Tipo))
                throw new ArgumentException("El tipo es obligatorio.");
            if (string.IsNullOrWhiteSpace(Universidad.Ciudad))
                throw new ArgumentException("La Ciudad es obligatoria");
            return await _repo.ActualizarAsync(Universidad);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("El ID debe ser mayor a 0.");
            return await _repo.EliminarAsync(id);
        }
    }
}