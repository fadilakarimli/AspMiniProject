using System.ComponentModel.DataAnnotations;

namespace AspMiniProject.ViewModels.UI.Account
{
    public class LoginVM
    {
        [Required]
        public string EmailOrUserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
