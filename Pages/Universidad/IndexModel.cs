using Microsoft.AspNetCore.Mvc.RazorPages;
using ApiKnowledgeMap.Servicios.Abstracciones;
using ModeloUniversidad = ApiKnowledgeMap.Modelos.Universidad;

namespace ApiKnowledgeMap.Pages.Universidad
{
    public class IndexModel : PageModel
    {
        private readonly IUniversidadService _servicio;
        public List<ModeloUniversidad> Items { get; set; } = new();

        public IndexModel(IUniversidadService servicio)
        {
            _servicio = servicio;
        }

        public async Task OnGetAsync()
        {
            Items = (await _servicio.ListarAsync()).ToList();
        }
    }
}