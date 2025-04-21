using AspMiniProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspMiniProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsletterController : Controller
    {
        private readonly INewsletterService _newsletterService;

        public NewsletterController(INewsletterService newsletterService)
        {
            _newsletterService = newsletterService;
        }

        public async Task<IActionResult> Index()
        {
            var newsletters = await _newsletterService.GetAllAsync();
            return View(newsletters);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _newsletterService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

    }

}
