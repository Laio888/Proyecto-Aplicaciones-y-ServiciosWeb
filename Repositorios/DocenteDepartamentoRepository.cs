using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;
using Dapper;

namespace ApiKnowledgeMap.Repositorios
{
    public class DocenteDepartamentoRepository : IDocenteDepartamentoRepository
    {
        private readonly IProveedorConexion _conexion;

        public DocenteDepartamentoRepository(IProveedorConexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<DocenteDepartamento>> ObtenerTodosAsync()
        {
            using var conn = _conexion.ObtenerConexion();

            return await conn.QueryAsync<DocenteDepartamento>(
                @"SELECT
                    docente AS Docente,
                    departamento AS Departamento,
                    dedicacion AS Dedicacion,
                    modalidad AS Modalidad,
                    fecha_ingreso AS FechaIngreso,
                    fecha_salida AS FechaSalida
                  FROM docente_departamento");
        }

        public async Task<DocenteDepartamento?> ObtenerPorIdAsync(int docente, int departamento)
        {
            using var conn = _conexion.ObtenerConexion();

            return await conn.QueryFirstOrDefaultAsync<DocenteDepartamento>(
                @"SELECT
                    docente AS Docente,
                    departamento AS Departamento,
                    dedicacion AS Dedicacion,
                    modalidad AS Modalidad,
                    fecha_ingreso AS FechaIngreso,
                    fecha_salida AS FechaSalida
                  FROM docente_departamento
                  WHERE docente = @Docente
                    AND departamento = @Departamento",
                new { Docente = docente, Departamento = departamento });
        }

        public async Task<bool> InsertarAsync(DocenteDepartamento item)
        {
            using var conn = _conexion.ObtenerConexion();

            var filas = await conn.ExecuteAsync(
                @"INSERT INTO docente_departamento
                    (docente, departamento, dedicacion, modalidad, fecha_ingreso, fecha_salida)
                  VALUES
                    (@Docente, @Departamento, @Dedicacion, @Modalidad, @FechaIngreso, @FechaSalida)",
                item);

            return filas > 0;
        }

        public async Task<bool> EliminarAsync(int docente, int departamento)
        {
            using var conn = _conexion.ObtenerConexion();

            var filas = await conn.ExecuteAsync(
                @"DELETE FROM docente_departamento
                  WHERE docente = @Docente
                    AND departamento = @Departamento",
                new { Docente = docente, Departamento = departamento });

            return filas > 0;
        }
    }
}