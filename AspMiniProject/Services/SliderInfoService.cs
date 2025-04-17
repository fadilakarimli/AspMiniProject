using AspMiniProject.Data;
using AspMiniProject.Helpers.Extensions;
using AspMiniProject.Models;
using AspMiniProject.Services.Interfaces;
using AspMiniProject.ViewModels.Admin.SliderInfo;
using Microsoft.EntityFrameworkCore;

namespace AspMiniProject.Services
{
    public class SliderInfoService : ISliderInfoService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderInfoService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<SliderInfoVM>> GetAllAsync()
        {
            return await _context.SliderInfos
                .Select(m => new SliderInfoVM
                {
                    Id = m.Id,
                    Title = m.Title,
                    Img = m.Img,
                    Description = m.Description
                }).ToListAsync();
        }


        public async Task CreateAsync(SliderInfoCreateVM model, string webRootPath)
        {
            if (model.Image == null)
                throw new Exception("Şəkil faylı boş ola bilməz");

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);
            string path = Path.Combine(webRootPath,"img" , fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await model.Image.CopyToAsync(stream);
            }

            SliderInfo sliderInfo = new SliderInfo
            {
                Title = model.Title,
                Description = model.Description,
                Img = fileName
            };

            await _context.SliderInfos.AddAsync(sliderInfo);
            await _context.SaveChangesAsync();
        }


        public async Task<SliderInfoDetailVM> GetDetailAsync(int id)
        {
            var slider = await _context.SliderInfos.FirstOrDefaultAsync(m => m.Id == id);

            if (slider is null) throw new Exception("SliderInfo not found");
            return new SliderInfoDetailVM
            {
                Title = slider.Title,
                Description = slider.Description,
                Image = slider.Img
            };
        }

        public async Task CreateAsync(SliderInfoCreateVM request)
        {
            if (request.Image.CheckFileSize(400))
                throw new Exception("Image size must be max 400KB");

            if (!request.Image.CheckFileType("image/"))
                throw new Exception("Image type must be image format");

            string folderPath = Path.Combine(_env.WebRootPath, "img");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string fileName = Guid.NewGuid().ToString() + "-" + request.Image.FileName;
            string path = Path.Combine(folderPath, fileName);

            using (FileStream stream = new(path, FileMode.Create))
            {
                await request.Image.CopyToAsync(stream);
            }

            await _context.SliderInfos.AddAsync(new SliderInfo
            {
                Title = request.Title,
                Description = request.Description,
                Img = fileName
            });

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var slider = await _context.SliderInfos.FirstOrDefaultAsync(m => m.Id == id);
            if (slider == null) throw new Exception("SliderInfo not found");

            _context.SliderInfos.Remove(slider);
            await _context.SaveChangesAsync();
        }

        public async Task<SliderInfoEditVM> GetEditVMAsync(int id)
        {
            var slider = await _context.SliderInfos.FirstOrDefaultAsync(d => d.Id == id);
            if (slider is null) throw new Exception("SliderInfo not found");

            return new SliderInfoEditVM
            {
                Title = slider.Title,
                Description = slider.Description,
                ImagePath = slider.Img
            };
        }

        public async Task EditAsync(int id, SliderInfoEditVM request)
        {
            var slider = await _context.SliderInfos.FirstOrDefaultAsync(d => d.Id == id);
            if (slider == null) throw new Exception("SliderInfo not found");

            bool exists = await _context.SliderInfos.AnyAsync(d => d.Title.Trim() == request.Title.Trim() && d.Id != slider.Id);
            if (exists) throw new Exception("SliderInfo already exists");

            slider.Title = request.Title;
            slider.Description = request.Description;

            if (request.Image != null)
            {
                if (!string.IsNullOrEmpty(slider.Img))
                {
                    string oldImagePath = Path.Combine(_env.WebRootPath, "img", slider.Img);
                    if (System.IO.File.Exists(oldImagePath))
                        System.IO.File.Delete(oldImagePath);
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.Image.FileName);
                string newImagePath = Path.Combine(_env.WebRootPath, "img", fileName);

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
