using System.ComponentModel.DataAnnotations;

namespace AspMiniProject.ViewModels.Admin.Banner
{
    public class BannerCreateVM
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public IFormFile Photo { get; set; }
    }
}
