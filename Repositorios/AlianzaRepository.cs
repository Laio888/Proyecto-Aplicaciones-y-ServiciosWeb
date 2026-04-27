using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;
using Dapper;

namespace ApiKnowledgeMap.Repositorios
{
    public class AlianzaRepository : IAlianzaRepository
    {
        private readonly IProveedorConexion _conexion;

        public AlianzaRepository(IProveedorConexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<Alianza>> ObtenerTodosAsync()
        {
            using var conn = _conexion.ObtenerConexion();

            return await conn.QueryAsync<Alianza>(
                @"SELECT 
                    aliado AS Aliado,
                    departamento AS Departamento,
                    fecha_inicio AS FechaInicio,
                    fecha_fin AS FechaFin,
                    docente AS Docente
                  FROM alianza");
        }

        public async Task<Alianza?> ObtenerPorIdAsync(int aliado, int departamento, int docente)
        {
            using var conn = _conexion.ObtenerConexion();

            return await conn.QueryFirstOrDefaultAsync<Alianza>(
                @"SELECT 
                    aliado AS Aliado,
                    departamento AS Departamento,
                    fecha_inicio AS FechaInicio,
                    fecha_fin AS FechaFin,
                    docente AS Docente
                  FROM alianza
                  WHERE aliado = @Aliado
                    AND departamento = @Departamento
                    AND docente = @Docente",
                new { Aliado = aliado, Departamento = departamento, Docente = docente });
        }

        public async Task<bool> InsertarAsync(Alianza alianza)
        {
            using var conn = _conexion.ObtenerConexion();

            var filas = await conn.ExecuteAsync(
                @"INSERT INTO alianza 
                    (aliado, departamento, fecha_inicio, fecha_fin, docente)
                  VALUES 
                    (@Aliado, @Departamento, @FechaInicio, @FechaFin, @Docente)",
                alianza);

            return filas > 0;
        }

        public async Task<bool> EliminarAsync(int aliado, int departamento, int docente)
        {
            using var conn = _conexion.ObtenerConexion();

            var filas = await conn.ExecuteAsync(
                @"DELETE FROM alianza
                  WHERE aliado = @Aliado
                    AND departamento = @Departamento
                    AND docente = @Docente",
                new { Aliado = aliado, Departamento = departamento, Docente = docente });

            return filas > 0;
        }
    }
}