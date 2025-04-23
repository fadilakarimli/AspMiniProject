using AspMiniProject.Data;
using AspMiniProject.Models;
using AspMiniProject.Services.Interfaces;
using AspMiniProject.ViewModels.Admin.SliderInfo;
using AspMiniProject.ViewModels.Admin.Team;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace AspMiniProject.Services
{
    public class TeamService : ITeamService
    {
        private readonly AppDbContext _context;

        public TeamService(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Team team)
        {
            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team is not null)
            {
                _context.Teams.Remove(team);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<TeamVM>> GetAllAsync()
        {
            return await _context.Teams
                .Select(s => new TeamVM
                {
                    Id = s.Id,
                    Image = s.Image,
                    FullName = s.FullName,
                    Position = s.Position

                }).ToListAsync();
        }


        public async Task<Team> GetByIdAsync(int id)
        {
            return await _context.Teams.FindAsync(id);
        }

        public async Task UpdateAsync(int id, Team team)
        {
            var exist = await _context.Teams.FindAsync(id);
            if (exist is null) throw new Exception("Teams info not found");

            exist.FullName = team.FullName;
            exist.Image = team.Image;
            exist.Position = team.Position;

            await _context.SaveChangesAsync();
        }
    }
}
