using Microsoft.AspNetCore.Mvc;

namespace AspMiniProject.ViewComponents.Home
{
    public class TestimonialViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}
