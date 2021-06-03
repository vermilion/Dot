using Cofoundry.Core;
using Cofoundry.Domain.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cofoundry.Domain.Internal
{
    /// <summary>
    /// Simple mapper for mapping to UserMicroSummary objects.
    /// </summary>
    public class UserMicroSummaryMapper : IUserMicroSummaryMapper
    {
        public UserMicroSummaryMapper()
        {
        }

        /// <summary>
        /// Maps an EF user record from the db into a UserMicroSummary object. If the
        /// db record is null then null is returned.
        /// </summary>
        /// <param name="dbUser">User record from the database.</param>
        public virtual UserMicroSummary Map(User dbUser)
        {
            if (dbUser == null) return null;

            var user = new UserMicroSummary()
            {
                Email = dbUser.Email,
                FirstName = dbUser.FirstName,
                LastName = dbUser.LastName,
                UserId = dbUser.UserId,
                Username = dbUser.Username
            };

            return user;
        }
    }
}
