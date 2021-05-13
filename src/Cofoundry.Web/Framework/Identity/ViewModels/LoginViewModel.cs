using System.ComponentModel.DataAnnotations;

namespace Cofoundry.Web.Identity
{
    public class LoginViewModel
    {
        [DataType(DataType.Text)]
        [Required]
        [Display(Name = "Email")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please provide your password")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}