using Microsoft.AspNetCore.Mvc.RazorPages;
using ApiKnowledgeMap.Servicios.Abstracciones;
using ModeloEnfoque = ApiKnowledgeMap.Modelos.Enfoque;

namespace ApiKnowledgeMap.Pages.Enfoque
{
    public class IndexModel : PageModel
    {
        private readonly IEnfoqueService _servicio;
        public List<ModeloEnfoque> Items { get; set; } = new();

        public IndexModel(IEnfoqueService servicio)
        {
            _servicio = servicio;
        }

        public async Task OnGetAsync()
        {
            Items = (await _servicio.ListarAsync()).ToList();
        }
    }
}