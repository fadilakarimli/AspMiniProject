using Microsoft.AspNetCore.Mvc;

namespace AspMiniProject.ViewComponents
{
    public class ProductDetailViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}
