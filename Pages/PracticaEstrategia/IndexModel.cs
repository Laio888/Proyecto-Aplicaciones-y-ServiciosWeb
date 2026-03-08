using Microsoft.AspNetCore.Mvc.RazorPages;
using ApiKnowledgeMap.Servicios.Abstracciones;
using ModeloPracticaEstrategia = ApiKnowledgeMap.Modelos.PracticaEstrategia;

namespace ApiKnowledgeMap.Pages.PracticaEstrategia
{
    public class IndexModel : PageModel
    {
        private readonly IPracticaEstrategiaService _servicio;
        public List<ModeloPracticaEstrategia> Items { get; set; } = new();

        public IndexModel(IPracticaEstrategiaService servicio)
        {
            _servicio = servicio;
        }

        public async Task OnGetAsync()
        {
            Items = (await _servicio.ListarAsync()).ToList();
        }
    }
}