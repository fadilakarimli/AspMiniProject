using AspMiniProject.Services.Interfaces;
using AspMiniProject.ViewModels.Admin.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspMiniProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories); 
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            await _categoryService.CreateCategoryAsync(request); 
            return RedirectToAction(nameof(Index));  
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            var category = await _categoryService.GetCategoryByIdAsync(id.Value);
            if (category is null) return NotFound();

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteCategoryAsync(id); 
            return RedirectToAction(nameof(Index)); 
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            var category = await _categoryService.GetCategoryByIdAsync(id.Value);
            if (category is null) return NotFound();

            return View(new CategoryEditVM { Name = category.Name });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CategoryEditVM request)
        {
            if (id is null) return BadRequest();
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            await _categoryService.EditCategoryAsync(id.Value, request);  
            return RedirectToAction(nameof(Index));  
        }
    }
}
