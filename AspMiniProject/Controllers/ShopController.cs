using Microsoft.AspNetCore.Mvc;

namespace AspMiniProject.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
