using System.ComponentModel.DataAnnotations;

namespace AspMiniProject.ViewModels.Admin.Category
{
    public class CategoryEditVM
    {
        [Required(ErrorMessage = "This input can not empty")]
        [MaxLength(30, ErrorMessage = "Category length must be 30")]
        [RegularExpression(@"^[a-zA-Z0-9\s\-\.\,]+$", ErrorMessage = "Name can only contain letters, numbers, spaces, dashes, commas and dots.")]

        public string? Name { get; set; }
        public int? MainImageId { get; set; }
    }
}
