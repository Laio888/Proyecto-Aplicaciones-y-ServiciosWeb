using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ApiKnowledgeMap.Servicios.Abstracciones;
using ModeloCarInnovacion = ApiKnowledgeMap.Modelos.CarInnovacion; // 👈

namespace ApiKnowledgeMap.Pages.CarInnovacion
{
    public class EliminarModel : PageModel
    {
        private readonly ICarInnovacionService _servicio;

        [BindProperty]
        public ModeloCarInnovacion Item { get; set; } = new();

        public EliminarModel(ICarInnovacionService servicio)
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
            return RedirectToPage("/CarInnovacion/Index");
        }
    }
}