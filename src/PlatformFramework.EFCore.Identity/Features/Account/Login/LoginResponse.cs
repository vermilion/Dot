using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PlatformFramework.EFCore.Identity.Abstrations;
using PlatformFramework.EFCore.Identity.Entities;
using PlatformFramework.EFCore.Identity.Models;
using PlatformFramework.Eventing;
using PlatformFramework.Eventing.Decorators.DatabaseRetry;
using PlatformFramework.Eventing.Decorators.Validation;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.EFCore.Identity.Features.Account
{

    public class LoginResponse
    {
        public class Success : LoginResponse
        {
            public Success(TokenResponse model, string refreshToken)
            {
                Model = model;
                RefreshToken = refreshToken;
            }

            public TokenResponse Model { get; }

            public string RefreshToken { get; }
        }

        public class NotFound : LoginResponse
        {
        }

        public class BadRequest : LoginResponse
        {
            public BadRequest(LoginRequest model)
            {
                Model = model;
            }

            public LoginRequest Model { get; }
        }
    }
}
