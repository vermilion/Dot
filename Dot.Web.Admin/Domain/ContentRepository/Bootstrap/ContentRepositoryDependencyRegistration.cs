using System;
using Cofoundry.Core.DependencyInjection;
using Cofoundry.Domain.Internal;

namespace Cofoundry.Domain.Registration
{
    public class ContentRepositoryDependencyRegistration : IDependencyRegistration
    {
        public void Register(IContainerRegister container)
        {
            container.Register<ContentRepository>(new Type[] { typeof(IContentRepository), typeof(IAdvancedContentRepository), typeof(IDomainRepository) });
            container.Register<IContentRepositoryWithElevatedPermissions, ContentRepositoryWithElevatedPermissions>();
            container.Register<IContentRepositoryWithCustomExecutionContext, ContentRepositoryWithCustomExecutionContext>();
            container.Register<IDomainRepositoryTransactionManager, DomainRepositoryTransactionManager>();
            
        }
    }
}
