using Cofoundry.Core.DependencyInjection;
using Cofoundry.Core.EntityFramework.Internal;
using Cofoundry.Domain.Data;
using Cofoundry.Domain.Data.Internal;
using Microsoft.EntityFrameworkCore;
using PlatformFramework.EFCore.Abstractions;
using PlatformFramework.EFCore.Context;
using System;

namespace Cofoundry.Core.EntityFramework.Registration
{
    public class EntityFrameworkDependencyRegistration : IDependencyRegistration
    {
        public void Register(IContainerRegister container)
        {
            container
                .Register<CofoundryDbContext>(new Type[] { typeof(CofoundryDbContext), typeof(DbContext) }, RegistrationOptions.Scoped())
                .Register<IUnitOfWork, UnitOfWork>()
                //.Register<IDbContextInitializer, DbContextInitializer>()
                .Register<IDbUnstructuredDataSerializer, DbUnstructuredDataSerializer>()
                ;
        }
    }
}
