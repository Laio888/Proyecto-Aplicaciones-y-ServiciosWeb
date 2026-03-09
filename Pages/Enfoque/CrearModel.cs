using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ApiKnowledgeMap.Servicios.Abstracciones;
using ModeloEnfoque = ApiKnowledgeMap.Modelos.Enfoque; // 👈

namespace ApiKnowledgeMap.Pages.Enfoque
{
    public class CrearModel : PageModel
    {
        private readonly IEnfoqueService _servicio;

        [BindProperty]
        public ModeloEnfoque Item { get; set; } = new();

        public CrearModel(IEnfoqueService servicio)
        {
            _servicio = servicio;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            await _servicio.CrearAsync(Item);
            return RedirectToPage("/Enfoque/Index");
        }
    }
}