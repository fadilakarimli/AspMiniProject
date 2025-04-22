namespace AspMiniProject.ViewModels.Admin.Blog
{
    public class BlogDetailVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishedDate { get; set; }

        public List<string> MainImages { get; set; } = new();
        public List<string> ExtraImages { get; set; } = new();
    }
}
