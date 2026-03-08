using Microsoft.AspNetCore.Mvc.RazorPages;
using ApiKnowledgeMap.Servicios.Abstracciones;
using ModeloCarInnovacion = ApiKnowledgeMap.Modelos.CarInnovacion;

namespace ApiKnowledgeMap.Pages.CarInnovacion
{
    public class IndexModel : PageModel
    {
        private readonly ICarInnovacionService _servicio;
        public List<ModeloCarInnovacion> Items { get; set; } = new();

        public IndexModel(ICarInnovacionService servicio)
        {
            _servicio = servicio;
        }

        public async Task OnGetAsync()
        {
            Items = (await _servicio.ListarAsync()).ToList();
        }
    }
}