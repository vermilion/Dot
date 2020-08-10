using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlatformFramework.EFCore.Context.Hooks.Interfaces;

namespace PlatformFramework.EFCore.Context.Hooks.PredefinedHooks
{
    internal sealed class SoftDeleteEntityHook : DeleteEntityHook<ISoftDeletable>
    {
        protected override Task BeforeSaveChanges(ISoftDeletable entity, HookEntityMetadata metadata)
        {
            metadata.Entry.State = EntityState.Modified;
            entity.IsDeleted = true;

            return Task.CompletedTask;
        }

        protected override Task AfterSaveChanges(ISoftDeletable entity, HookEntityMetadata metadata)
        {
            return Task.CompletedTask;
        }
    }
}