using System;
using System.Threading.Tasks;
using PlatformFramework.Abstractions;
using PlatformFramework.EFCore.Context.Hooks.Interfaces;
using PlatformFramework.Extensions;

namespace PlatformFramework.EFCore.Context.Hooks.PredefinedHooks
{
    internal sealed class CreationTrackingHook : InsertEntityHook<ICreationTrackable>
    {
        private readonly IUserSession _session;
        private readonly IClockProvider _clock;

        public CreationTrackingHook(IUserSession session, IClockProvider clock)
        {
            _session = session ?? throw new ArgumentNullException(nameof(session));
            _clock = clock ?? throw new ArgumentNullException(nameof(clock));
        }

        protected override Task BeforeSaveChanges(ICreationTrackable entity, HookEntityMetadata metadata)
        {
            metadata.Entry.Property(nameof(ICreationTrackable.CreatedDateTime)).CurrentValue = _clock.Now;
            metadata.Entry.Property(nameof(ICreationTrackable.CreatedByUserId)).CurrentValue = _session.UserId?.To<long>();

            return Task.CompletedTask;
        }

        protected override Task AfterSaveChanges(ICreationTrackable entity, HookEntityMetadata metadata)
        {
            return Task.CompletedTask;
        }
    }
}