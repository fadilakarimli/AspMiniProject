using AspMiniProject.Services.Interfaces;
using AspMiniProject.ViewModels.Admin.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace AspMiniProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var product = await _productService.GetDetailAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _productService.GetAllCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _productService.GetAllCategoriesAsync(); 
                ViewBag.Categories = new SelectList(categories, "Id", "Name");
                return View(model);
            }

            await _productService.CreateProductAsync(model);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]

        public async Task<IActionResult> Edit(int id)
        {
            var categoryList = await _productService.GetAllCategoriesAsync();
            ViewBag.Categories = categoryList.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            var product = await _productService.GetProductByIdForEditAsync(id);
            if (product == null) return NotFound();

            var productEditVM = new ProductEditVM
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId,
                MainImageId = product.ProductImages?.FirstOrDefault(pi => pi.IsMain)?.Id ?? 0,
                ExistingImages = product.ProductImages?.Select(pi => new ProductImageVM
                {
                    Id = pi.Id,
                    Img = pi.Img,
                    IsMain = pi.IsMain
                }).ToList() ?? new List<ProductImageVM>()
            };

            return View(productEditVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductEditVM model)
        {
            if (model.Price <= 0)
            {
                ModelState.AddModelError(nameof(model.Price), "Price must be a positive number.");
            }

            if (!ModelState.IsValid)
            {
                var categoryList = await _productService.GetAllCategoriesAsync();
                ViewBag.Categories = categoryList.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

                return View(model);
            }

            await _productService.EditProductAsync(id, model);
            return RedirectToAction(nameof(Index)); 
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }



    }
}
