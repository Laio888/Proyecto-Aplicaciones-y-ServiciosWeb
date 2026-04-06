using Dapper;
using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Repositorios
{
    public class RegistroCalificadoRepository : IRegistroCalificadoRepository
    {
        private readonly IProveedorConexion _proveedorConexion;

        public RegistroCalificadoRepository(IProveedorConexion proveedorConexion)
        {
            _proveedorConexion = proveedorConexion;
        }

        public async Task<IEnumerable<RegistroCalificado>> ObtenerTodosAsync()
        {
            using var conn = _proveedorConexion.ObtenerConexion();

            return await conn.QueryAsync<RegistroCalificado>(
                @"SELECT 
                    codigo AS Codigo,
                    cant_creditos AS CantCreditos,
                    hora_acom AS HoraAcom,
                    hora_ind AS HoraInd,
                    metodologia AS Metodologia,
                    fecha_inicio AS FechaInicio,
                    fecha_fin AS FechaFin,
                    duracion_anios AS DuracionAnios,
                    duracion_semestres AS DuracionSemestres,
                    tipo_titulacion AS TipoTitulacion,
                    programa AS Programa
                  FROM registro_calificado");
        }

        public async Task<RegistroCalificado?> ObtenerPorIdAsync(int codigo)
        {
            using var conn = _proveedorConexion.ObtenerConexion();

            return await conn.QueryFirstOrDefaultAsync<RegistroCalificado>(
                @"SELECT 
                    codigo AS Codigo,
                    cant_creditos AS CantCreditos,
                    hora_acom AS HoraAcom,
                    hora_ind AS HoraInd,
                    metodologia AS Metodologia,
                    fecha_inicio AS FechaInicio,
                    fecha_fin AS FechaFin,
                    duracion_anios AS DuracionAnios,
                    duracion_semestres AS DuracionSemestres,
                    tipo_titulacion AS TipoTitulacion,
                    programa AS Programa
                  FROM registro_calificado
                  WHERE codigo = @Codigo",
                new { Codigo = codigo });
        }

        public async Task<int> InsertarAsync(RegistroCalificado item)
        {
            using var conn = _proveedorConexion.ObtenerConexion();

            return await conn.ExecuteScalarAsync<int>(
                @"INSERT INTO registro_calificado
                  (codigo, cant_creditos, hora_acom, hora_ind, metodologia, fecha_inicio, fecha_fin, duracion_anios, duracion_semestres, tipo_titulacion, programa)
                  VALUES
                  (@Codigo, @CantCreditos, @HoraAcom, @HoraInd, @Metodologia, @FechaInicio, @FechaFin, @DuracionAnios, @DuracionSemestres, @TipoTitulacion, @Programa);
                  SELECT @Codigo;",
                item);
        }

        public async Task<bool> ActualizarAsync(RegistroCalificado item)
        {
            using var conn = _proveedorConexion.ObtenerConexion();

            var filas = await conn.ExecuteAsync(
                @"UPDATE registro_calificado
                  SET cant_creditos = @CantCreditos,
                      hora_acom = @HoraAcom,
                      hora_ind = @HoraInd,
                      metodologia = @Metodologia,
                      fecha_inicio = @FechaInicio,
                      fecha_fin = @FechaFin,
                      duracion_anios = @DuracionAnios,
                      duracion_semestres = @DuracionSemestres,
                      tipo_titulacion = @TipoTitulacion,
                      programa = @Programa
                  WHERE codigo = @Codigo",
                item);

            return filas > 0;
        }

        public async Task<bool> EliminarAsync(int codigo)
        {
            using var conn = _proveedorConexion.ObtenerConexion();

            var filas = await conn.ExecuteAsync(
                @"DELETE FROM registro_calificado
                  WHERE codigo = @Codigo",
                new { Codigo = codigo });

            return filas > 0;
        }
    }
}