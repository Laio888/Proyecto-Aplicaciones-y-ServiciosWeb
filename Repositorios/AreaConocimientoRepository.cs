using Dapper;
using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Repositorios
{
    public class AreaConocimientoRepository : IAreaConocimientoRepository
    {
        private readonly IProveedorConexion _conexion;

        public AreaConocimientoRepository(IProveedorConexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<AreaConocimiento>> ObtenerTodosAsync()
        {
            using var conn = _conexion.ObtenerConexion();
            return await conn.QueryAsync<AreaConocimiento>(
                @"SELECT
                    id         AS Id,
                    gran_area  AS GranArea,
                    area       AS Area,
                    disciplina AS Disciplina
                  FROM area_conocimiento");
        }

        public async Task<AreaConocimiento?> ObtenerPorIdAsync(int id)
        {
            using var conn = _conexion.ObtenerConexion();
            return await conn.QueryFirstOrDefaultAsync<AreaConocimiento>(
                @"SELECT
                    id         AS Id,
                    gran_area  AS GranArea,
                    area       AS Area,
                    disciplina AS Disciplina
                  FROM area_conocimiento
                  WHERE id = @Id",
                new { Id = id });
        }

        public async Task<int> InsertarAsync(AreaConocimiento a)
        {
            using var conn = _conexion.ObtenerConexion();
            return await conn.ExecuteScalarAsync<int>(
                @"INSERT INTO area_conocimiento (id, gran_area, area, disciplina)
                  VALUES (@Id, @GranArea, @Area, @Disciplina);
                  SELECT @Id;",
                a);
        }

        public async Task<bool> ActualizarAsync(AreaConocimiento a)
        {
            using var conn = _conexion.ObtenerConexion();
            var filas = await conn.ExecuteAsync(
                @"UPDATE area_conocimiento SET
                    gran_area  = @GranArea,
                    area       = @Area,
                    disciplina = @Disciplina
                  WHERE id = @Id",
                a);
            return filas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var conn = _conexion.ObtenerConexion();
            var filas = await conn.ExecuteAsync(
                "DELETE FROM area_conocimiento WHERE id = @Id",
                new { Id = id });
            return filas > 0;
        }
    }
}