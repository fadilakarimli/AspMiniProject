using AspMiniProject.Models;
using AspMiniProject.ViewModels.Admin.About;

namespace AspMiniProject.Services.Interfaces
{
    public interface IAboutService
    {
        Task CreateAsync(About about);
        Task DeleteAsync(int id);
        Task<List<AboutVM>> GetAllAsync(); 
        Task<AboutVM> GetByIdAsync(int id);
        Task EditAsync(AboutEditVM request);
    }
}
