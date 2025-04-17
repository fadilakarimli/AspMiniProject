using AspMiniProject.Models;
using AspMiniProject.ViewModels.Admin.Slider;

namespace AspMiniProject.Services.Interfaces
{
    public interface ISliderService
    {
        Task<List<SliderVM>> GetAllAsync();
        Task<SliderDetailVM> GetDetailAsync(int id);
        Task CreateAsync(SliderCreateVM request, string webRootPath);
        Task DeleteAsync(int id, string webRootPath);
        Task<SliderEditVM> GetEditAsync(int id);
        Task EditAsync(int id, SliderEditVM request, string webRootPath);
    }
}
