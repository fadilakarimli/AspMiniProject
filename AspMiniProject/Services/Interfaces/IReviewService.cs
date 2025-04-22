using AspMiniProject.Models;
using AspMiniProject.ViewModels.Admin.Review;

namespace AspMiniProject.Services.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewVM>> GetAllAsync();
        Task<ReviewDetailVM> GetByIdAsync(int id);
        Task CreateAsync(Review review);
        Task<bool> EditAsync(int id, ReviewCreateVM request);
        Task<bool> DeleteAsync(int id);
    }
}
