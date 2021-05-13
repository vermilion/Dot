using System.ComponentModel.DataAnnotations;

namespace Cofoundry.Web.Identity
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Username { get; set; }
    }
}