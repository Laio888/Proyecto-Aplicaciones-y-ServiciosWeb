using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ApiKnowledgeMap.Servicios
{
    public class ProgramaAcService : IProgramaAcService
    {
        private readonly IProgramaAcRepository _repo;
        private readonly IConfiguration _configuration;

        public ProgramaAcService(
            IProgramaAcRepository repo,
            IConfiguration configuration)
        {
            _repo = repo;
            _configuration = configuration;
        }

        public Task<IEnumerable<ProgramaAc>> ObtenerTodosAsync()
            => _repo.ObtenerTodosAsync();

        public Task<IEnumerable<ProgramaAc>> ObtenerPorProgramaAsync(int programaId)
            => _repo.ObtenerPorProgramaAsync(programaId);

        public Task<IEnumerable<ProgramaAc>> ObtenerPorAreaConocimientoAsync(int areaConocimientoId)
            => _repo.ObtenerPorAreaConocimientoAsync(areaConocimientoId);

        public Task<ProgramaAc?> ObtenerPorIdAsync(int programaId, int areaConocimientoId)
            => _repo.ObtenerPorIdAsync(programaId, areaConocimientoId);

        public async Task<bool> CrearAsync(ProgramaAc programaAc)
        {
            var connectionString = _configuration.GetConnectionString("SqlServer");

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("sp_asignar_area_programa", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Programa", programaAc.Programa);
            command.Parameters.AddWithValue("@AreaConocimiento", programaAc.AreaConocimiento);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            return true;
        }

        public async Task<bool> EliminarAsync(int programaId, int areaConocimientoId)
        {
            var connectionString = _configuration.GetConnectionString("SqlServer");

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("sp_eliminar_area_programa", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Programa", programaId);
            command.Parameters.AddWithValue("@AreaConocimiento", areaConocimientoId);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            return true;
        }
    }
}