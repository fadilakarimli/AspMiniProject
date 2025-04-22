using AspMiniProject.Models;
using AspMiniProject.ViewModels.Admin.Customer;

namespace AspMiniProject.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerVM>> GetAllCustomersAsync();
        Task<CustomerDetailVM> GetCustomerByIdAsync(int id);
        Task CreateCustomerAsync(CustomerCreateVM request);
        //Task EditCustomerAsync(int id, CustomerEditVM request);
        Task DeleteCustomerAsync(int id);
    }
}
