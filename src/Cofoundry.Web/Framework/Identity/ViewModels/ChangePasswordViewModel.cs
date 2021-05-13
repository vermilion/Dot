using System.ComponentModel.DataAnnotations;

namespace Cofoundry.Web.Identity
{
    public class ChangePasswordViewModel
    {
        [DataType(DataType.Text)]
        [Required]
        //[EmailAddress(ErrorMessage = "Please use a valid email address")]
        [Display(Name = "Email")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Current password")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required]
        [Display(Name = "New password")]
        [StringLength(300, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [Display(Name = "Confirm new password")]
        [StringLength(300, MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Password does not match")]
        public string ConfirmNewPassword { get; set; }

        /// <summary>
        /// Indicates if the password change is madatory (e.g. after first login)
        /// </summary>
        public bool IsPasswordChangeRequired { get; set; }
    }
}