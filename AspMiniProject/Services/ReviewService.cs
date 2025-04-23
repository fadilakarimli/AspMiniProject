using AspMiniProject.Data;
using AspMiniProject.Models;
using AspMiniProject.Services.Interfaces;
using AspMiniProject.ViewModels.Admin.Review;
using Microsoft.EntityFrameworkCore;

namespace AspMiniProject.Services
{
    public class ReviewService : IReviewService
    {
        private readonly AppDbContext _context;

        public ReviewService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Review review)
        {
            if (review == null)
            {
                throw new ArgumentNullException(nameof(review)); 
            }

            await _context.Reviews.AddAsync(review); 
            await _context.SaveChangesAsync(); 
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return false;

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditAsync(int id, ReviewCreateVM request)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return false;

            review.Description = request.Description;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ReviewVM>> GetAllAsync()
        {
            return await _context.Reviews.Include(y=>y.Customer).AsNoTracking()
                .Select(r => new ReviewVM
                {
                    Id = r.Id,
                    Description = r.Description,
                    CustomerName = r.Customer.FullName,
                }).ToListAsync();
        }

        public async Task<ReviewDetailVM> GetByIdAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return null;

            return new ReviewDetailVM
            {
                Id = review.Id,
                Description = review.Description
            };
        }
    }
}
