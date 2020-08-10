using System;
using System.Threading.Tasks;
using PlatformFramework.Abstractions;
using PlatformFramework.EFCore.Context.Hooks.Interfaces;
using PlatformFramework.Extensions;

namespace PlatformFramework.EFCore.Context.Hooks.PredefinedHooks
{
    internal sealed class ModificationTrackingHook : UpdateEntityHook<IModificationTrackable>
    {
        private readonly IUserSession _session;
        private readonly IClockProvider _clock;

        public ModificationTrackingHook(IUserSession session, IClockProvider clock)
        {
            _session = session ?? throw new ArgumentNullException(nameof(session));
            _clock = clock ?? throw new ArgumentNullException(nameof(clock));
        }

        protected override Task BeforeSaveChanges(IModificationTrackable entity, HookEntityMetadata metadata)
        {
            entity.ModifiedDateTime = _clock.Now;
            entity.ModifiedByUserId = _session.UserId?.To<long>();

            return Task.CompletedTask;
        }

        protected override Task AfterSaveChanges(IModificationTrackable entity, HookEntityMetadata metadata)
        {
            return Task.CompletedTask;
        }
    }
}