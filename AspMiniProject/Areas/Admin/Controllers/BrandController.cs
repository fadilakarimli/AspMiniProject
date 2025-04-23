using AspMiniProject.Models;
using AspMiniProject.Services.Interfaces;
using AspMiniProject.ViewModels.Admin.Banner;
using AspMiniProject.ViewModels.Admin.Brand;
using Microsoft.AspNetCore.Mvc;

namespace AspMiniProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
           _brandService = brandService;
        }

        public async Task<IActionResult> Index()
        {
            var brands = await _brandService.GetAllAsync();
            var brandVMs = brands.Select(b => new BrandVM
            {
                Id = b.Id,
                Image = b.Image
            }).ToList();

            return View(brandVMs);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandCreateVM vm)
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

            var brand = new Brand
            {

                Image = uniqueFileName
            };

            await _brandService.CreateAsync(brand);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var brand = await _brandService.GetByIdAsync(id);
            if (brand is null) return NotFound();

            var vm = new BrandEditVM
            {
              
                Image = brand.Image
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BrandEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var existingBrand= await _brandService.GetByIdAsync(id);
            if (existingBrand == null) return NotFound();

            string imageFileName = existingBrand.Image;

            if (vm.Photo != null)
            {
                string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");
                imageFileName = Guid.NewGuid().ToString() + "_" + vm.Photo.FileName;
                string filePath = Path.Combine(uploadFolder, imageFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.Photo.CopyToAsync(fileStream);
                }

                string oldImagePath = Path.Combine(uploadFolder, existingBrand.Image);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            var updatedBrand= new Brand
            {
                Id = id,
                Image = imageFileName
            };

            await _brandService.UpdateAsync(id, updatedBrand);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _brandService.GetByIdAsync(id);
            if (brand == null) return NotFound();

            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", brand.Image);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            await _brandService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var brand = await _brandService.GetByIdAsync(id);
            if (brand == null) return NotFound();

            var vm = new BrandDetailVM
            {
             
                Image = brand.Image
            };

            return View(vm);
        }


    }
}
