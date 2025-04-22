namespace AspMiniProject.Models
{
    public class Review : BaseEntity
    {
        public string Description { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
