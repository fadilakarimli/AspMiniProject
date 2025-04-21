using System.ComponentModel.DataAnnotations;

namespace AspMiniProject.ViewModels.Admin.Category
{
    public class CategoryCreateVM
    {
        [Required(ErrorMessage = "This input can not empty")]
        [MaxLength(30, ErrorMessage = "Category length must be 30")]
        public string? Name { get; set; }
    }
}
