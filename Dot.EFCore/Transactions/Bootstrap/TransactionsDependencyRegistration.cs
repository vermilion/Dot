using Cofoundry.Core.DependencyInjection;
using Cofoundry.Core.Transactions.Internal;
using Dot.EFCore.Transactions.Services.Interfaces;

namespace Cofoundry.Core.Transactions.Registration
{
    public class TransactionsDependencyRegistration : IDependencyRegistration
    {
        public void Register(IContainerRegister container)
        {
            container
                .RegisterScoped<ITransactionScopeManager, DefaultTransactionScopeManager>()
                .RegisterScoped<ITransactionScopeFactory, TransactionScopeFactory>();
        }
    }
}
