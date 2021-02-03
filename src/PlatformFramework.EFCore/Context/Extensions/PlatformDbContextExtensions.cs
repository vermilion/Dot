using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PlatformFramework.EFCore.Context.Hooks;
using PlatformFramework.EFCore.Context.Hooks.Interfaces;
using PlatformFramework.EFCore.Exceptions;
using DbException = PlatformFramework.EFCore.Exceptions.DbException;

namespace PlatformFramework.EFCore.Context.Extensions
{
    public static class PlatformDbContextExtensions
    {
        /// <summary>
        /// Context extension method with hooks intercepting save operation
        /// </summary>
        /// <param name="context">Context instance <see cref="DbContext"/></param>
        /// <param name="onSaveCompleted">Once save done this method is executed</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns>Task with number of entities changed</returns>
        public static async Task<int> SaveChangesWithHooks(
            this DbContext context,
            Action<EntityChangeContext>? onSaveCompleted = null,
            CancellationToken cancellationToken = default)
        {
            int result;
            try
            {
                var entryList = context.FindChangedEntries();
                var names = entryList.FindEntityNames();
                var hooks = context.GetService<IEnumerable<IEntityHook>>();

                await ExecuteHooks(hooks, entryList, HookPosition.Before);

                context.ChangeTracker.AutoDetectChangesEnabled = false;
                result = await context.SaveChangesAsync(true, cancellationToken);
                context.ChangeTracker.AutoDetectChangesEnabled = true;

                await ExecuteHooks(hooks, entryList, HookPosition.After);

                //for RowIntegrity scenarios
                await context.SaveChangesAsync(true, cancellationToken);

                onSaveCompleted?.Invoke(new EntityChangeContext(names, entryList));
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message, e);
            }
            catch (DbUpdateException e)
            {
                throw new DbException(e.Message, e);
            }

            return result;
        }

        private static async Task ExecuteHooks(IEnumerable<IEntityHook> hooksList, IEnumerable<EntityEntry> entryList, HookPosition position)
        {
            foreach (var entry in entryList)
            {
                var hooks = hooksList.Where(hook => hook.HookState == entry.State);

                foreach (var hook in hooks)
                {
                    var metadata = new HookEntityMetadata(entry);

                    switch (position)
                    {
                        case HookPosition.Before:
                            {
                                await hook.BeforeSaveChanges(entry.Entity, metadata);
                                break;
                            }
                        case HookPosition.After:
                            {
                                await hook.AfterSaveChanges(entry.Entity, metadata);
                                break;
                            }
                        default:
                            throw new ArgumentOutOfRangeException(nameof(position), position, null);
                    }
                }
            }
        }
    }
}
