using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PlatformFramework.EFCore.Identity.Abstrations;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using PlatformFramework.Eventing;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.EFCore.Identity.Features.Account
{
    public class RefreshTokenResponse
    {
        public class Success : RefreshTokenResponse
        {
            public Success(TokenResponse model)
            {
                Model = model;
            }

            public TokenResponse Model { get; }
        }

        public class Unauthorized : RefreshTokenResponse
        {
            public Unauthorized()
            {
            }

            public Unauthorized(string model)
            {
                Model = model;
            }

            public string? Model { get; }
        }
    }
}
