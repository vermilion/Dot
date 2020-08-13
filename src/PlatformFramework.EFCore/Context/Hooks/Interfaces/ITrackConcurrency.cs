namespace PlatformFramework.EFCore.Context.Hooks.Interfaces
{
    public interface ITrackConcurrency
    {
        byte[] RowVersion { get; set; }
    }
}