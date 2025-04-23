using AspMiniProject.Data;
using AspMiniProject.Helpers.Extensions;
using AspMiniProject.Models;
using AspMiniProject.Services.Interfaces;
using AspMiniProject.ViewModels.Admin.About;
using Microsoft.EntityFrameworkCore;

namespace AspMiniProject.Services
{
    public class AboutService : IAboutService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AboutService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<AboutVM>> GetAllAsync()
        {
            return await _context.Abouts
                .Select(about => new AboutVM
                {
                    Id = about.Id,
                    Title = about.Title,
                    Description = about.Description,
                    Image = about.Image,
                    Video = about.Video,
                    CreatedDate = about.CreatedDate
                }).ToListAsync();
        }
        public async Task<AboutVM> GetByIdAsync(int id)
        {
            var about = await _context.Abouts.FirstOrDefaultAsync(m => m.Id == id);
            if (about == null) return null;

            return new AboutVM
            {
                Id = about.Id,
                Title = about.Title,
                Description = about.Description,
                Image = about.Image,
                Video = about.Video
            };
        }

        public async Task EditAsync(AboutEditVM request)
        {
            string? oldPath = null;
            string? fileName = null;

            if (request.Photo != null)
            {
                fileName = $"{Guid.NewGuid()} - {request.Photo.FileName}";

                if (!string.IsNullOrWhiteSpace(request.Image))
                {
                    oldPath = _env.GetFilePath("img", request.Image);
                }
            }
            else
            {
                fileName = request.Image;
            }

            About dbAbout = await _context.Abouts.AsNoTracking().FirstOrDefaultAsync(m => m.Id == request.Id);
            if (dbAbout == null) return;

            dbAbout.Title = request.Title;
            dbAbout.Description = request.Desc;
            dbAbout.Video = request.Video;
            dbAbout.Image = fileName;

            _context.Abouts.Update(dbAbout);
            await _context.SaveChangesAsync();

            if (request.Photo != null)
            {
                if (!string.IsNullOrWhiteSpace(oldPath) && File.Exists(oldPath))
                {
                    File.Delete(oldPath);
                }

                string newPath = _env.GetFilePath("img", fileName);
                await request.Photo.SaveFileAsync(newPath);
            }
        }

        public async Task CreateAsync(About about)
        {
            await _context.Abouts.AddAsync(about);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var about = await _context.Abouts.FindAsync(id);
            if (about is not null)
            {
                _context.Abouts.Remove(about);
                await _context.SaveChangesAsync();
            }
        }

    }

}
