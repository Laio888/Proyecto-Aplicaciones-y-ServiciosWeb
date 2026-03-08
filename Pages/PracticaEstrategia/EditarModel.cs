using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ApiKnowledgeMap.Servicios.Abstracciones;
using ModeloPracticaEstrategia = ApiKnowledgeMap.Modelos.PracticaEstrategia;

namespace ApiKnowledgeMap.Pages.PracticaEstrategia
{
    public class EditarModel : PageModel
    {
        private readonly IPracticaEstrategiaService _servicio;

        [BindProperty]
        public ModeloPracticaEstrategia Item { get; set; } = new();

        public EditarModel(IPracticaEstrategiaService servicio)
        {
            _servicio = servicio;
        }

        public async Task OnGetAsync(int id)
        {
            Item = await _servicio.ObtenerPorIdAsync(id) ?? new();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            await _servicio.ActualizarAsync(Item);
            return RedirectToPage("/PracticaEstrategia/Index");
        }
    }
}