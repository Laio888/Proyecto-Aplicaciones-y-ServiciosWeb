using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ApiKnowledgeMap.Servicios.Abstracciones;
using ModeloEnfoque = ApiKnowledgeMap.Modelos.Enfoque; // 👈

namespace ApiKnowledgeMap.Pages.Enfoque
{
    public class EliminarModel : PageModel
    {
        private readonly IEnfoqueService _servicio;

        [BindProperty]
        public ModeloEnfoque Item { get; set; } = new();

        public EliminarModel(IEnfoqueService servicio)
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
            return RedirectToPage("/Enfoque/Index");
        }
    }
}