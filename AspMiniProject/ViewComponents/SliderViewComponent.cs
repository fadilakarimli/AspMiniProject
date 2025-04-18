using AspMiniProject.Services.Interfaces;
using AspMiniProject.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;

namespace AspMiniProject.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {
        private readonly ISliderService _sliderService;
        private readonly ISliderInfoService _sliderInfoService;

        public SliderViewComponent(ISliderService sliderService, ISliderInfoService sliderInfoService)
        {
            _sliderService = sliderService;
            _sliderInfoService = sliderInfoService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sliderImages = await _sliderService.GetAllAsync(); 
            var sliderInfos = await _sliderInfoService.GetAllAsync(); 

            var sliders = sliderImages
                .Zip(sliderInfos, (image, info) => new SliderComponentVM
                {
                    Image = image.Image,
                    Title = info.Title,
                    Description = info.Description,
                    Discount = info.Discount
                }).ToList();

            return View(sliders);
        }
    }
}
