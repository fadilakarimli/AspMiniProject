using AspMiniProject.Models;
using AspMiniProject.ViewModels.Admin.Product;

namespace AspMiniProject.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductVM>> GetAllProductsAsync();
        Task<ProductVM> GetProductByIdAsync(int id);
        Task CreateProductAsync(ProductCreateVM request);
        Task EditProductAsync(int id, ProductEditVM request);
        Task DeleteProductAsync(int id);
        Task<ProductDetailVM> GetDetailAsync(int id);
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Product> GetProductByIdForEditAsync(int id);
    }
}
