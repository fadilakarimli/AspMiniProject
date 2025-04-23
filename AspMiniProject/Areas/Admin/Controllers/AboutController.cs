using AspMiniProject.Data;
using AspMiniProject.Helpers.Extensions;
using AspMiniProject.Models;
using AspMiniProject.Services;
using AspMiniProject.Services.Interfaces;
using AspMiniProject.ViewModels.Admin.About;
using AspMiniProject.ViewModels.Admin.Brand;
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
            var brandVMs = abouts.Select(b => new AboutVM
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Video = b.Video,
                CreatedDate = b.CreatedDate,
                Image = b.Image
            }).ToList();
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

            if (!ModelState.IsValid) return View(request);

            About dbAbout = await _context.Abouts.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (dbAbout == null) return NotFound();
            bool isChanged = false;

            if (request.Title != dbAbout.Title)
            {
                dbAbout.Title = request.Title;
                isChanged = true;
            }

            if (request.Desc != dbAbout.Description)
            {
                dbAbout.Description = request.Desc;
                isChanged = true;
            }

            if (request.Video != dbAbout.Video)
            {
                dbAbout.Video = request.Video;
                isChanged = true;
            }

            if (request.Photo != null)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + request.Photo.FileName;
                string uploadFolder = Path.Combine(_env.WebRootPath, "img");
                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.Photo.CopyToAsync(fileStream);
                }

                dbAbout.Image = uniqueFileName;
                isChanged = true;
            }
            else
            {
                dbAbout.Image = dbAbout.Image; 
            }

            if (isChanged)
            {
                _context.Abouts.Update(dbAbout);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));  
        }






        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _aboutService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            string uniqueFileName = null;
            if (vm.Image != null)
            {
                string uploadFolder = Path.Combine(_env.WebRootPath, "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + vm.Image.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.Image.CopyToAsync(fileStream);
                }
            }

            var about = new About
            {
                Title = vm.Title,
                Description = vm.Description,
                Video = vm.Video,
                Image = uniqueFileName
            };

            await _aboutService.CreateAsync(about);
            return RedirectToAction(nameof(Index));
        }

    }

}
