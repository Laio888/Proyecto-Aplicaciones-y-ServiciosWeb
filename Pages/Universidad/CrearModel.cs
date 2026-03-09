using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ApiKnowledgeMap.Servicios.Abstracciones;
using ModeloUniversidad = ApiKnowledgeMap.Modelos.Universidad; // 👈

namespace ApiKnowledgeMap.Pages.Universidad
{
    public class CrearModel : PageModel
    {
        private readonly IUniversidadService _servicio;

        [BindProperty]
        public ModeloUniversidad Item { get; set; } = new();

        public CrearModel(IUniversidadService servicio)
        {
            _servicio = servicio;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            await _servicio.CrearAsync(Item);
            return RedirectToPage("/Universidad/Index");
        }
    }
}