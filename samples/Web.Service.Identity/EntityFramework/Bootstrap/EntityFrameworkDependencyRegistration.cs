using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cofoundry.BasicTestSite;
using Cofoundry.Core.DependencyInjection;
using Cofoundry.Core.EntityFramework.Internal;

namespace Cofoundry.Core.EntityFramework.Registration
{
    public class EntityFrameworkDependencyRegistration : IDependencyRegistration
    {
        public void Register(IContainerRegister container)
        {
            container
                .Register<ICofoundryDbContextInitializer, DbContextInitializer>();
        }
    }
}
