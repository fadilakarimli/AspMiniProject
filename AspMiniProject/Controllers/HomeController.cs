using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AspMiniProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
       
    }
}
