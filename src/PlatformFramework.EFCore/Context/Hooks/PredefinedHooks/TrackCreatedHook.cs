using Ardalis.GuardClauses;
using PlatformFramework.Abstractions;
using PlatformFramework.EFCore.Context.Hooks.Interfaces;
using PlatformFramework.Extensions;
using System.Threading.Tasks;

namespace PlatformFramework.EFCore.Context.Hooks.PredefinedHooks
{
    internal sealed class TrackCreatedHook : EntityCreatedHook<ITrackCreated>
    {
        private readonly IUserSession _session;
        private readonly IClockProvider _clock;

        public TrackCreatedHook(IUserSession session, IClockProvider clock)
        {
            _session = Guard.Against.Null(session, nameof(session));
            _clock = Guard.Against.Null(clock, nameof(clock));
        }

        protected override Task BeforeSaveChanges(ITrackCreated entity, HookEntityMetadata metadata)
        {
            entity.Created = _clock.Now;
            entity.CreatedBy = _session.UserId?.To<int>();

            return Task.CompletedTask;
        }

        protected override Task AfterSaveChanges(ITrackCreated entity, HookEntityMetadata metadata)
        {
            return Task.CompletedTask;
        }
    }
}