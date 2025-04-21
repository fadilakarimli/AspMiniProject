using AspMiniProject.Data;
using AspMiniProject.Models;
using AspMiniProject.Services.Interfaces;
using AspMiniProject.ViewModels.Admin.Banner;
using Microsoft.EntityFrameworkCore;

namespace AspMiniProject.Services
{
    public class BannerService : IBannerService
    {
        private readonly AppDbContext _context;

        public BannerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BannerVM>> GetAllAsync()
        {
            return await _context.Banners
                .Select(b => new BannerVM
                {
                    Id = b.Id,
                    Image = b.Image,
                    Title = b.Title
                })
                .ToListAsync();
        }

        public async Task<Banner> GetByIdAsync(int id)
        {
            return await _context.Banners.FindAsync(id);
        }

        public async Task CreateAsync(Banner banner)
        {
            await _context.Banners.AddAsync(banner);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Banner banner)
        {
            var exist = await _context.Banners.FindAsync(id);
            if (exist == null) throw new Exception("Banner not found");

            exist.Title = banner.Title;
            exist.Image = banner.Image;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var banner = await _context.Banners.FindAsync(id);
            if (banner != null)
            {
                _context.Banners.Remove(banner);
                await _context.SaveChangesAsync();
            }
        }
    }
}

