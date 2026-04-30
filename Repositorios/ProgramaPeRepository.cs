using Dapper;
using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Repositorios
{
    public class ProgramaPeRepository : IProgramaPeRepository
    {
        private readonly IProveedorConexion _conexion;

        public ProgramaPeRepository(IProveedorConexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<ProgramaPe>> ObtenerTodosAsync()
        {
            using var conn = _conexion.ObtenerConexion();
            return await conn.QueryAsync<ProgramaPe>(
                @"SELECT
                    programa            AS Programa,
                    practica_estrategia AS PracticaEstrategia
                  FROM programa_pe");
        }

        public async Task<IEnumerable<ProgramaPe>> ObtenerPorProgramaAsync(int programaId)
        {
            using var conn = _conexion.ObtenerConexion();
            return await conn.QueryAsync<ProgramaPe>(
                @"SELECT
                    programa            AS Programa,
                    practica_estrategia AS PracticaEstrategia
                  FROM programa_pe
                  WHERE programa = @ProgramaId",
                new { ProgramaId = programaId });
        }

        public async Task<IEnumerable<ProgramaPe>> ObtenerPorPracticaEstrategiaAsync(int practicaEstrategiaId)
        {
            using var conn = _conexion.ObtenerConexion();
            return await conn.QueryAsync<ProgramaPe>(
                @"SELECT
                    programa            AS Programa,
                    practica_estrategia AS PracticaEstrategia
                  FROM programa_pe
                  WHERE practica_estrategia = @PracticaEstrategiaId",
                new { PracticaEstrategiaId = practicaEstrategiaId });
        }

        public async Task<ProgramaPe?> ObtenerPorIdAsync(int programaId, int practicaEstrategiaId)
        {
            using var conn = _conexion.ObtenerConexion();
            return await conn.QueryFirstOrDefaultAsync<ProgramaPe>(
                @"SELECT
                    programa            AS Programa,
                    practica_estrategia AS PracticaEstrategia
                  FROM programa_pe
                  WHERE programa            = @ProgramaId
                    AND practica_estrategia = @PracticaEstrategiaId",
                new { ProgramaId = programaId, PracticaEstrategiaId = practicaEstrategiaId });
        }

        public async Task<bool> InsertarAsync(ProgramaPe p)
        {
            using var conn = _conexion.ObtenerConexion();
            var filas = await conn.ExecuteAsync(
                @"INSERT INTO programa_pe (programa, practica_estrategia)
                  VALUES (@Programa, @PracticaEstrategia)",
                p);
            return filas > 0;
        }

        public async Task<bool> EliminarAsync(int programaId, int practicaEstrategiaId)
        {
            using var conn = _conexion.ObtenerConexion();
            var filas = await conn.ExecuteAsync(
                @"DELETE FROM programa_pe
                  WHERE programa            = @ProgramaId
                    AND practica_estrategia = @PracticaEstrategiaId",
                new { ProgramaId = programaId, PracticaEstrategiaId = practicaEstrategiaId });
            return filas > 0;
        }
    }
}