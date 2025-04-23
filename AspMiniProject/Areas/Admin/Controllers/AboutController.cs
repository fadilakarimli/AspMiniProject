using AspMiniProject.Data;
using AspMiniProject.Helpers.Extensions;
using AspMiniProject.Models;
using AspMiniProject.Services.Interfaces;
using AspMiniProject.ViewModels.Admin.About;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspMiniProject.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IAboutService _aboutService;

        public AboutController(AppDbContext context, IWebHostEnvironment env, IAboutService aboutService)
        {
            _context = context;
            _env = env;
            _aboutService = aboutService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var abouts = await _aboutService.GetAllAsync();
            if (abouts == null || !abouts.Any())
            {
                return View(new List<AboutVM>());
            }
            return View(abouts);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            var about = await _aboutService.GetByIdAsync((int)id);
            if (about == null) return NotFound();

            return View(about);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            About about = await _context.Abouts.FirstOrDefaultAsync(m => m.Id == id);
            if (about == null) return NotFound();

            var aboutEditVM = new AboutEditVM
            {
                Id = about.Id,
                Title = about.Title,
                Desc = about.Description,
                Video = about.Video,
                Image = about.Image
            };

            return View(aboutEditVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, AboutEditVM request)
        {
            if (id is null) return BadRequest();

            About dbAbout = await _context.Abouts.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (dbAbout == null) return NotFound();

            request.Image = dbAbout.Image;

            if (!ModelState.IsValid) return View(request);

            if (request.Photo != null)
            {
                if (!request.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File can be only image format");
                    return View(request);
                }

                if (!request.Photo.CheckFileSize(200))
                {
                    ModelState.AddModelError("Photo", "File size can be max 200 kb");
                    return View(request);
                }
            }

            await _aboutService.EditAsync(request);

            return RedirectToAction(nameof(Index));
        }
    }

}
