using Cofoundry.Core.DependencyInjection;

namespace Cofoundry.Web
{
    public class DotStartupBuilder
    {
        internal ICollection<IDependencyRegistration> Registrations { get; } = new List<IDependencyRegistration>();

        public void RegisterModule<T>(T registration)
            where T : IDependencyRegistration, new()
        {
            Registrations.Add(registration);
        }
    }
}