using AspMiniProject.ViewModels.Admin.Review;

namespace AspMiniProject.ViewModels.Admin.Customer
{
    public class CustomerVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Image { get; set; }
        public ICollection<ReviewVM> Reviews { get; set; }
    }
}
