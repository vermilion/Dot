using System;
using System.Threading.Tasks;
using PlatformFramework.Interfaces.Runtime;
using PlatformFramework.Interfaces.Timing;
using PlatformFramework.Shared.Extensions;

namespace PlatformFramework.EFCore.Context.Hooks.PrefefinedHooks
{
    internal sealed class CreationTrackingHook : DbContextInsertEntityHook
    {
        private readonly IUserSession _session;
        private readonly IClockProvider _clock;

        public CreationTrackingHook(IUserSession session, IClockProvider clock)
        {
            _session = session ?? throw new ArgumentNullException(nameof(session));
            _clock = clock ?? throw new ArgumentNullException(nameof(clock));
        }

        public override bool CanHook(EntityConfigFlags flags)
        {
            return flags.Has(EfCore.IsCreationTrackingEnabled);
        }

        public override Task BeforeSaveChanges(object entity, HookEntityMetadata metadata)
        {
            metadata.Entry.Property(EfCore.CreatedDateTime).CurrentValue = _clock.Now;
            metadata.Entry.Property(EfCore.CreatedByUserId).CurrentValue = _session.UserId.To<long>();

            return Task.CompletedTask;
        }

        public override Task AfterSaveChanges(object entity, HookEntityMetadata metadata)
        {
            return Task.CompletedTask;
        }
    }
}