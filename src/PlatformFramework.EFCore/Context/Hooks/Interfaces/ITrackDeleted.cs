using System;

namespace PlatformFramework.EFCore.Context.Hooks.Interfaces
{
    /// <summary>
    /// Implement this interface for deletion-tracking scenarios
    /// </summary>
    public interface ITrackDeleted
    {
        /// <summary>
        /// Flag if entity is deleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Date the entity was deleted
        /// </summary>
        public DateTime? Deleted { get; set; }

        /// <summary>
        /// User ID when entity was deleted
        /// </summary>
        public long? DeletedBy { get; set; }
    }
}