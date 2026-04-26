using Microsoft.AspNetCore.Mvc;

namespace ApiKnowledgeMap.Controllers
{
    public class EnfoqueViewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}