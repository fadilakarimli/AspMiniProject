using AspMiniProject.Services.Interfaces;
using AspMiniProject.ViewModels.Admin.Blog;
using Microsoft.AspNetCore.Mvc;

namespace AspMiniProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await _blogService.GetAllBlogsAsync();
            return View(blogs);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var blog = await _blogService.GetDetailAsync(id);
            if (blog == null) return NotFound();
            return View(blog);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCreateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            await _blogService.CreateBlogAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var blog = await _blogService.GetBlogByIdForEditAsync(id);
            if (blog == null) return NotFound();

            return View(blog); 
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, BlogEditVM model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.Images != null && model.Images.Count > 0)
            {
                await _blogService.EditBlogAsync(id, model);
            }
            else
            {
                await _blogService.EditBlogAsync(id, model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var blog = await _blogService.GetBlogByIdAsync(id);
            if (blog == null) return NotFound();

            return View(blog);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _blogService.DeleteBlogAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
