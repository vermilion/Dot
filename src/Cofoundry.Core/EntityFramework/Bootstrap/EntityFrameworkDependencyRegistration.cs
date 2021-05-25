using Cofoundry.Core.DependencyInjection;
using Cofoundry.Core.EntityFramework.Internal;
using Cofoundry.Domain.Data;
using Cofoundry.Domain.Data.Internal;
using Microsoft.EntityFrameworkCore;
using System;

namespace Cofoundry.Core.EntityFramework.Registration
{
    public class EntityFrameworkDependencyRegistration : IDependencyRegistration
    {
        public void Register(IContainerRegister container)
        {
            container
                .Register<CofoundryDbContext>(new Type[] { typeof(CofoundryDbContext), typeof(DbContext) }, RegistrationOptions.Scoped())
                //.Register<IDbContextInitializer, DbContextInitializer>()
                .Register<IDbUnstructuredDataSerializer, DbUnstructuredDataSerializer>()
                ;
        }
    }
}
