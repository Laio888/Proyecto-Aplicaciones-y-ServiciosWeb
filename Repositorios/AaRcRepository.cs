using Dapper;
using ApiKnowledgeMap.Modelos;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Repositorios
{
    public class AaRcRepository : IAaRcRepository
    {
        private readonly IProveedorConexion _proveedorConexion;

        public AaRcRepository(IProveedorConexion proveedorConexion)
        {
            _proveedorConexion = proveedorConexion;
        }

        public async Task<IEnumerable<AaRc>> ObtenerTodosAsync()
        {
            using var conn = _proveedorConexion.ObtenerConexion();

            return await conn.QueryAsync<AaRc>(
                @"SELECT 
                    activ_academicas_idcurso AS ActivAcademicasIdcurso,
                    registro_calificado_codigo AS RegistroCalificadoCodigo,
                    componente AS Componente,
                    semestre AS Semestre
                  FROM aa_rc");
        }

        public async Task<AaRc?> ObtenerPorIdAsync(int activAcademicasIdcurso, int registroCalificadoCodigo)
        {
            using var conn = _proveedorConexion.ObtenerConexion();

            return await conn.QueryFirstOrDefaultAsync<AaRc>(
                @"SELECT 
                    activ_academicas_idcurso AS ActivAcademicasIdcurso,
                    registro_calificado_codigo AS RegistroCalificadoCodigo,
                    componente AS Componente,
                    semestre AS Semestre
                  FROM aa_rc
                  WHERE activ_academicas_idcurso = @activAcademicasIdcurso
                  AND registro_calificado_codigo = @registroCalificadoCodigo",
                new { activAcademicasIdcurso, registroCalificadoCodigo });
        }

        public async Task<bool> InsertarAsync(AaRc item)
        {
            using var conn = _proveedorConexion.ObtenerConexion();

            var filas = await conn.ExecuteAsync(
                @"INSERT INTO aa_rc
                  (activ_academicas_idcurso, registro_calificado_codigo, componente, semestre)
                  VALUES
                  (@ActivAcademicasIdcurso, @RegistroCalificadoCodigo, @Componente, @Semestre)",
                item);

            return filas > 0;
        }

        public async Task<bool> ActualizarAsync(AaRc item)
        {
            using var conn = _proveedorConexion.ObtenerConexion();

            var filas = await conn.ExecuteAsync(
                @"UPDATE aa_rc
                  SET componente = @Componente,
                      semestre = @Semestre
                  WHERE activ_academicas_idcurso = @ActivAcademicasIdcurso
                  AND registro_calificado_codigo = @RegistroCalificadoCodigo",
                item);

            return filas > 0;
        }

        public async Task<bool> EliminarAsync(int activAcademicasIdcurso, int registroCalificadoCodigo)
        {
            using var conn = _proveedorConexion.ObtenerConexion();

            var filas = await conn.ExecuteAsync(
                @"DELETE FROM aa_rc
                  WHERE activ_academicas_idcurso = @activAcademicasIdcurso
                  AND registro_calificado_codigo = @registro_calificado_codigo",
                new { activAcademicasIdcurso, registro_calificado_codigo = registroCalificadoCodigo });

            return filas > 0;
        }
    }
}