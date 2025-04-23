using System.ComponentModel.DataAnnotations;

namespace AspMiniProject.ViewModels.Admin.About
{
    public class AboutCreateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Video { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
