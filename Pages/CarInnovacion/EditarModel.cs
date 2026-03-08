using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ApiKnowledgeMap.Servicios.Abstracciones;
using ModeloCarInnovacion = ApiKnowledgeMap.Modelos.CarInnovacion; // 👈

namespace ApiKnowledgeMap.Pages.CarInnovacion
{
    public class EditarModel : PageModel
    {
        private readonly ICarInnovacionService _servicio;

        [BindProperty]
        public ModeloCarInnovacion Item { get; set; } = new();

        public EditarModel(ICarInnovacionService servicio)
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
            return RedirectToPage("/CarInnovacion/Index");
        }
    }
}