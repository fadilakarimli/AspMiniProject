using AspMiniProject.Services.Interfaces;
using AspMiniProject.ViewModels.Admin.Customer;
using Microsoft.AspNetCore.Mvc;

namespace AspMiniProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            var editVM = new CustomerEditVM
            {
                FullName = customer.FullName,
                Image = customer.Image
            };

            return View(editVM);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, CustomerEditVM model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _customerService.EditCustomerAsync(id, model);
        //        return RedirectToAction(nameof(Index));
        //    }

        //    ModelState.AddModelError("", "Customer not found or failed to update.");
        //    return View(model);
        //}

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerCreateVM model)
        {
            if (ModelState.IsValid)
            {
                await _customerService.CreateCustomerAsync(model);
                return RedirectToAction(nameof(Index)); 
            }

            ModelState.AddModelError("", "This name already exists or invalid data.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            var customerDetailVM = new CustomerDetailVM
            {
                FullName = customer.FullName,
                Image = customer.Image
            };

            return View(customerDetailVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _customerService.DeleteCustomerAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error deleting customer: {ex.Message}");
                return RedirectToAction(nameof(Index)); 
            }
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return View(customers);
        }
    }
}
