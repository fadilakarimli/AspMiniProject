using AspMiniProject.Data;
using AspMiniProject.Models;
using AspMiniProject.Services.Interfaces;
using AspMiniProject.ViewModels.Admin.Banner;
using AspMiniProject.ViewModels.Admin.Brand;
using Microsoft.EntityFrameworkCore;

namespace AspMiniProject.Services
{
    public class BrandService  : IBrandService
    {
        private readonly AppDbContext _context;

        public BrandService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BrandVM>> GetAllAsync()
        {
            return await _context.Brands
                .Select(b => new BrandVM
                {
                    Id = b.Id,
                    Image = b.Image,
                    
                })
                .ToListAsync();
        }

        public async Task<Brand> GetByIdAsync(int id)
        {
            return await _context.Brands.FindAsync(id);
        }

        public async Task CreateAsync(Brand brand)
        {
            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Brand brand)
        {
            var exist = await _context.Brands.FindAsync(id);
            if (exist == null) throw new Exception("Brand not found");

            exist.Image = brand.Image;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand != null)
            {
                _context.Brands.Remove(brand);
                await _context.SaveChangesAsync();
            }
        }
    }
}
