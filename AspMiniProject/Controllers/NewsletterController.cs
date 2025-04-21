using AspMiniProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspMiniProject.Controllers
{
    public class NewsletterController : Controller
    {
        private readonly INewsletterService _newsletterService;

        public NewsletterController(INewsletterService newsletterService)
        {
            _newsletterService = newsletterService;
        }
        public IActionResult Subscribe()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Subscribe(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                await _newsletterService.AddEmailAsync(email);

                TempData["SuccessMessage"] = "You have successfully subscribed!";
            }
            else
            {
                TempData["ErrorMessage"] = "Please enter a valid email address.";
            }

            return RedirectToAction("Subscribe", "Newsletter");

        }   
    }
}
