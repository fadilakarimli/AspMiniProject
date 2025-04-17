using System.ComponentModel.DataAnnotations;

namespace AspMiniProject.ViewModels.Admin.Slider
{
    public class SliderCreateVM
    {
        [Required]
        public IFormFile Image { get; set; }
    }
}
