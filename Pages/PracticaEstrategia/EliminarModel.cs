using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ApiKnowledgeMap.Servicios.Abstracciones;
using ModeloPracticaEstrategia = ApiKnowledgeMap.Modelos.PracticaEstrategia;

namespace ApiKnowledgeMap.Pages.PracticaEstrategia
{
    public class EliminarModel : PageModel
    {
        private readonly IPracticaEstrategiaService _servicio;

        [BindProperty]
        public ModeloPracticaEstrategia Item { get; set; } = new();

        public EliminarModel(IPracticaEstrategiaService servicio)
        {
            _servicio = servicio;
        }

        public async Task OnGetAsync(int id)
        {
            Item = await _servicio.ObtenerPorIdAsync(id) ?? new();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _servicio.EliminarAsync(Item.Id);
            return RedirectToPage("/PracticaEstrategia/Index");
        }
    }
}