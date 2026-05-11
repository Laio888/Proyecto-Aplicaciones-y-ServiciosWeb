using ApiKnowledgeMap.Servicios.Abstracciones;
using Microsoft.AspNetCore.Mvc;

namespace ApiKnowledgeMap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly IReporteService _service;

        public ReportesController(IReporteService service)
        {
            _service = service;
        }

        [HttpGet("programas-universidad")]
        public async Task<IActionResult> ProgramasPorUniversidad()
        {
            var datos = await _service.ProgramasPorUniversidadAsync();
            return Ok(datos);
        }

        [HttpGet("programas-areas")]
        public async Task<IActionResult> ProgramasConAreas()
        {
            var datos = await _service.ProgramasConAreasAsync();
            return Ok(datos);
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            var datos = await _service.ObtenerDashboardResumenAsync();
            return Ok(datos);
        }
        [HttpGet("programas-innovacion")]
        public async Task<IActionResult> ProgramasConInnovacion()
        {
            var datos = await _service.ProgramasConInnovacionAsync();
            return Ok(datos);
        }

        [HttpGet("programas-practicas")]
        public async Task<IActionResult> ProgramasConPracticas()
        {
            var datos = await _service.ProgramasConPracticasAsync();
            return Ok(datos);
        }

        [HttpGet("programas-normativa")]
        public async Task<IActionResult> ProgramasConNormativa()
        {
            var datos = await _service.ProgramasConNormativaAsync();
            return Ok(datos);
        }

        [HttpGet("registros-calificados")]
        public async Task<IActionResult> RegistrosCalificados()
        {
            var datos = await _service.RegistrosCalificadosAsync();
            return Ok(datos);
        }

        [HttpGet("actividades-registro")]
        public async Task<IActionResult> ActividadesConRegistro()
        {
            var datos = await _service.ActividadesConRegistroAsync();
            return Ok(datos);
        }

        [HttpGet("enfoques-registro")]
        public async Task<IActionResult> EnfoquesConRegistro()
        {
            var datos = await _service.EnfoquesConRegistroAsync();
            return Ok(datos);
        }

        [HttpGet("alianzas-programa")]
        public async Task<IActionResult> AlianzasPorPrograma()
        {
            var datos = await _service.AlianzasPorProgramaAsync();
            return Ok(datos);
        }
    }
}