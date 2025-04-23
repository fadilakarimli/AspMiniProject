using AspMiniProject.Data;
using AspMiniProject.Models;
using AspMiniProject.Services.Interfaces;
using AspMiniProject.ViewModels.Admin.Customer;
using AspMiniProject.ViewModels.Admin.Review;
using Microsoft.EntityFrameworkCore;

namespace AspMiniProject.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CustomerService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IEnumerable<CustomerVM>> GetAllCustomersAsync()
        {
            var customers = await _context.Customers.Include(y => y.Reviews).AsNoTracking()
                                       .Select(c => new CustomerVM
                                       {
                                           Id = c.Id,
                                           FullName = c.FullName,
                                           Image = c.Image,
                                           Reviews = c.Reviews.Select(c => new ReviewVM { Id = c.Id, Description =c.Description }).ToList(),  

                                       }).ToListAsync();
            return customers;
        }

        public async Task<CustomerDetailVM> GetCustomerByIdAsync(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (customer == null) return null;

            return new CustomerDetailVM
            {
                FullName = customer.FullName,
                Image = customer.Image
            };
        }

        public async Task CreateCustomerAsync(CustomerCreateVM request)
        {
            var newCustomer = new Customer
            {
                FullName = request.FullName,
                // Bildiyimiz ki, email və digər sahələr burada olmalıdır, amma bu misalda göstərmirik
            };

            // Şəkili yükləmək üçün fayl adı və yolunu təyin edirik
            if (request.Image != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.Image.FileName);
                string filePath = Path.Combine(_env.WebRootPath, "img", fileName);

                // Şəkili serverə yükləyirik
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.Image.CopyToAsync(stream);
                }

                // Şəkili müştəriyə əlavə edirik
                newCustomer.Image = fileName;  // Image sahəsini burada əlavə etməliyik
            }

            await _context.Customers.AddAsync(newCustomer);
            await _context.SaveChangesAsync();
        }



        //public async Task EditCustomerAsync(int customerId, CustomerEditVM request)
        //{
        //    var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
        //    if (customer == null)
        //    {
        //        return;
        //    }

        //    if (request.NewImage != null)
        //    {
        //        customer.Image = await _env(request.NewImage);
        //    }

        //    customer.FullName = request.FullName;

        //    await _context.SaveChangesAsync();
        //}

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (customer == null)
            {
                throw new Exception("Customer not found.");
            }

            _context.Customers.Remove(customer);

            var changes = await _context.SaveChangesAsync();
            if (changes == 0)
            {
                throw new Exception("No changes were made to the database.");
            }
        }

    }
}
