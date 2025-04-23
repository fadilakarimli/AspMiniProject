using AspMiniProject.Services.Interfaces;
using AspMiniProject.ViewModels.Admin.Customer;
using AspMiniProject.ViewModels.Admin.Review;
using Microsoft.AspNetCore.Mvc;

namespace AspMiniProject.ViewComponents
{
    public class TestimonialViewComponent : ViewComponent
    {
        private readonly ICustomerService _customerService;
        private readonly IReviewService _reviewService;
        public TestimonialViewComponent(ICustomerService customerService,
                                        IReviewService reviewService)
        {
            _customerService = customerService;
            _reviewService = reviewService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<CustomerVM> customers = await _customerService.GetAllCustomersAsync();
            IEnumerable<ReviewVM> reviews = await _reviewService.GetAllAsync();
            return await Task.FromResult(View(new CustomerReviewsVMVC { Customers = customers, Reviews = reviews }));
        }
    }
    public class CustomerReviewsVMVC
    {
        public IEnumerable<CustomerVM> Customers { get; set; }
        public IEnumerable<ReviewVM> Reviews { get; set; }
    }
}
