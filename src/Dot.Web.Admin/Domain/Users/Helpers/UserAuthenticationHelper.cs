using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cofoundry.Domain.Data;

namespace Cofoundry.Domain.Internal
{
    /// <summary>
    /// Shared user authentication methods
    /// </summary>
    public class UserAuthenticationHelper
    {
        private readonly IPasswordCryptographyService _cryptographyService;

        public UserAuthenticationHelper(
            IPasswordCryptographyService cryptographyService
            )
        {
            _cryptographyService = cryptographyService;
        }

        public bool IsPasswordCorrect(User user, string password)
        {
            if (user == null) return false;

            if (string.IsNullOrWhiteSpace(user.Password))
            {
                throw new InvalidOperationException("Cannot authenticate via password because the specified account does not have a password set.");
            }

            bool result = _cryptographyService.Verify(password, user.Password);

            return result;
        }
    }
}
