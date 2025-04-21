using System.ComponentModel.DataAnnotations;

namespace AspMiniProject.ViewModels.Admin.Banner
{
    public class BannerEditVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }   
        public IFormFile Photo { get; set; }

    }
}
