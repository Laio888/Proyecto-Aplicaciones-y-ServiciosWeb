using ApiKnowledgeMap.Modelos.Reportes;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;
using Dapper;

namespace ApiKnowledgeMap.Repositorios
{
    public class ReporteRepository : IReporteRepository
    {
        private readonly IProveedorConexion _conexion;

        public ReporteRepository(IProveedorConexion conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<ProgramaUniversidadReporte>> ProgramasPorUniversidadAsync()
        {
            using var conn = _conexion.ObtenerConexion();

            return await conn.QueryAsync<ProgramaUniversidadReporte>(
                @"SELECT
                    u.nombre AS Universidad,
                    f.nombre AS Facultad,
                    p.nombre AS Programa,
                    p.nivel AS Nivel,
                    p.ciudad AS Ciudad
                  FROM programa p
                  INNER JOIN facultad f ON p.facultad = f.id
                  INNER JOIN universidad u ON f.universidad = u.id
                  LEFT JOIN registro_calificado rc ON rc.programa = p.id
                  ORDER BY u.nombre, f.nombre, p.nombre");
        }

        public async Task<IEnumerable<ProgramaAreaReporte>> ProgramasConAreasAsync()
        {
            using var conn = _conexion.ObtenerConexion();

            return await conn.QueryAsync<ProgramaAreaReporte>(
                @"SELECT
                    p.nombre AS Programa,
                    f.nombre AS Facultad,
                    u.nombre AS Universidad,
                    ac.area AS Area,
                    ac.disciplina AS Disciplina
                  FROM programa p
                  INNER JOIN facultad f ON p.facultad = f.id
                  INNER JOIN universidad u ON f.universidad = u.id
                  INNER JOIN programa_ac pac ON pac.programa = p.id
                  INNER JOIN area_conocimiento ac ON pac.area_conocimiento = ac.id
                  ORDER BY p.nombre, ac.area");
        }

        public async Task<DashboardResumen> ObtenerDashboardResumenAsync()
        {
            using var conn = _conexion.ObtenerConexion();

            var sql = @"
                SELECT
                    (SELECT COUNT(*) FROM universidad) AS TotalUniversidades,
                    (SELECT COUNT(*) FROM facultad) AS TotalFacultades,
                    (SELECT COUNT(*) FROM programa) AS TotalProgramas,
                    (SELECT COUNT(*) FROM area_conocimiento) AS TotalAreas,
                    (SELECT COUNT(*) FROM docente) AS TotalDocentes,
                    (SELECT COUNT(*) FROM aliado) AS TotalAliados";

            return await conn.QueryFirstAsync<DashboardResumen>(sql);
        }
        public async Task<IEnumerable<ProgramaInnovacionReporte>> ProgramasConInnovacionAsync()
        {
            using var conn = _conexion.ObtenerConexion();

            return await conn.QueryAsync<ProgramaInnovacionReporte>(
                @"SELECT
            u.nombre AS Universidad,
            f.nombre AS Facultad,
            p.nombre AS Programa,
            ci.nombre AS Caracteristica,
            ci.tipo AS TipoInnovacion
          FROM programa p
          INNER JOIN facultad f ON p.facultad = f.id
          INNER JOIN universidad u ON f.universidad = u.id
          INNER JOIN programa_ci pci ON pci.programa = p.id
          INNER JOIN car_innovacion ci ON pci.car_innovacion = ci.id
          ORDER BY p.nombre");
        }
        public async Task<IEnumerable<ProgramaPracticaReporte>> ProgramasConPracticasAsync()
        {
            using var conn = _conexion.ObtenerConexion();

            return await conn.QueryAsync<ProgramaPracticaReporte>(
                @"SELECT
            u.nombre AS Universidad,
            f.nombre AS Facultad,
            p.nombre AS Programa,
            pe.nombre AS Practica,
            pe.tipo AS TipoPractica
          FROM programa p
          INNER JOIN facultad f ON p.facultad = f.id
          INNER JOIN universidad u ON f.universidad = u.id
          INNER JOIN programa_pe ppe ON ppe.programa = p.id
          INNER JOIN practica_estrategia pe ON ppe.practica_estrategia = pe.id
          ORDER BY p.nombre");
        }
        public async Task<IEnumerable<ProgramaNormativaReporte>> ProgramasConNormativaAsync()
        {
            using var conn = _conexion.ObtenerConexion();

            return await conn.QueryAsync<ProgramaNormativaReporte>(
                @"SELECT
            u.nombre AS Universidad,
            f.nombre AS Facultad,
            p.nombre AS Programa,
            an.tipo AS TipoNormativa,
            an.fuente AS Fuente
          FROM programa p
          INNER JOIN facultad f ON p.facultad = f.id
          INNER JOIN universidad u ON f.universidad = u.id
          INNER JOIN an_programa ap ON ap.programa = p.id
          INNER JOIN aspecto_normativo an ON ap.aspecto_normativo = an.id
          ORDER BY p.nombre");
        }
        public async Task<IEnumerable<RegistroCalificadoReporte>> RegistrosCalificadosAsync()
        {
            using var conn = _conexion.ObtenerConexion();

            return await conn.QueryAsync<RegistroCalificadoReporte>(
                @"SELECT
            u.nombre AS Universidad,
            f.nombre AS Facultad,
            p.nombre AS Programa,
            rc.codigo AS CodigoRegistro,
            rc.metodologia AS Metodologia
          FROM registro_calificado rc
          INNER JOIN programa p ON rc.programa = p.id
          INNER JOIN facultad f ON p.facultad = f.id
          INNER JOIN universidad u ON f.universidad = u.id
          ORDER BY p.nombre");
        }
        public async Task<IEnumerable<ActividadRegistroReporte>> ActividadesConRegistroAsync()
        {
            using var conn = _conexion.ObtenerConexion();

            return await conn.QueryAsync<ActividadRegistroReporte>(
                @"SELECT
            u.nombre AS Universidad,
            p.nombre AS Programa,
            aa.nombre AS Actividad,
            rc.codigo AS CodigoRegistro,
            aar.componente AS Componente
          FROM aa_rc aar
          INNER JOIN activ_academica aa 
              ON aar.activ_academicas_idcurso = aa.id
          INNER JOIN registro_calificado rc 
              ON aar.registro_calificado_codigo = rc.codigo
          INNER JOIN programa p 
              ON rc.programa = p.id
          INNER JOIN facultad f 
              ON p.facultad = f.id
          INNER JOIN universidad u 
              ON f.universidad = u.id
          ORDER BY p.nombre");
        }
        public async Task<IEnumerable<EnfoqueRegistroReporte>> EnfoquesConRegistroAsync()
        {
            using var conn = _conexion.ObtenerConexion();

            return await conn.QueryAsync<EnfoqueRegistroReporte>(
                @"SELECT
            u.nombre AS Universidad,
            p.nombre AS Programa,
            rc.codigo AS CodigoRegistro,
            e.nombre AS Enfoque,
            e.descripcion AS Descripcion
          FROM enfoque_rc er
          INNER JOIN enfoque e 
              ON er.enfoque = e.id
          INNER JOIN registro_calificado rc 
              ON er.registro_calificado = rc.codigo
          INNER JOIN programa p 
              ON rc.programa = p.id
          INNER JOIN facultad f 
              ON p.facultad = f.id
          INNER JOIN universidad u 
              ON f.universidad = u.id
          ORDER BY p.nombre");
        }
        public async Task<IEnumerable<AlianzaProgramaReporte>> AlianzasPorProgramaAsync()
        {
            using var conn = _conexion.ObtenerConexion();

            return await conn.QueryAsync<AlianzaProgramaReporte>(
                @"SELECT
            u.nombre AS Universidad,
            p.nombre AS Programa,
            a.razon_social AS Aliado,
            CONCAT(d.nombres, ' ', d.apellidos) AS Docente,
            a.ciudad AS Ciudad
          FROM alianza al
          INNER JOIN aliado a ON al.aliado = a.nit
          INNER JOIN docente d ON al.docente = d.cedula
          INNER JOIN programa p ON al.departamento = p.id
          INNER JOIN facultad f ON p.facultad = f.id
          INNER JOIN universidad u ON f.universidad = u.id
          ORDER BY p.nombre");
        }
    }
}