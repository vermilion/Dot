namespace PlatformFramework.EFCore.Context.Hooks.Interfaces
{
    /// <summary>
    /// Implement this interface for concurrency check scenarios
    /// </summary>
    public interface ITrackConcurrency
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        byte[] RowVersion { get; set; }
    }
}