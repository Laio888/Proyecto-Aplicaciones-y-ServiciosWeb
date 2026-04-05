using Dapper;
using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Repositorios
{
    public class UniversidadRepository : IUniversidadRepository
    {
        private readonly IProveedorConexion _proveedorConexion;

        public UniversidadRepository(IProveedorConexion proveedorConexion)
        {
            _proveedorConexion = proveedorConexion;
        }

        public async Task<IEnumerable<Universidad>> ObtenerTodosAsync()
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            return await conn.QueryAsync<Universidad>(
                "SELECT id AS Id, nombre AS Nombre, tipo AS Tipo, ciudad AS Ciudad FROM universidad");
        }

        public async Task<Universidad?> ObtenerPorIdAsync(int id)
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            return await conn.QueryFirstOrDefaultAsync<Universidad>(
                "SELECT id AS Id, nombre AS Nombre, tipo AS Tipo, ciudad AS Ciudad FROM universidad WHERE id = @Id",
                new { Id = id });
        }

        public async Task<int> InsertarAsync(Universidad Universidad)
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            return await conn.ExecuteScalarAsync<int>(
                @"INSERT INTO universidad (id, nombre, tipo, ciudad )
                  VALUES (@Id, @Nombre, @Tipo, @Ciudad);
                  SELECT SCOPE_IDENTITY();", Universidad);
        }

        public async Task<bool> ActualizarAsync(Universidad Universidad)
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            var filas = await conn.ExecuteAsync(
                @"UPDATE universidad 
                  SET nombre = @Nombre, ciudad = @Ciudad, tipo = @Tipo
                  WHERE id = @Id", Universidad);
            return filas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            var filas = await conn.ExecuteAsync(
                "DELETE FROM universidad WHERE id = @Id", new { Id = id });
            return filas > 0;
        }
    }
}