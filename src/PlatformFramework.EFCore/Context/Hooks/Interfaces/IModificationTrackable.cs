using System;

namespace PlatformFramework.EFCore.Context.Hooks.Interfaces
{
    /// <summary>
    /// Implement this interface for modification-tracking scenarios
    /// </summary>
    public interface IModificationTrackable
    {
        /// <summary>
        /// Date the entity was modified
        /// </summary>
        public DateTime? ModifiedDateTime { get; set; }

        /// <summary>
        /// User ID when entity was last modified
        /// </summary>
        public long? ModifiedByUserId { get; set; }
    }
}