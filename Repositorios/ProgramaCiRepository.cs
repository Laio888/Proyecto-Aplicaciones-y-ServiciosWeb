using Dapper;
using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Repositorios
{
    public class ProgramaCiRepository : IProgramaCiRepository
    {
        private readonly IProveedorConexion _conexion;

        public ProgramaCiRepository(IProveedorConexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<ProgramaCi>> ObtenerTodosAsync()
        {
            using var conn = _conexion.ObtenerConexion();
            return await conn.QueryAsync<ProgramaCi>(
                @"SELECT
                    programa       AS Programa,
                    car_innovacion AS CarInnovacion
                  FROM programa_ci");
        }

        public async Task<IEnumerable<ProgramaCi>> ObtenerPorProgramaAsync(int programaId)
        {
            using var conn = _conexion.ObtenerConexion();
            return await conn.QueryAsync<ProgramaCi>(
                @"SELECT
                    programa       AS Programa,
                    car_innovacion AS CarInnovacion
                  FROM programa_ci
                  WHERE programa = @ProgramaId",
                new { ProgramaId = programaId });
        }

        public async Task<IEnumerable<ProgramaCi>> ObtenerPorCarInnovacionAsync(int carInnovacionId)
        {
            using var conn = _conexion.ObtenerConexion();
            return await conn.QueryAsync<ProgramaCi>(
                @"SELECT
                    programa       AS Programa,
                    car_innovacion AS CarInnovacion
                  FROM programa_ci
                  WHERE car_innovacion = @CarInnovacionId",
                new { CarInnovacionId = carInnovacionId });
        }

        public async Task<ProgramaCi?> ObtenerPorIdAsync(int programaId, int carInnovacionId)
        {
            using var conn = _conexion.ObtenerConexion();
            return await conn.QueryFirstOrDefaultAsync<ProgramaCi>(
                @"SELECT
                    programa       AS Programa,
                    car_innovacion AS CarInnovacion
                  FROM programa_ci
                  WHERE programa = @ProgramaId
                    AND car_innovacion = @CarInnovacionId",
                new { ProgramaId = programaId, CarInnovacionId = carInnovacionId });
        }

        public async Task<bool> InsertarAsync(ProgramaCi p)
        {
            using var conn = _conexion.ObtenerConexion();
            var filas = await conn.ExecuteAsync(
                @"INSERT INTO programa_ci (programa, car_innovacion)
                  VALUES (@Programa, @CarInnovacion)",
                p);
            return filas > 0;
        }

        public async Task<bool> EliminarAsync(int programaId, int carInnovacionId)
        {
            using var conn = _conexion.ObtenerConexion();
            var filas = await conn.ExecuteAsync(
                @"DELETE FROM programa_ci
                  WHERE programa = @ProgramaId
                    AND car_innovacion = @CarInnovacionId",
                new { ProgramaId = programaId, CarInnovacionId = carInnovacionId });
            return filas > 0;
        }
    }
}