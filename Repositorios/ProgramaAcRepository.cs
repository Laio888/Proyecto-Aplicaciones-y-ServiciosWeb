using Dapper;
using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;
using System.Data;

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
                "sp_asignar_area_programa",
                new
                {
                    programa = p.Programa,
                    area = p.AreaConocimiento
                },
                commandType: CommandType.StoredProcedure
            );

            return filas > 0;
        }

        public async Task<bool> EliminarAsync(int programaId, int areaConocimientoId)
        {
            using var conn = _conexion.ObtenerConexion();

            var filas = await conn.ExecuteAsync(
                "sp_eliminar_area_programa",
                new
                {
                    programa = programaId,
                    area = areaConocimientoId
                },
                commandType: CommandType.StoredProcedure
            );

            return true;
        }
    }
}