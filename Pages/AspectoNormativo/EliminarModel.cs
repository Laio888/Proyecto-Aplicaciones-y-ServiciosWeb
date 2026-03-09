using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ApiKnowledgeMap.Servicios.Abstracciones;
using ModeloAspectoNormativo = ApiKnowledgeMap.Modelos.AspectoNormativo; // 👈

namespace ApiKnowledgeMap.Pages.AspectoNormativo
{
    public class EliminarModel : PageModel
    {
        private readonly IAspectoNormativoService _servicio;

        [BindProperty]
        public ModeloAspectoNormativo Item { get; set; } = new();

        public EliminarModel(IAspectoNormativoService servicio)
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
            return RedirectToPage("/AspectoNormativo/Index");
        }
    }
}