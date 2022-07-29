﻿using Dot.EFCore.Transactions.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cofoundry.Domain.Extendable
{
    public interface IExtendableDomainRepositoryTransactionManager : IDomainRepositoryTransactionManager
    {
        /// <summary>
        /// TransactionScopeManager instance only to be used for
        /// extending the transaction manager with extension methods
        /// </summary>
        ITransactionScopeManager TransactionScopeManager { get; }

        /// <summary>
        /// DbContext that can be used to apply TransactionScopeManager
        /// method to. This is only to be used for extending the transaction 
        /// manager with extension methods
        /// </summary>
        DbContext DbContext { get; }
    }
}
