using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PlatformFramework.EFCore.Context.Hooks.PredefinedHooks
{
    internal sealed class SoftDeleteEntityHook : DbContextDeleteEntityHook
    {
        public override bool CanHook(EntityConfigFlags flags)
        {
            return flags.Has(EfCore.IsSoftDeleteEnabled);
        }

        public override Task BeforeSaveChanges(object entity, HookEntityMetadata metadata)
        {
            metadata.Entry.State = EntityState.Modified;
            metadata.Entry.Property(EfCore.IsDeleted).CurrentValue = true;

            return Task.CompletedTask;
        }

        public override Task AfterSaveChanges(object entity, HookEntityMetadata metadata)
        {
            return Task.CompletedTask;
        }
    }
}