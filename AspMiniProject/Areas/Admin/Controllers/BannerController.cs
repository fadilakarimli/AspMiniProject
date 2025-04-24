using AspMiniProject.Models;
using AspMiniProject.Services.Interfaces;
using AspMiniProject.ViewModels.Admin.Banner;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspMiniProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BannerController : Controller
    {
        private readonly IBannerService _bannerService;
        private readonly IWebHostEnvironment _env;

        public BannerController(IBannerService bannerService, IWebHostEnvironment env)
        {
            _bannerService = bannerService;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var banners = await _bannerService.GetAllAsync();
            var bannerVMs = banners.Select(b => new BannerVM
            {
                Id = b.Id,
                Title = b.Title,
                Image = b.Image
            }).ToList();

            return View(bannerVMs); 
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BannerCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            string uniqueFileName = null;
            if (vm.Photo != null)
            {
                string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + vm.Photo.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.Photo.CopyToAsync(fileStream);
                }
            }

            var banner = new Banner
            {
                Title = vm.Title,
                Image = uniqueFileName 
            };

            await _bannerService.CreateAsync(banner);
            return RedirectToAction(nameof(Index));
        }


            [HttpGet]
            public async Task<IActionResult> Edit(int id)
            {
                var banner = await _bannerService.GetByIdAsync(id);
                if (banner is null) return NotFound();

                var vm = new BannerEditVM
                {
                    Title = banner.Title,
                    Image = banner.Image
                };

                return View(vm);
            }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BannerEditVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existBanner = await _bannerService.GetByIdAsync(model.Id);
            if (existBanner == null) return NotFound();

            string imageFileName = existBanner.Image!;

            if (model.Photo != null)
            {
                imageFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Photo.FileName);
                string filePath = Path.Combine(_env.WebRootPath, "img", imageFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(stream);
                }

                if (!string.IsNullOrEmpty(existBanner.Image))
                {
                    string oldPath = Path.Combine(_env.WebRootPath, "img", existBanner.Image);
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }
            }

            var updatedBanner = new Banner
            {
                Id = model.Id,
                Title = model.Title,
                Image = imageFileName
            };

            await _bannerService.UpdateAsync(model.Id, updatedBanner);
            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var banner = await _bannerService.GetByIdAsync(id);
            if (banner == null) return NotFound();

            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", banner.Image);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            await _bannerService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var banner = await _bannerService.GetByIdAsync(id);
            if (banner == null) return NotFound();

            var vm = new BannerDetailVM
            {
                Title = banner.Title,
                Image = banner.Image
            };

            return View(vm);
        }


    }


}

