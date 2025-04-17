using Microsoft.AspNetCore.Mvc;

namespace AspMiniProject.ViewComponents
{
    public class ShopBannerViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}
