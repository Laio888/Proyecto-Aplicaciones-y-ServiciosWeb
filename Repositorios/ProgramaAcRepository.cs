using Dapper;
using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Repositorios
{
    public class ProgramaAcRepository : IProgramaAcRepository
    {
        private readonly IProveedorConexion _conexion;

        public ProgramaAcRepository(IProveedorConexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<ProgramaAc>> ObtenerTodosAsync()
        {
            using var conn = _conexion.ObtenerConexion();
            return await conn.QueryAsync<ProgramaAc>(
                @"SELECT
                    programa          AS Programa,
                    area_conocimiento AS AreaConocimiento
                  FROM programa_ac");
        }

        public async Task<IEnumerable<ProgramaAc>> ObtenerPorProgramaAsync(int programaId)
        {
            using var conn = _conexion.ObtenerConexion();
            return await conn.QueryAsync<ProgramaAc>(
                @"SELECT
                    programa          AS Programa,
                    area_conocimiento AS AreaConocimiento
                  FROM programa_ac
                  WHERE programa = @ProgramaId",
                new { ProgramaId = programaId });
        }

        public async Task<IEnumerable<ProgramaAc>> ObtenerPorAreaConocimientoAsync(int areaConocimientoId)
        {
            using var conn = _conexion.ObtenerConexion();
            return await conn.QueryAsync<ProgramaAc>(
                @"SELECT
                    programa          AS Programa,
                    area_conocimiento AS AreaConocimiento
                  FROM programa_ac
                  WHERE area_conocimiento = @AreaConocimientoId",
                new { AreaConocimientoId = areaConocimientoId });
        }

        public async Task<ProgramaAc?> ObtenerPorIdAsync(int programaId, int areaConocimientoId)
        {
            using var conn = _conexion.ObtenerConexion();
            return await conn.QueryFirstOrDefaultAsync<ProgramaAc>(
                @"SELECT
                    programa          AS Programa,
                    area_conocimiento AS AreaConocimiento
                  FROM programa_ac
                  WHERE programa          = @ProgramaId
                    AND area_conocimiento = @AreaConocimientoId",
                new { ProgramaId = programaId, AreaConocimientoId = areaConocimientoId });
        }

        public async Task<bool> InsertarAsync(ProgramaAc p)
        {
            using var conn = _conexion.ObtenerConexion();
            var filas = await conn.ExecuteAsync(
                @"INSERT INTO programa_ac (programa, area_conocimiento)
                  VALUES (@Programa, @AreaConocimiento)",
                p);
            return filas > 0;
        }

        public async Task<bool> EliminarAsync(int programaId, int areaConocimientoId)
        {
            using var conn = _conexion.ObtenerConexion();
            var filas = await conn.ExecuteAsync(
                @"DELETE FROM programa_ac
                  WHERE programa          = @ProgramaId
                    AND area_conocimiento = @AreaConocimientoId",
                new { ProgramaId = programaId, AreaConocimientoId = areaConocimientoId });
            return filas > 0;
        }
    }
}