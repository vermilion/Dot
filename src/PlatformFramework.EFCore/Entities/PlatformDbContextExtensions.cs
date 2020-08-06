using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PlatformFramework.EFCore.Context;
using PlatformFramework.EFCore.Context.Extensions;
using PlatformFramework.EFCore.Context.Hooks;
using PlatformFramework.EFCore.Context.Hooks.Interfaces;
using PlatformFramework.EFCore.Exceptions;
using DbException = PlatformFramework.EFCore.Exceptions.DbException;

namespace PlatformFramework.EFCore.Entities
{
    public class PlatformDbContextExtensions
    {
        private readonly DbContext _context;
        private readonly EntitiesRegistry _registry;
        private readonly IEnumerable<IDbContextEntityHook> _hooks;

        public PlatformDbContextExtensions(DbContext context)
        {
            _context = context;

            _registry = context.GetService<EntitiesRegistry>();
            _hooks = context.GetService<IEnumerable<IDbContextEntityHook>>();
        }

        public async Task<int> SaveChanges(Action<EntityChangeContext>? onSaveCompleted = null, CancellationToken cancellationToken = default)
        {
            int result;
            try
            {
                var entryList = _context.FindChangedEntries();
                var names = entryList.FindEntityNames();

                await ExecuteHooks(entryList, HookPosition.Before);

                _context.ChangeTracker.AutoDetectChangesEnabled = false;
                result = await _context.SaveChangesAsync(true, cancellationToken);
                _context.ChangeTracker.AutoDetectChangesEnabled = true;

                await ExecuteHooks(entryList, HookPosition.After);

                //for RowIntegrity scenarios
                await _context.SaveChangesAsync(true, cancellationToken);

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

        private async Task ExecuteHooks(IEnumerable<EntityEntry> entryList, HookPosition position)
        {
            foreach (var entry in entryList)
            {
                var customizer = _registry.GetCustomizer(entry.Entity.GetType());
                if (customizer == null)
                    continue;

                var hooks = _hooks
                    .Where(hook => hook.HookState == entry.State)
                    .Where(hook => hook.CanHook(customizer.Flags));

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
