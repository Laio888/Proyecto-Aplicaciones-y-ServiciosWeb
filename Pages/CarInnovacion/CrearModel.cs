using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ApiKnowledgeMap.Servicios.Abstracciones;
using ModeloCarInnovacion = ApiKnowledgeMap.Modelos.CarInnovacion; // 👈

namespace ApiKnowledgeMap.Pages.CarInnovacion
{
    public class CrearModel : PageModel
    {
        private readonly ICarInnovacionService _servicio;

        [BindProperty]
        public ModeloCarInnovacion Item { get; set; } = new();

        public CrearModel(ICarInnovacionService servicio)
        {
            _servicio = servicio;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            await _servicio.CrearAsync(Item);
            return RedirectToPage("/CarInnovacion/Index");
        }
    }
}