using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Servicios
{
    public class AaRcService : IAaRcService
    {
        private readonly IAaRcRepository _repo;

        public AaRcService(IAaRcRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<AaRc>> ListarAsync()
            => await _repo.ObtenerTodosAsync();

        public async Task<AaRc?> ObtenerPorIdAsync(int activAcademicasIdcurso, int registroCalificadoCodigo)
        {
            if (activAcademicasIdcurso <= 0)
                throw new ArgumentException("La actividad académica es obligatoria.");

            if (registroCalificadoCodigo <= 0)
                throw new ArgumentException("El registro calificado es obligatorio.");

            return await _repo.ObtenerPorIdAsync(activAcademicasIdcurso, registroCalificadoCodigo);
        }

        public async Task<bool> CrearAsync(AaRc item)
        {
            if (item.ActivAcademicasIdcurso <= 0)
                throw new ArgumentException("La actividad académica es obligatoria.");

            if (item.RegistroCalificadoCodigo <= 0)
                throw new ArgumentException("El registro calificado es obligatorio.");

            if (string.IsNullOrWhiteSpace(item.Componente))
                throw new ArgumentException("El componente es obligatorio.");

            if (string.IsNullOrWhiteSpace(item.Semestre))
                throw new ArgumentException("El semestre es obligatorio.");

            item.Componente = item.Componente.Trim();
            item.Semestre = item.Semestre.Trim();

            return await _repo.InsertarAsync(item);
        }

        public async Task<bool> ActualizarAsync(AaRc item)
        {
            if (item.ActivAcademicasIdcurso <= 0)
                throw new ArgumentException("La actividad académica es obligatoria.");

            if (item.RegistroCalificadoCodigo <= 0)
                throw new ArgumentException("El registro calificado es obligatorio.");

            if (string.IsNullOrWhiteSpace(item.Componente))
                throw new ArgumentException("El componente es obligatorio.");

            if (string.IsNullOrWhiteSpace(item.Semestre))
                throw new ArgumentException("El semestre es obligatorio.");

            item.Componente = item.Componente.Trim();
            item.Semestre = item.Semestre.Trim();

            return await _repo.ActualizarAsync(item);
        }

        public async Task<bool> EliminarAsync(int activAcademicasIdcurso, int registroCalificadoCodigo)
        {
            if (activAcademicasIdcurso <= 0)
                throw new ArgumentException("La actividad académica es obligatoria.");

            if (registroCalificadoCodigo <= 0)
                throw new ArgumentException("El registro calificado es obligatorio.");

            return await _repo.EliminarAsync(activAcademicasIdcurso, registroCalificadoCodigo);
        }
    }
}