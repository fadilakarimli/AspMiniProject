using AspMiniProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspMiniProject.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public ProductViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await _productService.GetAllProductsAsync();

            return await Task.FromResult(View(products));
        }
    }
}
