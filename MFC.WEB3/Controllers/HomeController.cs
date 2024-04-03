using Microsoft.AspNetCore.Mvc;

namespace MFC.WEB.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
