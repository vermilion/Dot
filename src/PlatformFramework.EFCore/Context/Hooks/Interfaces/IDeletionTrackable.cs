using System;

namespace PlatformFramework.EFCore.Context.Hooks.Interfaces
{
    public interface IDeletionTrackable
    {
        public DateTime? DeletedDateTime { get; set; }

        public long? DeletedByUserId { get; set; }
    }
}