using Microsoft.AspNetCore.Mvc.RazorPages;
using ApiKnowledgeMap.Servicios.Abstracciones;
using ModeloAspectoNormativo = ApiKnowledgeMap.Modelos.AspectoNormativo;

namespace ApiKnowledgeMap.Pages.AspectoNormativo
{
    public class IndexModel : PageModel
    {
        private readonly IAspectoNormativoService _servicio;
        public List<ModeloAspectoNormativo> Items { get; set; } = new();

        public IndexModel(IAspectoNormativoService servicio)
        {
            _servicio = servicio;
        }

        public async Task OnGetAsync()
        {
            Items = (await _servicio.ListarAsync()).ToList();
        }
    }
}