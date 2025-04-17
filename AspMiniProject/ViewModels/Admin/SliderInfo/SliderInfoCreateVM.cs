using System.ComponentModel.DataAnnotations;

namespace AspMiniProject.ViewModels.Admin.SliderInfo
{
    public class SliderInfoCreateVM
    {
        [Required(ErrorMessage = "This input can not empty")]
        public string Discount { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
