using AspMiniProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspMiniProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
            
        }
        public async Task<IActionResult> Index(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }
    }
}
