using Microsoft.AspNetCore.Mvc;
using AspMiniProject.Services.Interfaces;
using AspMiniProject.ViewModels.Admin.Review;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspMiniProject.Models;

namespace AspMiniProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly ICustomerService _customerService;

        public ReviewController(IReviewService reviewService, ICustomerService customerService)
        {
            _reviewService = reviewService;
            _customerService = customerService;
        }

        public async Task<IActionResult> Index()
        {
            var reviews = await _reviewService.GetAllAsync();
            return View(reviews);
        }

        // GET: Create
        public async Task<IActionResult> Create()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            ViewBag.Customers = new SelectList(customers, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReviewCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                var customers = await _customerService.GetAllCustomersAsync();
                ViewBag.Customers = new SelectList(customers, "Id", "FullName");
                return View(request);
            }

            if (request.CustomerId == 0)
            {
                ModelState.AddModelError("CustomerId", "Please select a valid customer.");
                var customers = await _customerService.GetAllCustomersAsync();
                ViewBag.Customers = new SelectList(customers, "Id", "FullName");
                return View(request);
            }

            var review = new Review
            {
                Description = request.Description,
                CustomerId = request.CustomerId 
            };

            try
            {
                await _reviewService.CreateAsync(review);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("General", $"Failed to create the review: {ex.Message}");
                var customers = await _customerService.GetAllCustomersAsync();
                ViewBag.Customers = new SelectList(customers, "Id", "FullName");
                return View(request);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null) return NotFound();

            var vm = new ReviewCreateVM
            {
                Description = review.Description
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ReviewCreateVM request)
        {
            if (!ModelState.IsValid) return View(request);

            var result = await _reviewService.EditAsync(id, request);
            if (!result) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null) return NotFound();

            return View(review);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _reviewService.DeleteAsync(id);
            if (!result) return NotFound();

            return RedirectToAction(nameof(Index));
        }

    }
}
