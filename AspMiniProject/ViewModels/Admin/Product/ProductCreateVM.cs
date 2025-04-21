namespace AspMiniProject.ViewModels.Admin.Product
{
    public class ProductCreateVM
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
