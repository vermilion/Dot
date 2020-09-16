using Microsoft.AspNetCore.Identity;
using PlatformFramework.EFCore.Abstractions;

namespace PlatformFramework.EFCore.Identity.Entities
{
    public class User : IdentityUser<int>, IEntity
    {
    }
}
