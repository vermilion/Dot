using System.Threading.Tasks;
using Ardalis.GuardClauses;
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
            _session = Guard.Against.Null(session, nameof(session));
            _clock = Guard.Against.Null(clock, nameof(clock));
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