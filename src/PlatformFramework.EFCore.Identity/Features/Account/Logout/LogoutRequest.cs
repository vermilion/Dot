using Microsoft.Extensions.Logging;
using PlatformFramework.EFCore.Identity.Abstrations;
using PlatformFramework.Eventing;
using System.Threading;
using System.Threading.Tasks;

namespace PlatformFramework.EFCore.Identity.Features.Account
{
    public class LogoutRequest : IRequest<LogoutResponse>
    {
        public LogoutRequest(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
