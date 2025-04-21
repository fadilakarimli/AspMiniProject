using AspMiniProject.ViewModels.Admin.Category;

namespace AspMiniProject.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryVM>> GetAllCategoriesAsync();
        Task<CategoryDetailVM> GetCategoryByIdAsync(int id);
        Task<bool> CreateCategoryAsync(CategoryCreateVM request);
        Task<bool> EditCategoryAsync(int id, CategoryEditVM request);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
