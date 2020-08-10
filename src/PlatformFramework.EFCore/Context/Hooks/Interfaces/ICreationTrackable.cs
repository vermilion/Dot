using System;

namespace PlatformFramework.EFCore.Context.Hooks.Interfaces
{
    /// <summary>
    /// Implement this interface for creation-tracking scenarios
    /// </summary>
    public interface ICreationTrackable
    {
        /// <summary>
        /// Date the entity was created
        /// </summary>
        public DateTime CreatedDateTime { get; set; }

        /// <summary>
        /// User ID when entity was created
        /// </summary>
        public long? CreatedByUserId { get; set; }
    }
}