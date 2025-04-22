using AspMiniProject.Data;
using AspMiniProject.ViewModels.Admin.Blog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspMiniProject.ViewComponents
{
    public class BlogViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public BlogViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var blogs = await _context.Blogs
                .OrderByDescending(b => b.PublishedDate)
                .Select(b => new BlogVM
                {
                    Title = b.Title,
                    Description = b.Description,
                    Image = b.BlogImages.FirstOrDefault(bi => bi.IsMain).ImagePath,
                    PublishedDate = b.PublishedDate
                })
                .Take(3)
                .ToListAsync();

            return View(blogs);
        }
    }
}
