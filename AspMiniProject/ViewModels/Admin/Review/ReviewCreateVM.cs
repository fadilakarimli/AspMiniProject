using System.ComponentModel.DataAnnotations;

namespace AspMiniProject.ViewModels.Admin.Review
{
    public class ReviewCreateVM
    {
        public string Description { get; set; }
        [Required]
        public int CustomerId { get; set; }

    }
}
