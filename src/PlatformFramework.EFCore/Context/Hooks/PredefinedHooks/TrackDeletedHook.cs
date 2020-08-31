using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using PlatformFramework.Abstractions;
using PlatformFramework.EFCore.Context.Hooks.Interfaces;
using PlatformFramework.Extensions;
using System.Threading.Tasks;

namespace PlatformFramework.EFCore.Context.Hooks.PredefinedHooks
{
    internal sealed class TrackDeletedHook : EntityDeletedHook<ITrackDeleted>
    {
        private readonly IUserSession _session;
        private readonly IClockProvider _clock;

        public TrackDeletedHook(IUserSession session, IClockProvider clock)
        {
            _session = Guard.Against.Null(session, nameof(session));
            _clock = Guard.Against.Null(clock, nameof(clock));
        }

        protected override Task BeforeSaveChanges(ITrackDeleted entity, HookEntityMetadata metadata)
        {
            metadata.Entry.State = EntityState.Modified;

            entity.IsDeleted = true;
            entity.Deleted = _clock.Now;
            entity.DeletedBy = _session.UserId?.To<long>();

            return Task.CompletedTask;
        }

        protected override Task AfterSaveChanges(ITrackDeleted entity, HookEntityMetadata metadata)
        {
            return Task.CompletedTask;
        }
    }
}