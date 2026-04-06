using Dapper;
using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Repositorios
{
    public class AcreditacionRepository : IAcreditacionRepository
    {
        private readonly IProveedorConexion _proveedorConexion;

        public AcreditacionRepository(IProveedorConexion proveedorConexion)
        {
            _proveedorConexion = proveedorConexion;
        }

        public async Task<IEnumerable<Acreditacion>> ObtenerTodosAsync()
        {
            using var conn = _proveedorConexion.ObtenerConexion();

            return await conn.QueryAsync<Acreditacion>(
                @"SELECT 
                    resolucion AS Resolucion,
                    tipo AS Tipo,
                    calificacion AS Calificacion,
                    fecha_inicio AS FechaInicio,
                    fecha_fin AS FechaFin,
                    programa AS Programa
                  FROM acreditacion");
        }

        public async Task<Acreditacion?> ObtenerPorIdAsync(int id)
        {
            using var conn = _proveedorConexion.ObtenerConexion();

            return await conn.QueryFirstOrDefaultAsync<Acreditacion>(
                @"SELECT 
                    resolucion AS Resolucion,
                    tipo AS Tipo,
                    calificacion AS Calificacion,
                    fecha_inicio AS FechaInicio,
                    fecha_fin AS FechaFin,
                    programa AS Programa
                  FROM acreditacion
                  WHERE resolucion = @Resolucion",
                new { Resolucion = id });
        }

        public async Task<int> InsertarAsync(Acreditacion acreditacion)
        {
            using var conn = _proveedorConexion.ObtenerConexion();

            return await conn.ExecuteScalarAsync<int>(
                @"INSERT INTO acreditacion (resolucion, tipo, calificacion,fecha_inicio, fecha_fin, programa)
                  VALUES (@Resolucion, @Tipo,@Calificacion, @FechaInicio,@FechaFin, @Programa);
                  SELECT @Resolucion;",
                acreditacion);
        }

        public async Task<bool> ActualizarAsync(Acreditacion acreditacion)
        {
            using var conn = _proveedorConexion.ObtenerConexion();

            var filas = await conn.ExecuteAsync(
                @"UPDATE acreditacion
                  SET tipo = @Tipo,
                      calificacion = @Calificacion,
                      fecha_inicio = @FechaInicio,
                      fecha_fin = @FechaFin,
                      programa = @Programa
                  WHERE resolucion = @Resolucion",
                acreditacion);

            return filas > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            using var conn = _proveedorConexion.ObtenerConexion();

            var filas = await conn.ExecuteAsync(
                "DELETE FROM acreditacion WHERE resolucion = @Resolucion",
                new { Resolucion = id });

            return filas > 0;
        }
    }
}