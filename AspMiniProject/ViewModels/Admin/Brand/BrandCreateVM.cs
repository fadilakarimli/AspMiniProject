using System.ComponentModel.DataAnnotations;

namespace AspMiniProject.ViewModels.Admin.Brand
{
    public class BrandCreateVM
    {
        [Required]
        public IFormFile Photo { get; set; }
    }
}
