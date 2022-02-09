﻿using Cofoundry.Core.Data.Internal;
using Cofoundry.Core.DependencyInjection;

namespace Cofoundry.Core.Data.Registration
{
    public class DataTimeDependencyRegistration : IDependencyRegistration
    {
        public void Register(IContainerRegister container)
        {
            container
                .RegisterScoped<ITransactionScopeManager, DefaultTransactionScopeManager>()
                .RegisterScoped<ITransactionScopeFactory, TransactionScopeFactory>()
                ;
        }
    }
}
