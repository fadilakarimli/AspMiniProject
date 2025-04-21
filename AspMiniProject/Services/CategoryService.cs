using AspMiniProject.Data;
using AspMiniProject.Models;
using AspMiniProject.Services.Interfaces;
using AspMiniProject.ViewModels.Admin.Category;
using Microsoft.EntityFrameworkCore;

namespace AspMiniProject.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryVM>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories.Select(m => new CategoryVM { Id = m.Id, Name = m.Name });
        }

        public async Task<CategoryDetailVM> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            if (category is null)
                return null;

            return new CategoryDetailVM { Name = category.Name };
        }

        public async Task<bool> CreateCategoryAsync(CategoryCreateVM request)
        {
            bool existsCategory = await _context.Categories.AnyAsync(m => m.Name.Trim() == request.Name.Trim());
            if (existsCategory)
            {
                return false;
            }

            await _context.Categories.AddAsync(new Category { Name = request.Name });
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditCategoryAsync(int id, CategoryEditVM request)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            if (category is null)
                return false;

            bool existsCategory = await _context.Categories.AnyAsync(m => m.Name.Trim() == request.Name.Trim() && m.Id != category.Id);
            if (existsCategory)
            {
                return false;
            }

            category.Name = request.Name;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            if (category is null)
                return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
