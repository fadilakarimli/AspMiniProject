using AspMiniProject.Models;
using AspMiniProject.ViewModels.Admin.SliderInfo;

namespace AspMiniProject.Services.Interfaces
{
    public interface ISliderInfoService
    {
        Task<List<SliderInfoVM>> GetAllAsync();
        Task<SliderInfo> GetByIdAsync(int id);
        Task CreateAsync(SliderInfo sliderInfo);
        Task UpdateAsync(int id, SliderInfo sliderInfo);
        Task DeleteAsync(int id);

    }
}
