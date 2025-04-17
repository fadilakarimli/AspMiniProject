using AspMiniProject.Services.Interfaces;
using AspMiniProject.ViewModels.Admin.SliderInfo;
using Microsoft.AspNetCore.Mvc;

namespace AspMiniProject.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class SliderInfoController : Controller
    {
        private readonly ISliderInfoService _sliderInfoService;
        private readonly IWebHostEnvironment _env;

        public SliderInfoController(ISliderInfoService sliderInfoService, IWebHostEnvironment env)
        {
            _sliderInfoService = sliderInfoService;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _sliderInfoService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            try
            {
                return View(await _sliderInfoService.GetDetailAsync(id.Value));
            }
            catch { return NotFound(); }
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderInfoCreateVM request)
        {
            if (!ModelState.IsValid) return View(request);

            try
            {
                await _sliderInfoService.CreateAsync(request, _env.WebRootPath);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Image", ex.Message);
                return View(request);
            }
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sliderInfoService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch { return NotFound(); }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            try
            {
                return View(await _sliderInfoService.GetEditVMAsync(id.Value));
            }
            catch { return NotFound(); }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SliderInfoEditVM request)
        {
            if (id is null) return BadRequest();
            if (!ModelState.IsValid) return View(request);

            try
            {
                await _sliderInfoService.EditAsync(id.Value, request);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(request);
            }
        }

    }
}
