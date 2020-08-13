using System.Threading.Tasks;
using Ardalis.GuardClauses;
using PlatformFramework.Abstractions;
using PlatformFramework.EFCore.Context.Hooks.Interfaces;
using PlatformFramework.Extensions;

namespace PlatformFramework.EFCore.Context.Hooks.PredefinedHooks
{
    internal sealed class TrackUpdatedHook : EntityUpdatedHook<ITrackUpdated>
    {
        private readonly IUserSession _session;
        private readonly IClockProvider _clock;

        public TrackUpdatedHook(IUserSession session, IClockProvider clock)
        {
            _session = Guard.Against.Null(session, nameof(session));
            _clock = Guard.Against.Null(clock, nameof(clock));
        }

        protected override Task BeforeSaveChanges(ITrackUpdated entity, HookEntityMetadata metadata)
        {
            entity.Updated = _clock.Now;
            entity.UpdatedBy = _session.UserId?.To<long>();

            return Task.CompletedTask;
        }

        protected override Task AfterSaveChanges(ITrackUpdated entity, HookEntityMetadata metadata)
        {
            return Task.CompletedTask;
        }
    }
}