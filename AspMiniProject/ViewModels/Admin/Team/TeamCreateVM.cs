using System.ComponentModel.DataAnnotations;

namespace AspMiniProject.ViewModels.Admin.Team
{
    public class TeamCreateVM
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
