using AspMiniProject.Models;
using AspMiniProject.ViewModels.Admin.Banner;
using AspMiniProject.ViewModels.Admin.Brand;

namespace AspMiniProject.Services.Interfaces
{
    public interface IBrandService
    {
        Task<List<BrandVM>> GetAllAsync();
        Task<Brand> GetByIdAsync(int id);
        Task CreateAsync(Brand brand);
        Task UpdateAsync(int id, Brand brand);
        Task DeleteAsync(int id);
    }
}
