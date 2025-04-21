using AspMiniProject.Models;

namespace AspMiniProject.Services.Interfaces
{
    public interface INewsletterService
    {
        Task AddEmailAsync(string email);
        Task DeleteAsync(int id);
        Task<List<Newsletter>> GetAllAsync();
    }
}
