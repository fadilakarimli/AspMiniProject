namespace AspMiniProject.ViewModels.Admin.Customer
{
    public class CustomerEditVM
    {
        public string FullName { get; set; }
        public string Image { get; set; } 
        public IFormFile NewImage { get; set; } 
    }
}
