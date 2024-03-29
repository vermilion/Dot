﻿using Dot.EFCore.Transactions.Services.Interfaces;

namespace Cofoundry.Domain
{
    /// <summary>
    /// DomainRepository wrapper around ITransactionScopeManager.
    /// </summary>
    public interface IDomainRepositoryTransactionManager
    {
        /// <summary>
        /// Creates a new transaction scope associated with the connection in use by the
        /// specified dbContext instance. The scope can be nested inside another scope in 
        /// which case the underlying db transaction is only committed once both the outer
        /// and inner transaction(s) have been committed. The returned ITransactionScope 
        /// implements IDisposable and should be wrapped in a using statement.
        /// </summary>
        /// <returns>ITransactionScope, which is IDisposable and must be disposed.</returns>
        ITransactionScope CreateScope();

        /// <summary>
        /// <para>
        /// Adds a function to a queue that will be processed once the ambient primary 
        /// transaction scope has completed, if there is no primary transaction scope then 
        /// the function is executed immediately. In a nested-transaction scenario the 
        /// queue is only processed once the outer primary transaction is complete. This 
        /// is useful to ensure the correct execution of code that should only run after a 
        /// transaction is fully complete, irrespective of any parent transaction scopes. 
        /// </para>
        /// <para>
        /// Cache clearing and message publishing are the two main examples of code that 
        /// needs to be queued to run after a transaction is complete. E.g. if a parent
        /// transaction scope rolls back, the child scope will roll back and any queued 
        /// completion tasks in the child scope should not be executed.
        /// </para>
        /// </summary>
        /// <param name="actionToQueue">
        /// An async action to add to the completion task queue. Actions will be
        /// processed in order once the primary (top level) transaction scope has been 
        /// completed or immediately if there is no primary transaction scope. All 
        /// completion tasks are executed outside of the transaction scope.
        /// </param>
        Task QueueCompletionTaskAsync(Func<Task> actionToQueue);
    }
}
