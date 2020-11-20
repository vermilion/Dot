using PlatformFramework.Models;
using System;

namespace PlatformFramework.EFCore.Identity.Models
{
    public class UserListModel : ReadModel
    {
        /// <summary>
        /// Gets or sets a flag indicating if a user has confirmed their telephone address
        /// </summary>
        public bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// Gets or sets a telephone number for the user
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the date and time, in UTC, when any user lockout ends
        /// Remarks: A value in the past means the user is not locked out
        /// </summary>
        public DateTimeOffset? LockoutEnd { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating if the user could be locked out
        /// </summary>
        public bool LockoutEnabled { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating if a user has confirmed their email address
        /// </summary>
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// Gets or sets the email address for this user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the number of failed login attempts for the current user
        /// </summary>
        public int AccessFailedCount { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating if two factor authentication is enabled for this user
        /// </summary>
        public bool TwoFactorEnabled { get; set; }

        /// <summary>
        /// Gets or sets the user name for this user
        /// </summary>
        public string UserName { get; set; }
    }
}
