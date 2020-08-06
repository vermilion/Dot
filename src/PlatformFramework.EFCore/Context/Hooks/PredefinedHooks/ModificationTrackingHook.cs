using System;
using System.Threading.Tasks;
using PlatformFramework.Abstractions;
using PlatformFramework.Extensions;

namespace PlatformFramework.EFCore.Context.Hooks.PredefinedHooks
{
    internal sealed class ModificationTrackingHook : DbContextUpdateEntityHook
    {
        private readonly IUserSession _session;
        private readonly IClockProvider _clock;

        public ModificationTrackingHook(IUserSession session, IClockProvider clock)
        {
            _session = session ?? throw new ArgumentNullException(nameof(session));
            _clock = clock ?? throw new ArgumentNullException(nameof(clock));
        }

        public override bool CanHook(EntityConfigFlags flags)
        {
            return flags.Has(EfCore.IsModificationTrackingEnabled);
        }

        public override Task BeforeSaveChanges(object entity, HookEntityMetadata metadata)
        {
            metadata.Entry.Property(EfCore.ModifiedDateTime).CurrentValue = _clock.Now;
            metadata.Entry.Property(EfCore.ModifiedByUserId).CurrentValue = _session.UserId?.To<long>();

            return Task.CompletedTask;
        }

        public override Task AfterSaveChanges(object entity, HookEntityMetadata metadata)
        {
            return Task.CompletedTask;
        }
    }
}