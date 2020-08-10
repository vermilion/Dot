using System;

namespace PlatformFramework.EFCore.Context.Hooks.Interfaces
{
    public interface ICreationTrackable
    {
        public DateTime? CreatedDateTime { get; set; }

        public long? CreatedByUserId { get; set; }
    }
}