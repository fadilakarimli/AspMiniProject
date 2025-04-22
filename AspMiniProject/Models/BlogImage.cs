namespace AspMiniProject.Models
{
    public class BlogImage : BaseEntity
    {
        public string ImagePath { get; set; } 
        public bool IsMain { get; set; } 
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
