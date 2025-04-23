using AspMiniProject.Data;
using AspMiniProject.Helpers.Extensions;
using AspMiniProject.Models;
using AspMiniProject.Services.Interfaces;
using AspMiniProject.ViewModels.Admin.About;
using Microsoft.AspNetCore.Mvc;
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
            About dbAbout = await _context.Abouts.FirstOrDefaultAsync(m => m.Id == request.Id);
            if (dbAbout == null) return;

            bool isChanged = false;

            if (dbAbout.Title != request.Title)
            {
                dbAbout.Title = request.Title;
                isChanged = true;
            }

            if (dbAbout.Description != request.Desc)
            {
                dbAbout.Description = request.Desc;
                isChanged = true;
            }

            if (dbAbout.Video != request.Video)
            {
                dbAbout.Video = request.Video;
                isChanged = true;
            }

            if (request.Photo != null)
            {
                string fileName = Guid.NewGuid().ToString() + " - " + request.Photo.FileName;
                string filePath = _env.GetFilePath("img", fileName);

                await request.Photo.SaveFileAsync(filePath);

                string oldImagePath = _env.GetFilePath("img", dbAbout.Image);
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }

                dbAbout.Image = fileName;
                isChanged = true;
            }

            if (isChanged)
            {
                _context.Abouts.Update(dbAbout);
                await _context.SaveChangesAsync();
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
