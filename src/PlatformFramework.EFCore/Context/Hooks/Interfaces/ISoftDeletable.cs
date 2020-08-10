namespace PlatformFramework.EFCore.Context.Hooks.Interfaces
{
    /// <summary>
    /// Implement this interface for soft-deletion-tracking scenarios
    /// </summary>
    public interface ISoftDeletable : IDeletionTrackable
    {
        /// <summary>
        /// Flag if entity is deleted
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}