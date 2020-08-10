using System;

namespace PlatformFramework.EFCore.Context.Hooks.Interfaces
{
    public interface IModificationTrackable
    {
        public DateTime? ModifiedDateTime { get; set; }

        public long? ModifiedByUserId { get; set; }
    }
}