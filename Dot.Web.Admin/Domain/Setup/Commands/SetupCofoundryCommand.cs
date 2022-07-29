using Cofoundry.Domain.CQS;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Cofoundry.Domain
{
    public class SetupCofoundryCommand : IRequest<Unit>
    {
        [Required]
        [StringLength(50)]
        public string ApplicationName { get; set; }

        [Required]
        [StringLength(32)]
        public string UserFirstName { get; set; }

        [Required]
        [StringLength(32)]
        public string UserLastName { get; set; }

        [Required]
        [StringLength(150)]
        [EmailAddress(ErrorMessage = "Please use a valid email address")]
        public string UserEmail { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 8)]
        [IgnoreDataMember]
        public string UserPassword { get; set; }

        /// <summary>
        /// True if a password change should be required when the master user 
        /// first logs. The default value is false but if setting up the site
        /// programmatically then you may want to set this to be true.
        /// </summary>
        public bool RequirePasswordChange { get; set; }
    }
}
