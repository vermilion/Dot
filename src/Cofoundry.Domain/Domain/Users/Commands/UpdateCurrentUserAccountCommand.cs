using System.ComponentModel.DataAnnotations;
using Cofoundry.Domain.CQS;

namespace Cofoundry.Domain
{
    /// <summary>
    /// Updates the user account of the currently logged in user.
    /// </summary>
    public class UpdateCurrentUserAccountCommand : ICommand, ILoggableCommand
    {
        /// <summary>
        /// The first name is required.
        /// </summary>
        [Required]
        [StringLength(32)]
        public string FirstName { get; set; }

        /// <summary>
        /// The last name is required.
        /// </summary>
        [Required]
        [StringLength(32)]
        public string LastName { get; set; }

        /// <summary>
        /// The email address is required if the user area has UseEmailAsUsername 
        /// set to true.
        /// </summary>
        [StringLength(150)]
        [EmailAddress(ErrorMessage = "Please use a valid email address")]
        public string Email { get; set; }
    }
}
