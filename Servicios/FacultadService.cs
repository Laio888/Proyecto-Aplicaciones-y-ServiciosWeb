using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Servicios
{
    public class FacultadService : IFacultadService
    {
        private readonly IFacultadRepository _repo;

        public FacultadService(IFacultadRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Facultad>> ListarAsync()
            => await _repo.ObtenerTodosAsync();

        public async Task<Facultad?> ObtenerPorIdAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("El ID debe ser mayor a 0.");
            return await _repo.ObtenerPorIdAsync(id);
        }

        public async Task<int> CrearAsync(Facultad Facultad)
        {
            if (string.IsNullOrWhiteSpace(Facultad.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");
            if (string.IsNullOrWhiteSpace(Facultad.Tipo))
                throw new ArgumentException("El tipo es obligatorio.");
            //if (DateTime.IsNull(Facultad.FechaFun))
              //  throw new ArgumentException("La Fecha es obligatoria");
            if (Facultad.UniversidadId<= 0)
                throw new ArgumentException("La Universidad es obligatoria");

            // Calcular el siguiente ID automáticamente
            var todos = await _repo.ObtenerTodosAsync();
            Facultad.Id = todos.Any() ? todos.Max(x => x.Id) + 1 : 1;

            Facultad.Nombre = Facultad.Nombre.Trim();
            Facultad.Tipo = Facultad.Tipo.Trim();
            
            return await _repo.InsertarAsync(Facultad);
        }

        public async Task<bool> ActualizarAsync(Facultad Facultad)
        {
            if (Facultad.Id <= 0) throw new ArgumentException("ID inválido.");
            if (string.IsNullOrWhiteSpace(Facultad.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");
            if (string.IsNullOrWhiteSpace(Facultad.Tipo))
                throw new ArgumentException("El tipo es obligatorio.");
           // if (DateTime.IsNull(Facultad.FechaFun))
           //     throw new ArgumentException("La Fecha es obligatoria");
            if (Facultad.UniversidadId <= 0)
                throw new ArgumentException("La Universidad es obligatoria");
            return await _repo.ActualizarAsync(Facultad);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("El ID debe ser mayor a 0.");
            return await _repo.EliminarAsync(id);
        }
    }
}