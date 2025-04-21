using AspMiniProject.Data;
using AspMiniProject.Models;
using AspMiniProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AspMiniProject.Services
{

    public class NewsletterService : INewsletterService
    {
        private readonly AppDbContext _context;

        public NewsletterService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddEmailAsync(string email)
        {
            var newsletter = new Newsletter { Email = email };
            _context.Newsletters.Add(newsletter);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Newsletter>> GetAllAsync()
        {
            return await _context.Newsletters.OrderByDescending(x => x.Email).ToListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var newsletter = await _context.Newsletters.FindAsync(id);
            if (newsletter != null)
            {
                _context.Newsletters.Remove(newsletter);
                await _context.SaveChangesAsync();
            }
        }
    }
}
