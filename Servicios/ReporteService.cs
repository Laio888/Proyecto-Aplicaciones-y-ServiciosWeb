using ApiKnowledgeMap.Modelos.Reportes;
using ApiKnowledgeMap.Repositorios.Abstracciones;
using ApiKnowledgeMap.Servicios.Abstracciones;

namespace ApiKnowledgeMap.Servicios
{
    public class ReporteService : IReporteService
    {
        private readonly IReporteRepository _repo;

        public ReporteService(IReporteRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<ProgramaUniversidadReporte>> ProgramasPorUniversidadAsync()
            => _repo.ProgramasPorUniversidadAsync();

        public Task<IEnumerable<ProgramaAreaReporte>> ProgramasConAreasAsync()
            => _repo.ProgramasConAreasAsync();

        public Task<DashboardResumen> ObtenerDashboardResumenAsync()
            => _repo.ObtenerDashboardResumenAsync();
        public Task<IEnumerable<ProgramaInnovacionReporte>> ProgramasConInnovacionAsync()
=> _repo.ProgramasConInnovacionAsync();

        public Task<IEnumerable<ProgramaPracticaReporte>> ProgramasConPracticasAsync()
            => _repo.ProgramasConPracticasAsync();

        public Task<IEnumerable<ProgramaNormativaReporte>> ProgramasConNormativaAsync()
            => _repo.ProgramasConNormativaAsync();

        public Task<IEnumerable<RegistroCalificadoReporte>> RegistrosCalificadosAsync()
            => _repo.RegistrosCalificadosAsync();

        public Task<IEnumerable<ActividadRegistroReporte>> ActividadesConRegistroAsync()
            => _repo.ActividadesConRegistroAsync();

        public Task<IEnumerable<EnfoqueRegistroReporte>> EnfoquesConRegistroAsync()
            => _repo.EnfoquesConRegistroAsync();

        public Task<IEnumerable<AlianzaProgramaReporte>> AlianzasPorProgramaAsync()
            => _repo.AlianzasPorProgramaAsync();
    }
}