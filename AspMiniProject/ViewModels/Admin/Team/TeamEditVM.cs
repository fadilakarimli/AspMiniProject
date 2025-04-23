using System.ComponentModel.DataAnnotations;

namespace AspMiniProject.ViewModels.Admin.Team
{
    public class TeamEditVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "FullName is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Position is required")]
        public string Position { get; set; }

        public string Image { get; set; }

        public IFormFile Photo { get; set; }
    }
}
