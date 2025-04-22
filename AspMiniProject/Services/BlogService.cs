using AspMiniProject.Data;
using AspMiniProject.Models;
using AspMiniProject.Services.Interfaces;
using AspMiniProject.ViewModels.Admin.Blog;
using Microsoft.EntityFrameworkCore;

namespace AspMiniProject.Services
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BlogService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<BlogVM>> GetAllBlogsAsync()
        {
            var blogs = await _context.Blogs.Include(b => b.BlogImages).ToListAsync();

            return blogs.Select(b => new BlogVM
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                PublishedDate = b.PublishedDate,
                Image = b.BlogImages.FirstOrDefault(bi => bi.IsMain)?.ImagePath
            }).ToList();
        }

        public async Task<BlogVM> GetBlogByIdAsync(int id)
        {
            var blog = await _context.Blogs.Include(b => b.BlogImages).FirstOrDefaultAsync(b => b.Id == id);
            if (blog == null) return null;

            return new BlogVM
            {
                Id = blog.Id,
                Title = blog.Title,
                Description = blog.Description,
                PublishedDate = blog.PublishedDate,
                Image = blog.BlogImages.FirstOrDefault(bi => bi.IsMain)?.ImagePath
            };
        }

        public async Task<Blog> GetBlogByIdForEditAsync(int id)
        {
            return await _context.Blogs.Include(b => b.BlogImages).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task CreateBlogAsync(BlogCreateVM request)
        {
            var newBlog = new Blog
            {
                Title = request.Title,
                Description = request.Description,
                PublishedDate = request.PublishedDate,
                BlogImages = new List<BlogImage>()
            };

            await _context.Blogs.AddAsync(newBlog);
            await _context.SaveChangesAsync();

            if (request.Images != null && request.Images.Count > 0)
            {
                bool isFirst = true;
                foreach (var image in request.Images)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    string filePath = Path.Combine(_env.WebRootPath, "img", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    newBlog.BlogImages.Add(new BlogImage
                    {
                        ImagePath = fileName,
                        IsMain = isFirst,
                        BlogId = newBlog.Id
                    });

                    isFirst = false;
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task EditBlogAsync(int id, BlogEditVM request)
        {
            var blog = await _context.Blogs.Include(b => b.BlogImages).FirstOrDefaultAsync(b => b.Id == id);
            if (blog == null) return;

            blog.Title = request.Title;
            blog.Description = request.Description;
            blog.PublishedDate = request.PublishedDate;

            if (request.Images != null && request.Images.Count > 0)
            {
                foreach (var image in request.Images)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    string filePath = Path.Combine(_env.WebRootPath, "img", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    await _context.BlogImages.AddAsync(new BlogImage
                    {
                        ImagePath = fileName,
                        IsMain = false,
                        BlogId = blog.Id
                    });
                }
            }

            foreach (var image in blog.BlogImages)
            {
                image.IsMain = (image.Id == request.MainImageId);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteBlogAsync(int id)
        {
            var blog = await _context.Blogs.Include(b => b.BlogImages).FirstOrDefaultAsync(b => b.Id == id);
            if (blog == null) return;

            foreach (var image in blog.BlogImages)
            {
                string path = Path.Combine(_env.WebRootPath, "img", image.ImagePath);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
        }

        public async Task<BlogDetailVM> GetDetailAsync(int id)
        {
            var blog = await _context.Blogs
                .Include(b => b.BlogImages)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (blog == null) return null;

            return new BlogDetailVM
            {
                Id = blog.Id,
                Title = blog.Title,
                Description = blog.Description,
                PublishedDate = blog.PublishedDate,
                MainImages = blog.BlogImages.Where(bi => bi.IsMain).Select(bi => bi.ImagePath).ToList(),
                ExtraImages = blog.BlogImages.Where(bi => !bi.IsMain).Select(bi => bi.ImagePath).ToList()
            };
        }
    }

}
