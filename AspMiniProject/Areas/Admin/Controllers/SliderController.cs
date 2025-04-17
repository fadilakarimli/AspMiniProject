using AspMiniProject.Services.Interfaces;
using AspMiniProject.ViewModels.Admin.Slider;
using Microsoft.AspNetCore.Mvc;

namespace AspMiniProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IWebHostEnvironment _env;

        public SliderController(ISliderService sliderService, IWebHostEnvironment env)
        {
            _sliderService = sliderService;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var sliders = await _sliderService.GetAllAsync();
            return View(sliders);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            try
            {
                var slider = await _sliderService.GetDetailAsync(id.Value);
                return View(slider);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM request)
        {
            if (!ModelState.IsValid) return View(request);

            try
            {
                await _sliderService.CreateAsync(request, _env.WebRootPath);
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            try
            {
                await _sliderService.DeleteAsync(id.Value, _env.WebRootPath);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            try
            {
                var vm = await _sliderService.GetEditAsync(id.Value);
                return View(vm);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SliderEditVM request)
        {
            if (id is null) return BadRequest();
            if (!ModelState.IsValid) return View(request);

            try
            {
                await _sliderService.EditAsync(id.Value, request, _env.WebRootPath);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Image", ex.Message);
                return View(request);
            }
        }
    }
}
