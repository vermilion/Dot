﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Domain
{
    /// <summary>
    /// Represents a users connection to the system at a specific point in time.
    /// </summary>
    public class UserContext : IUserContext
    {
        /// <summary>
        /// Id of the User if they are logged in.
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Indicates if the user should be required to change thier password when they log on.
        /// </summary>
        public bool IsPasswordChangeRequired { get; set; }

        /// <summary>
        /// The role that this user belongs to. If this is null then the anonymous role will be used.
        /// </summary>
        public int? RoleId { get; set; }

        /// <summary>
        /// Optional role code for the role this user belongs to. The role code indicates that the role
        /// is a code-first role.
        /// </summary>
        public string RoleCode { get; set; }
    }
}
