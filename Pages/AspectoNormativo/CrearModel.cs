using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ApiKnowledgeMap.Servicios.Abstracciones;
using ModeloAspectoNormativo = ApiKnowledgeMap.Modelos.AspectoNormativo; // 👈

namespace ApiKnowledgeMap.Pages.AspectoNormativo
{
    public class CrearModel : PageModel
    {
        private readonly IAspectoNormativoService _servicio;

        [BindProperty]
        public ModeloAspectoNormativo Item { get; set; } = new();

        public CrearModel(IAspectoNormativoService servicio)
        {
            _servicio = servicio;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            await _servicio.CrearAsync(Item);
            return RedirectToPage("/AspectoNormativo/Index");
        }
    }
}