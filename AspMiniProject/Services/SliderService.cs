using AspMiniProject.Data;
using AspMiniProject.Helpers.Extensions;
using AspMiniProject.Models;
using AspMiniProject.Services.Interfaces;
using AspMiniProject.ViewModels.Admin.Slider;
using Microsoft.EntityFrameworkCore;

namespace AspMiniProject.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;

        public SliderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SliderVM>> GetAllAsync()
        {
            return await _context.Sliders
                .Select(m => new SliderVM { Id = m.Id, Image = m.Img })
                .ToListAsync();
        }

        public async Task<SliderDetailVM> GetDetailAsync(int id)
        {
            var slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);
            if (slider == null) throw new Exception("Slider not found");

            return new SliderDetailVM { Img = slider.Img };
        }

        public async Task CreateAsync(SliderCreateVM request, string webRootPath)
        {
            if (request.Image.CheckFileSize(100))
                throw new Exception("Image size must be max 100KB");

            if (!request.Image.CheckFileType("image/"))
                throw new Exception("Image type must be image format");

            string fileName = Guid.NewGuid().ToString() + "-" + request.Image.FileName;
            string path = Path.Combine(webRootPath, "img", fileName);

            using (FileStream stream = new(path, FileMode.Create))
            {
                await request.Image.CopyToAsync(stream);
            }

            await _context.Sliders.AddAsync(new Slider { Img = fileName });
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, string webRootPath)
        {
            var slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);
            if (slider == null) throw new Exception("Slider not found");

            string path = Path.Combine(webRootPath, "img", slider.Img);
            path.Delete();

            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
        }

        public async Task<SliderEditVM> GetEditAsync(int id)
        {
            var slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider == null) throw new Exception("Slider not found");

            return new SliderEditVM { ImagePath = slider.Img };
        }

        public async Task EditAsync(int id, SliderEditVM request, string webRootPath)
        {
            var slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider == null) throw new Exception("Slider not found");

            if (request.Image != null)
            {
                if (request.Image.CheckFileSize(100))
                    throw new Exception("Image size must be max 100KB");

                if (!request.Image.CheckFileType("image/"))
                    throw new Exception("Image type must be image format");

                if (!string.IsNullOrEmpty(slider.Img))
                {
                    string oldImagePath = Path.Combine(webRootPath, "img", slider.Img);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.Image.FileName);
                string newImagePath = Path.Combine(webRootPath, "img", fileName);

                using (var stream = new FileStream(newImagePath, FileMode.Create))
                {
                    await request.Image.CopyToAsync(stream);
                }

                slider.Img = fileName;
            }

            await _context.SaveChangesAsync();
        }
    }
}
