using ApiKnowledgeMap.Servicios;
using ApiKnowledgeMap.Servicios.Abstracciones;
using ApiKnowledgeMap.Servicios.Conexion;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiKnowledgeMap.Controllers
{
    [ApiController]
    [Route("api/Enfoque")]
    public class EnfoqueController : ControllerBase
    {
        private readonly IServicioCrud _servicio;

        public EnfoqueController(IServicioCrud service)
        {
            _servicio = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int? limite = null)
        {
            var datos = await _servicio.ListarAsync("enfoque", null, limite);
            return Ok(datos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var datos = await _servicio.ObtenerPorClaveAsync("enfoque", null, "id", id);
            if (datos.Count == 0) return NotFound();
            return Ok(datos[0]);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Dictionary<string, object?> datos)
        {
            string id = datos["id"].ToString();

            bool existe = await _servicio.ExisteAsync(
                "enfoque",
                "dbo",
                "id",
                id
            );

            if (existe)
            {
                return BadRequest("El ID ya existe.");
            }

            await _servicio.CrearAsync("enfoque", "dbo", datos);

            return Ok("Registro creado correctamente");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(string id, [FromBody] Dictionary<string, object?> datos)
        {
            var actualizados = await _servicio.ActualizarAsync("enfoque", null, "id", id, datos);
            return actualizados > 0 ? Ok("Registro actualizado") : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(string id)
        {
            var eliminados = await _servicio.EliminarAsync("enfoque", null, "id", id);
            return eliminados > 0 ? Ok("Registro eliminado") : NotFound();
        }
    }
}