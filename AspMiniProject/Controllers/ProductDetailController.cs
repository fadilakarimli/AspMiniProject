using AspMiniProject.Models;
using AspMiniProject.Services.Interfaces;
using AspMiniProject.ViewModels.Admin.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspMiniProject.Controllers
{
    public class ProductDetailController : Controller
    {
        private readonly IProductService _productService;

        public ProductDetailController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(int? id)
        {
            if (id == null) return BadRequest();

            var product = await _productService.GetProductByIdAsync(id.Value);

            if (product == null) return NotFound();

            return View(product);
        }
     




    }

}
