using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Servicios
{
    public class EnfoqueRcService : IEnfoqueRcService
    {
        private readonly IEnfoqueRcRepository _repo;

        public EnfoqueRcService(IEnfoqueRcRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<EnfoqueRc>> ListarAsync()
            => await _repo.ObtenerTodosAsync();

        public async Task<EnfoqueRc?> ObtenerPorIdAsync(int enfoque, int registroCalificado)
        {
            if (enfoque <= 0)
                throw new ArgumentException("El enfoque es obligatorio.");

            if (registroCalificado <= 0)
                throw new ArgumentException("El registro calificado es obligatorio.");

            return await _repo.ObtenerPorIdAsync(enfoque, registroCalificado);
        }

        public async Task<bool> CrearAsync(EnfoqueRc item)
        {
            if (item.Enfoque <= 0)
                throw new ArgumentException("El enfoque es obligatorio.");

            if (item.RegistroCalificado <= 0)
                throw new ArgumentException("El registro calificado es obligatorio.");

            return await _repo.InsertarAsync(item);
        }

        public async Task<bool> ActualizarAsync(EnfoqueRc item)
        {
            if (item.Enfoque <= 0)
                throw new ArgumentException("El enfoque es obligatorio.");

            if (item.RegistroCalificado <= 0)
                throw new ArgumentException("El registro calificado es obligatorio.");

            return await _repo.ActualizarAsync(item);
        }

        public async Task<bool> EliminarAsync(int enfoque, int registroCalificado)
        {
            if (enfoque <= 0)
                throw new ArgumentException("El enfoque es obligatorio.");

            if (registroCalificado <= 0)
                throw new ArgumentException("El registro calificado es obligatorio.");

            return await _repo.EliminarAsync(enfoque, registroCalificado);
        }
    }
}