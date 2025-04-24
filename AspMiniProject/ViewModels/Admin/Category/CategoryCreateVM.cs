using System.ComponentModel.DataAnnotations;

namespace AspMiniProject.ViewModels.Admin.Category
{
    public class CategoryCreateVM
    {
        [Required]
        [MaxLength(255)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Category name can only contain letters and spaces.")]
        public string Name { get; set; }
    }
}
