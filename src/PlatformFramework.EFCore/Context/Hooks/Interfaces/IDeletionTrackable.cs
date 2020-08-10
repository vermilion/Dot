using System;

namespace PlatformFramework.EFCore.Context.Hooks.Interfaces
{
    /// <summary>
    /// Implement this interface for deletion-tracking scenarios
    /// </summary>
    public interface IDeletionTrackable
    {
        /// <summary>
        /// Date the entity was deleted
        /// </summary>
        public DateTime? DeletedDateTime { get; set; }

        /// <summary>
        /// User ID when entity was deleted
        /// </summary>
        public long? DeletedByUserId { get; set; }
    }
}