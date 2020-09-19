using Microsoft.AspNetCore.Identity;
using PlatformFramework.EFCore.Abstractions;

namespace PlatformFramework.EFCore.Identity.Entities
{
    public class UserClaim : IdentityUserClaim<int>, IEntity
    {
    }
}
