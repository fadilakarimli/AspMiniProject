using System.ComponentModel.DataAnnotations;

namespace AspMiniProject.ViewModels.Admin.Product
{
    public class ProductCreateVM
    {
        [Required(ErrorMessage = "Name is required.")]
        [RegularExpression(@"^[a-zA-Z0-9\s\-\.\,]+$", ErrorMessage = "Name can only contain letters, numbers, spaces, dashes, commas and dots.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Price must be a valid number.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }

        public List<IFormFile> Images { get; set; }
    }
}
