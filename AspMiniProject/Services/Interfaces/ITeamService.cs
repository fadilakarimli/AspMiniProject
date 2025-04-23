using AspMiniProject.Models;
using AspMiniProject.ViewModels.Admin.SliderInfo;
using AspMiniProject.ViewModels.Admin.Team;

namespace AspMiniProject.Services.Interfaces
{
    public interface ITeamService
    {
        Task<List<TeamVM>> GetAllAsync();
        Task<Team> GetByIdAsync(int id);
        Task CreateAsync(Team team);
        Task UpdateAsync(int id, Team team);
        Task DeleteAsync(int id);
    }
}
