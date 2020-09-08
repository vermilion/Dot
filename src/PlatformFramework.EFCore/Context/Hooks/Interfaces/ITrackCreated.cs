using System;

namespace PlatformFramework.EFCore.Context.Hooks.Interfaces
{
    /// <summary>
    /// Implement this interface for creation-tracking scenarios
    /// </summary>
    public interface ITrackCreated
    {
        /// <summary>
        /// Date the entity was created
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// User ID when entity was created
        /// </summary>
        public int? CreatedBy { get; set; }
    }
}