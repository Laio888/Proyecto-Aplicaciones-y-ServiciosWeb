using Dapper;
using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Repositorios
{
    public class RolRepository : IRolRepository
    {
        private readonly IProveedorConexion _conexion;

        public RolRepository(IProveedorConexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<Rol>> ObtenerTodosAsync()
        {
            using var conn = _conexion.ObtenerConexion();
            return await conn.QueryAsync<Rol>(
                @"SELECT id AS Id, nombre AS Nombre FROM rol");
        }

        public async Task<Rol?> ObtenerPorIdAsync(int id)
        {
            using var conn = _conexion.ObtenerConexion();
            return await conn.QueryFirstOrDefaultAsync<Rol>(
                @"SELECT id AS Id, nombre AS Nombre 
                  FROM rol WHERE id = @Id",
                new { Id = id });
        }

        public async Task<bool> InsertarAsync(Rol r)
        {
            using var conn = _conexion.ObtenerConexion();
            var filas = await conn.ExecuteAsync(
                @"INSERT INTO rol (id, nombre) VALUES (@Id, @Nombre)", r);
            return filas > 0;
        }

        public async Task<bool> ActualizarAsync(Rol r)
        {
            using var conn = _conexion.ObtenerConexion();
            var filas = await conn.ExecuteAsync(
                @"UPDATE rol SET nombre = @Nombre WHERE id = @Id", r);
            return filas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var conn = _conexion.ObtenerConexion();
            var filas = await conn.ExecuteAsync(
                "DELETE FROM rol WHERE id = @Id", new { Id = id });
            return filas > 0;
        }
    }
}