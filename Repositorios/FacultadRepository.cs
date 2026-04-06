using Dapper;
using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Repositorios
{
    public class FacultadRepository : IFacultadRepository
    {
        private readonly IProveedorConexion _proveedorConexion;

        public FacultadRepository(IProveedorConexion proveedorConexion)
        {
            _proveedorConexion = proveedorConexion;
        }

        public async Task<IEnumerable<Facultad>> ObtenerTodosAsync()
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            return await conn.QueryAsync<Facultad>(
                "SELECT f.id AS Id, f.nombre AS Nombre, f.tipo AS Tipo, f.fecha_fun AS FechaFun, f.universidad AS UniversidadId, u.id AS Id, u.nombre AS Nombre FROM facultad f INNER JOIN universidad u ON f.universidad = u.id");
        }

        public async Task<Facultad?> ObtenerPorIdAsync(int id)
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            return await conn.QueryFirstOrDefaultAsync<Facultad>(
                "SELECT id AS Id, nombre AS Nombre, tipo AS Tipo,  universidad AS UniversidadId FROM facultad WHERE id = @Id",
                new { Id = id });
        }

        public async Task<int> InsertarAsync(Facultad Facultad)
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            return await conn.ExecuteScalarAsync<int>(
                @"INSERT INTO facultad (id, nombre, tipo, fecha_fun, universidad )
                  VALUES (@Id, @Nombre, @Tipo, @FechaFun, @UniversidadId);
                  SELECT SCOPE_IDENTITY();", Facultad);
        }

        public async Task<bool> ActualizarAsync(Facultad Facultad)
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            var filas = await conn.ExecuteAsync(
                @"UPDATE facultad 
                  SET nombre = @Nombre, tipo = @Tipo, fecha_fun = @FechaFun, universidad = @Universidad
                  WHERE id = @Id", Facultad);
            return filas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            var filas = await conn.ExecuteAsync(
                "DELETE FROM facultad WHERE id = @Id", new { Id = id });
            return filas > 0;
        }
    }
}