using AspMiniProject.Models;
using AspMiniProject.ViewModels.Admin.SliderInfo;

namespace AspMiniProject.Services.Interfaces
{
    public interface ISliderInfoService
    {
        Task<List<SliderInfoVM>> GetAllAsync();
        Task<SliderInfoDetailVM> GetDetailAsync(int id);
        Task CreateAsync(SliderInfoCreateVM request);
        Task DeleteAsync(int id);
        Task<SliderInfoEditVM> GetEditVMAsync(int id);
        Task EditAsync(int id, SliderInfoEditVM request);
        Task CreateAsync(SliderInfoCreateVM model, string webRootPath);

    }
}
