using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ApiKnowledgeMap.Servicios.Abstracciones;
using ModeloPracticaEstrategia = ApiKnowledgeMap.Modelos.PracticaEstrategia;

namespace ApiKnowledgeMap.Pages.PracticaEstrategia
{
    public class CrearModel : PageModel
    {
        private readonly IPracticaEstrategiaService _servicio;

        [BindProperty]
        public ModeloPracticaEstrategia Item { get; set; } = new();

        public CrearModel(IPracticaEstrategiaService servicio)
        {
            _servicio = servicio;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            await _servicio.CrearAsync(Item);
            return RedirectToPage("/PracticaEstrategia/Index");
        }
    }
}