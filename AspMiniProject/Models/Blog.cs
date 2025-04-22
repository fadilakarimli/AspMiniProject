namespace AspMiniProject.Models
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; } 
        public string Description { get; set; }
        public DateTime PublishedDate { get; set; }

        public ICollection<BlogImage> BlogImages { get; set; }
    }
}
