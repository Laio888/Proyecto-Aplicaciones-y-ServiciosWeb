using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ApiKnowledgeMap.Servicios.Abstracciones;
using ModeloUniversidad = ApiKnowledgeMap.Modelos.Universidad; // 👈

namespace ApiKnowledgeMap.Pages.Universidad
{
    public class EditarModel : PageModel
    {
        private readonly IUniversidadService _servicio;

        [BindProperty]
        public ModeloUniversidad Item { get; set; } = new();

        public EditarModel(IUniversidadService servicio)
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
            return RedirectToPage("/Universidad/Index");
        }
    }
}