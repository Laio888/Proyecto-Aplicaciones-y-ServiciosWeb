using Dapper;
using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Repositorios
{
    public class PremioRepository : IPremioRepository
    {
        private readonly IProveedorConexion _conexion;

        public PremioRepository(IProveedorConexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<Premio>> ObtenerTodosAsync()
        {
            using var conn = _conexion.ObtenerConexion();
            return await conn.QueryAsync<Premio>(
                @"SELECT
                    id               AS Id,
                    nombre           AS Nombre,
                    descripcion      AS Descripcion,
                    fecha            AS Fecha,
                    entidad_otorgante AS EntidadOtorgante,
                    pais             AS Pais,
                    programa         AS Programa
                  FROM premio");
        }

        public async Task<Premio?> ObtenerPorIdAsync(int id)
        {
            using var conn = _conexion.ObtenerConexion();
            return await conn.QueryFirstOrDefaultAsync<Premio>(
                @"SELECT
                    id               AS Id,
                    nombre           AS Nombre,
                    descripcion      AS Descripcion,
                    fecha            AS Fecha,
                    entidad_otorgante AS EntidadOtorgante,
                    pais             AS Pais,
                    programa         AS Programa
                  FROM premio
                  WHERE id = @Id",
                new { Id = id });
        }

        public async Task<int> InsertarAsync(Premio p)
        {
            using var conn = _conexion.ObtenerConexion();
            return await conn.ExecuteScalarAsync<int>(
                @"INSERT INTO premio
                    (id, nombre, descripcion, fecha, entidad_otorgante, pais, programa)
                  VALUES
                    (@Id, @Nombre, @Descripcion, @Fecha, @EntidadOtorgante, @Pais, @Programa);
                  SELECT @Id;",
                p);
        }

        public async Task<bool> ActualizarAsync(Premio p)
        {
            using var conn = _conexion.ObtenerConexion();
            var filas = await conn.ExecuteAsync(
                @"UPDATE premio SET
                    nombre            = @Nombre,
                    descripcion       = @Descripcion,
                    fecha             = @Fecha,
                    entidad_otorgante = @EntidadOtorgante,
                    pais              = @Pais,
                    programa          = @Programa
                  WHERE id = @Id",
                p);
            return filas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var conn = _conexion.ObtenerConexion();
            var filas = await conn.ExecuteAsync(
                "DELETE FROM premio WHERE id = @Id",
                new { Id = id });
            return filas > 0;
        }
    }
}