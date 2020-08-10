using System.Threading.Tasks;
using Ardalis.GuardClauses;
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
            _session = Guard.Against.Null(session, nameof(session));
            _clock = Guard.Against.Null(clock, nameof(clock));
        }

        protected override Task BeforeSaveChanges(ICreationTrackable entity, HookEntityMetadata metadata)
        {
            entity.CreatedDateTime = _clock.Now;
            entity.CreatedByUserId = _session.UserId?.To<long>();

            return Task.CompletedTask;
        }

        protected override Task AfterSaveChanges(ICreationTrackable entity, HookEntityMetadata metadata)
        {
            return Task.CompletedTask;
        }
    }
}