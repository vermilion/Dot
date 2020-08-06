using System;
using System.Threading.Tasks;
using PlatformFramework.Interfaces.Runtime;
using PlatformFramework.Interfaces.Timing;
using PlatformFramework.Shared.Extensions;

namespace PlatformFramework.EFCore.Context.Hooks.PrefefinedHooks
{
    internal sealed class DeletionTrackingHook : DbContextDeleteEntityHook
    {
        private readonly IUserSession _session;
        private readonly IClockProvider _clock;

        public DeletionTrackingHook(IUserSession session, IClockProvider clock)
        {
            _session = session ?? throw new ArgumentNullException(nameof(session));
            _clock = clock ?? throw new ArgumentNullException(nameof(clock));
        }

        public override bool CanHook(EntityConfigFlags flags)
        {
            return flags.Has(EfCore.IsSoftDeleteEnabled) && flags.Has(EfCore.IsDeletionTrackingEnabled);
        }

        public override Task BeforeSaveChanges(object entity, HookEntityMetadata metadata)
        {
            metadata.Entry.Property(EfCore.DeletedDateTime).CurrentValue = _clock.Now;
            metadata.Entry.Property(EfCore.DeletedByUserId).CurrentValue = _session.UserId.To<long>();

            return Task.CompletedTask;
        }

        public override Task AfterSaveChanges(object entity, HookEntityMetadata metadata)
        {
            return Task.CompletedTask;
        }
    }
}