using AspMiniProject.Models;
using AspMiniProject.ViewModels.Admin.Banner;

namespace AspMiniProject.Services.Interfaces
{
    public interface IBannerService
    {
        Task<List<BannerVM>> GetAllAsync();
        Task<Banner> GetByIdAsync(int id);
        Task CreateAsync(Banner banner);
        Task UpdateAsync(int id, Banner banner);
        Task DeleteAsync(int id);
    }
}
