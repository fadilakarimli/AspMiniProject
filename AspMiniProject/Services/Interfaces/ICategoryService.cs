using AspMiniProject.ViewModels.Admin.Category;

namespace AspMiniProject.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryVM>> GetAllCategoriesAsync();
        Task<CategoryDetailVM> GetCategoryByIdAsync(int id);
        Task CreateCategoryAsync(CategoryCreateVM request);
        Task EditCategoryAsync(int id, CategoryEditVM request);
        Task DeleteCategoryAsync(int id);

    }
}
