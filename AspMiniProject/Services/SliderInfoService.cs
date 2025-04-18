using AspMiniProject.Data;
using AspMiniProject.Helpers.Extensions;
using AspMiniProject.Models;
using AspMiniProject.Services.Interfaces;
using AspMiniProject.ViewModels.Admin.SliderInfo;
using Microsoft.EntityFrameworkCore;

namespace AspMiniProject.Services
{
    public class SliderInfoService : ISliderInfoService
    {
        private readonly AppDbContext _context;

        public SliderInfoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SliderInfoVM>> GetAllAsync()
        {
            return await _context.SliderInfos
                .Select(s => new SliderInfoVM
                {
                    Id = s.Id,
                    Title = s.Title,
                    Description = s.Description,
                    Discount = s.Discount
                }).ToListAsync();
        }


        public async Task<SliderInfo> GetByIdAsync(int id)
        {
            return await _context.SliderInfos.FindAsync(id);
        }

        public async Task CreateAsync(SliderInfo sliderInfo)
        {
            await _context.SliderInfos.AddAsync(sliderInfo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, SliderInfo sliderInfo)
        {
            var exist = await _context.SliderInfos.FindAsync(id);
            if (exist is null) throw new Exception("Slider info not found");

            exist.Title = sliderInfo.Title;
            exist.Description = sliderInfo.Description;
            exist.Discount = sliderInfo.Discount;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var sliderInfo = await _context.SliderInfos.FindAsync(id);
            if (sliderInfo is not null)
            {
                _context.SliderInfos.Remove(sliderInfo);
                await _context.SaveChangesAsync();
            }
        }
    }

}
