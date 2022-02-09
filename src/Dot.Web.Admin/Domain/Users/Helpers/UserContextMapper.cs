﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cofoundry.Domain.Data;

namespace Cofoundry.Domain.Internal
{
    public class UserContextMapper
    {
        public UserContext Map(User dbUser)
        {
            if (dbUser == null) return null;

            if (dbUser.Role == null)
            {
                throw new ArgumentException("User role is null. Ensure this has been included in the query.", nameof(dbUser));
            }

            var cx = new UserContext();

            cx.IsPasswordChangeRequired = dbUser.RequirePasswordChange;
            cx.RoleId = dbUser.RoleId;
            cx.RoleCode = dbUser.Role.RoleCode;
            cx.UserId = dbUser.UserId;

            return cx;
        }
    }
}
