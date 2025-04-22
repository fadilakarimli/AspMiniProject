namespace AspMiniProject.ViewModels.Admin.Blog
{
    public class BlogEditVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishedDate { get; set; }

        public List<IFormFile>? Images { get; set; }

        public List<BlogImageVM> ExistingImages { get; set; }
        public int MainImageId { get; set; }
    }
}
