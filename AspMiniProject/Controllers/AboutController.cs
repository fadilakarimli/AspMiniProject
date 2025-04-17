using Microsoft.AspNetCore.Mvc;

namespace AspMiniProject.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
