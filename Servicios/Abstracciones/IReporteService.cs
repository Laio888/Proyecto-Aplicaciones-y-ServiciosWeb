using ApiKnowledgeMap.Modelos.Reportes;

namespace ApiKnowledgeMap.Servicios.Abstracciones
{
    public interface IReporteService
    {
        Task<IEnumerable<ProgramaUniversidadReporte>> ProgramasPorUniversidadAsync();
        Task<IEnumerable<ProgramaAreaReporte>> ProgramasConAreasAsync();
        Task<DashboardResumen> ObtenerDashboardResumenAsync();
        Task<IEnumerable<ProgramaInnovacionReporte>> ProgramasConInnovacionAsync();
        Task<IEnumerable<ProgramaPracticaReporte>> ProgramasConPracticasAsync();
        Task<IEnumerable<ProgramaNormativaReporte>> ProgramasConNormativaAsync();
        Task<IEnumerable<RegistroCalificadoReporte>> RegistrosCalificadosAsync();
        Task<IEnumerable<ActividadRegistroReporte>> ActividadesConRegistroAsync();
        Task<IEnumerable<EnfoqueRegistroReporte>> EnfoquesConRegistroAsync();
        Task<IEnumerable<AlianzaProgramaReporte>> AlianzasPorProgramaAsync();
    }
}