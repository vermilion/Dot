using System.Threading.Tasks;
using Ardalis.GuardClauses;
using PlatformFramework.Abstractions;
using PlatformFramework.EFCore.Context.Hooks.Interfaces;
using PlatformFramework.Extensions;

namespace PlatformFramework.EFCore.Context.Hooks.PredefinedHooks
{
    internal sealed class DeletionTrackingHook : DeleteEntityHook<IDeletionTrackable>
    {
        private readonly IUserSession _session;
        private readonly IClockProvider _clock;

        public DeletionTrackingHook(IUserSession session, IClockProvider clock)
        {
            _session = Guard.Against.Null(session, nameof(session));
            _clock = Guard.Against.Null(clock, nameof(clock));
        }

        protected override Task BeforeSaveChanges(IDeletionTrackable entity, HookEntityMetadata metadata)
        {
            entity.DeletedDateTime = _clock.Now;
            entity.DeletedByUserId = _session.UserId?.To<long>();

            return Task.CompletedTask;
        }

        protected override Task AfterSaveChanges(IDeletionTrackable entity, HookEntityMetadata metadata)
        {
            return Task.CompletedTask;
        }
    }
}