using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Servicios
{
    public class CarInnovacionService : ICarInnovacionService
    {
        private readonly ICarInnovacionRepository _repo;

        public CarInnovacionService(ICarInnovacionRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<CarInnovacion>> ListarAsync()
            => await _repo.ObtenerTodosAsync();

        public async Task<CarInnovacion?> ObtenerPorIdAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("El ID debe ser mayor a 0.");
            return await _repo.ObtenerPorIdAsync(id);
        }

        public async Task<int> CrearAsync(CarInnovacion carInnovacion)
        {
            if (string.IsNullOrWhiteSpace(carInnovacion.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");
            if (string.IsNullOrWhiteSpace(carInnovacion.Tipo))
                throw new ArgumentException("El tipo es obligatorio.");

            carInnovacion.Nombre = carInnovacion.Nombre.Trim();
            carInnovacion.Tipo   = carInnovacion.Tipo.Trim();
            return await _repo.InsertarAsync(carInnovacion);
        }

        public async Task<bool> ActualizarAsync(CarInnovacion carInnovacion)
        {
            if (carInnovacion.Id <= 0) throw new ArgumentException("ID inválido.");
            if (string.IsNullOrWhiteSpace(carInnovacion.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");
            return await _repo.ActualizarAsync(carInnovacion);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("El ID debe ser mayor a 0.");
            return await _repo.EliminarAsync(id);
        }
    }
}