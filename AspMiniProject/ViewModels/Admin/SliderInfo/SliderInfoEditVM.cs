using System.ComponentModel.DataAnnotations;

namespace AspMiniProject.ViewModels.Admin.SliderInfo
{
    public class SliderInfoEditVM
    {
        [Required(ErrorMessage = "This input can not be empty")]
        public string Discount { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
   
    }
}
