namespace AspMiniProject.ViewModels.Admin.Blog
{
    public class BlogCreateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishedDate { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
