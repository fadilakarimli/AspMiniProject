using AspMiniProject.Models;
using AspMiniProject.ViewModels.Admin.Blog;

namespace AspMiniProject.Services.Interfaces
{
    public interface IBlogService
    {
        Task<List<BlogVM>> GetAllBlogsAsync();
        Task<BlogVM> GetBlogByIdAsync(int id);
        Task<BlogEditVM> GetBlogByIdForEditAsync(int id);
        Task CreateBlogAsync(BlogCreateVM request);
        Task EditBlogAsync(int id, BlogEditVM request);
        Task DeleteBlogAsync(int id);
        Task<BlogDetailVM> GetDetailAsync(int id);
    }
}
