using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Servicios
{
    public class ProgramaService : IProgramaService
    {
        private readonly IProgramaRepository _repo;

        public ProgramaService(IProgramaRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Programa>> ListarAsync()
            => await _repo.ObtenerTodosAsync();

        public async Task<Programa?> ObtenerPorIdAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("El ID debe ser mayor a 0.");
            return await _repo.ObtenerPorIdAsync(id);
        }

        public async Task<int> CrearAsync(Programa Programa)
        {
            if (string.IsNullOrWhiteSpace(Programa.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");
            if (string.IsNullOrWhiteSpace(Programa.Tipo))
                throw new ArgumentException("El tipo es obligatorio.");
            if (string.IsNullOrWhiteSpace(Programa.Nivel))
                throw new ArgumentException("El campo es obligatorio.");
            if (string.IsNullOrWhiteSpace(Programa.FechaCreacion))
                throw new ArgumentException("El campo es obligatorio.");
            if (string.IsNullOrWhiteSpace(Programa.FechaCierre))
                throw new ArgumentException("El campo es obligatorio.");
            if (string.IsNullOrWhiteSpace(Programa.NumeroCohortes))
                throw new ArgumentException("El campo es obligatorio.");
            if (string.IsNullOrWhiteSpace(Programa.CantGraduados))
                throw new ArgumentException("El campo es obligatorio.");
            if (string.IsNullOrWhiteSpace(Programa.FechaActualizacion))
                throw new ArgumentException("la fecha es obligatorio.");
            if (string.IsNullOrWhiteSpace(Programa.Ciudad))
                throw new ArgumentException("La ciudad es obligatorio.");
            if (Programa.FacultadId<= 0)
                throw new ArgumentException("La facultad es obligatoria");

            // Calcular el siguiente ID automáticamente
            var todos = await _repo.ObtenerTodosAsync();
            Programa.Id = todos.Any() ? todos.Max(x => x.Id) + 1 : 1;

            Programa.Nombre = Programa.Nombre.Trim();
            Programa.Tipo = Programa.Tipo.Trim();
            Programa.Nivel = Programa.Tipo.Trim();
            Programa.FechaCreacion = Programa.Tipo.Trim();
            Programa.FechaCierre = Programa.Tipo.Trim();
            Programa.NumeroCohortes = Programa.Tipo.Trim();
            Programa.CantGraduados = Programa.Tipo.Trim();
            Programa.FechaActualizacion = Programa.Tipo.Trim();
            Programa.Ciudad = Programa.Tipo.Trim();
            
            return await _repo.InsertarAsync(Programa);
        }

        public async Task<bool> ActualizarAsync(Programa Programa)
        {
            if (Programa.Id <= 0) throw new ArgumentException("ID inválido.");
            if (string.IsNullOrWhiteSpace(Programa.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");
            if (string.IsNullOrWhiteSpace(Programa.Tipo))
                throw new ArgumentException("El tipo es obligatorio.");
            if (string.IsNullOrWhiteSpace(Programa.Nivel))
                throw new ArgumentException("El campo es obligatorio.");
            if (string.IsNullOrWhiteSpace(Programa.FechaCreacion))
                throw new ArgumentException("El campo es obligatorio.");
            if (string.IsNullOrWhiteSpace(Programa.FechaCierre))
                throw new ArgumentException("El campo es obligatorio.");
            if (string.IsNullOrWhiteSpace(Programa.NumeroCohortes))
                throw new ArgumentException("El campo es obligatorio.");
            if (string.IsNullOrWhiteSpace(Programa.CantGraduados))
                throw new ArgumentException("El campo es obligatorio.");
            if (string.IsNullOrWhiteSpace(Programa.FechaActualizacion))
                throw new ArgumentException("la fecha es obligatorio.");
            if (string.IsNullOrWhiteSpace(Programa.Ciudad))
                throw new ArgumentException("La ciudad es obligatorio.");
            if (Programa.FacultadId<= 0)
                throw new ArgumentException("La facultad es obligatoria");
            return await _repo.ActualizarAsync(Programa);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("El ID debe ser mayor a 0.");
            return await _repo.EliminarAsync(id);
        }
    }
}