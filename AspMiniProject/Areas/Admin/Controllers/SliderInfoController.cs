using AspMiniProject.Models;
using AspMiniProject.Services.Interfaces;
using AspMiniProject.ViewModels.Admin.SliderInfo;
using Microsoft.AspNetCore.Mvc;

namespace AspMiniProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderInfoController : Controller
    {
        private readonly ISliderInfoService _sliderInfoService;

        public SliderInfoController(ISliderInfoService sliderInfoService)
        {
            _sliderInfoService = sliderInfoService;
        }
        public async Task<IActionResult> Index()
        {
            var sliderInfos = await _sliderInfoService.GetAllAsync();
            return View(sliderInfos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderInfoCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var newSliderInfo = new SliderInfo
            {
                Title = vm.Title,
                Description = vm.Description,
                Discount = vm.Discount
            };

            await _sliderInfoService.CreateAsync(newSliderInfo);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var sliderInfo = await _sliderInfoService.GetByIdAsync(id);
            if (sliderInfo is null) return NotFound();

            var vm = new SliderInfoEditVM
            {
                Title = sliderInfo.Title,
                Description = sliderInfo.Description,
                Discount = sliderInfo.Discount
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SliderInfoEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var updatedSlider = new SliderInfo
            {
                Title = vm.Title,
                Description = vm.Description,
                Discount = vm.Discount
            };

            await _sliderInfoService.UpdateAsync(id, updatedSlider);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _sliderInfoService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
