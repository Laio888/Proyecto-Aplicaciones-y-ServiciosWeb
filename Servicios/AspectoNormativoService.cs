using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Servicios
{
    public class AspectoNormativoService : IAspectoNormativoService
    {
        private readonly IAspectoNormativoRepository _repo;

        public AspectoNormativoService(IAspectoNormativoRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<AspectoNormativo>> ListarAsync()
            => await _repo.ObtenerTodosAsync();

        public async Task<AspectoNormativo?> ObtenerPorIdAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("El ID debe ser mayor a 0.");
            return await _repo.ObtenerPorIdAsync(id);
        }

        public async Task<int> CrearAsync(AspectoNormativo AspectoNormativo)
        {
            if (string.IsNullOrWhiteSpace(AspectoNormativo.Tipo))
                throw new ArgumentException("El nombre es obligatorio.");
            if (string.IsNullOrWhiteSpace(AspectoNormativo.Descripcion))
                throw new ArgumentException("La descripcion es obligatoria.");
            if (string.IsNullOrWhiteSpace(AspectoNormativo.Fuente))
                throw new ArgumentException("La Fuente es obligatoria.");

            // Calcular el siguiente ID automáticamente
            var todos = await _repo.ObtenerTodosAsync();
            AspectoNormativo.Id = todos.Any() ? todos.Max(x => x.Id) + 1 : 1;

            AspectoNormativo.Tipo = AspectoNormativo.Tipo.Trim();
            AspectoNormativo.Descripcion = AspectoNormativo.Descripcion.Trim();
            AspectoNormativo.Fuente = AspectoNormativo.Fuente.Trim();
            AspectoNormativo.Descripcion = AspectoNormativo.Descripcion.Trim();
            return await _repo.InsertarAsync(AspectoNormativo);
        }

        public async Task<bool> ActualizarAsync(AspectoNormativo AspectoNormativo)
        {
            if (AspectoNormativo.Id <= 0) throw new ArgumentException("ID inválido.");
            if (string.IsNullOrWhiteSpace(AspectoNormativo.Tipo))
                throw new ArgumentException("El nombre es obligatorio.");
            if (string.IsNullOrWhiteSpace(AspectoNormativo.Descripcion))
                throw new ArgumentException("La descripcion es obligatoria.");
            if (string.IsNullOrWhiteSpace(AspectoNormativo.Fuente))
                throw new ArgumentException("La Fuente es obligatoria.");
            return await _repo.ActualizarAsync(AspectoNormativo);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("El ID debe ser mayor a 0.");
            return await _repo.EliminarAsync(id);
        }
    }
}