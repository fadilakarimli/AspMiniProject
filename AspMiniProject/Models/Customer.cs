namespace AspMiniProject.Models
{
    public class Customer : BaseEntity
    {
        public string FullName {  get; set; }
        public string Image { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
     