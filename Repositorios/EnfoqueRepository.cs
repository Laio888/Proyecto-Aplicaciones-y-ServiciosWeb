using Dapper;
using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Repositorios
{
    public class EnfoqueRepository : IEnfoqueRepository
    {
        private readonly IProveedorConexion _proveedorConexion;

        public EnfoqueRepository(IProveedorConexion proveedorConexion)
        {
            _proveedorConexion = proveedorConexion;
        }

        public async Task<IEnumerable<Enfoque>> ObtenerTodosAsync()
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            return await conn.QueryAsync<Enfoque>(
                "SELECT id AS Id, nombre AS Nombre, descripcion AS Descripcion FROM enfoque");
        }

        public async Task<Enfoque?> ObtenerPorIdAsync(int id)
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            return await conn.QueryFirstOrDefaultAsync<Enfoque>(
                "SELECT id AS Id, nombre AS Nombre, descripcion AS Descripcion FROM enfoque WHERE id = @Id",
                new { Id = id });
        }

        public async Task<int> InsertarAsync(Enfoque enfoque)
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            return await conn.ExecuteScalarAsync<int>(
                @"INSERT INTO enfoque (id, nombre, descripcion)
                  VALUES (@Id, @Nombre, @Descripcion);
                  SELECT SCOPE_IDENTITY();", enfoque);
        }

        public async Task<bool> ActualizarAsync(Enfoque enfoque)
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            var filas = await conn.ExecuteAsync(
                @"UPDATE enfoque 
                  SET nombre = @Nombre, descripcion = @Descripcion
                  WHERE id = @Id", enfoque);
            return filas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var conn = _proveedorConexion.ObtenerConexion();
            var filas = await conn.ExecuteAsync(
                "DELETE FROM enfoque WHERE id = @Id", new { Id = id });
            return filas > 0;
        }
    }
}