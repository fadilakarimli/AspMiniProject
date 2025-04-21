namespace AspMiniProject.ViewModels.Admin.Product
{
    public class ProductDetailVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public List<string> MainImages { get; set; } = new();
        public List<string> ExtraImages { get; set; } = new();
    }
}
