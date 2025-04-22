using AspMiniProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspMiniProject.ViewComponents
{
    public class ProductDetailViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public ProductDetailViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var products = await _productService.GetProductByIdAsync(id);

            return await Task.FromResult(View(products));
        }
    }
}
