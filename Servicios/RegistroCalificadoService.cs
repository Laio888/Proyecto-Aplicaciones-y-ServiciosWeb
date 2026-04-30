using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Servicios
{
    public class RegistroCalificadoService : IRegistroCalificadoService
    {
        private readonly IRegistroCalificadoRepository _repo;

        public RegistroCalificadoService(IRegistroCalificadoRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<RegistroCalificado>> ListarAsync()
            => await _repo.ObtenerTodosAsync();

        public async Task<RegistroCalificado?> ObtenerPorIdAsync(int codigo)
        {
            if (codigo <= 0)
                throw new ArgumentException("El código debe ser mayor a 0.");

            return await _repo.ObtenerPorIdAsync(codigo);
        }

        public async Task<int> CrearAsync(RegistroCalificado item)
        {
            if (item.Codigo <= 0)
                throw new ArgumentException("El código es obligatorio.");

            if (string.IsNullOrWhiteSpace(item.CantCreditos))
                throw new ArgumentException("La cantidad de créditos es obligatoria.");

            if (string.IsNullOrWhiteSpace(item.HoraAcom))
                throw new ArgumentException("La hora acompañada es obligatoria.");

            if (string.IsNullOrWhiteSpace(item.HoraInd))
                throw new ArgumentException("La hora independiente es obligatoria.");

            if (string.IsNullOrWhiteSpace(item.Metodologia))
                throw new ArgumentException("La metodología es obligatoria.");

            if (string.IsNullOrWhiteSpace(item.DuracionAnios))
                throw new ArgumentException("La duración en años es obligatoria.");

            if (string.IsNullOrWhiteSpace(item.DuracionSemestres))
                throw new ArgumentException("La duración en semestres es obligatoria.");

            if (string.IsNullOrWhiteSpace(item.TipoTitulacion))
                throw new ArgumentException("El tipo de titulación es obligatorio.");

            if (item.Programa <= 0)
                throw new ArgumentException("El programa es obligatorio.");

            item.CantCreditos = item.CantCreditos.Trim();
            item.HoraAcom = item.HoraAcom.Trim();
            item.HoraInd = item.HoraInd.Trim();
            item.Metodologia = item.Metodologia.Trim();
            item.DuracionAnios = item.DuracionAnios.Trim();
            item.DuracionSemestres = item.DuracionSemestres.Trim();
            item.TipoTitulacion = item.TipoTitulacion.Trim();

            return await _repo.InsertarAsync(item);
        }

        public async Task<bool> ActualizarAsync(RegistroCalificado item)
        {
            if (item.Codigo <= 0)
                throw new ArgumentException("El código es obligatorio.");

            if (item.Programa <= 0)
                throw new ArgumentException("El programa es obligatorio.");

            return await _repo.ActualizarAsync(item);
        }

        public async Task<bool> EliminarAsync(int codigo)
        {
            if (codigo <= 0)
                throw new ArgumentException("El código debe ser mayor a 0.");

            return await _repo.EliminarAsync(codigo);
        }
    }
}