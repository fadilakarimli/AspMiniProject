using System.ComponentModel.DataAnnotations;

namespace AspMiniProject.ViewModels.Admin.Customer
{
    public class CustomerCreateVM
    {
        [Required(ErrorMessage = "Full Name is required.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Image is required.")]
        public IFormFile Image { get; set; }

      
    }
}
