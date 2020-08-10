namespace PlatformFramework.EFCore.Context.Hooks.Interfaces
{
    public interface ISoftDeletable : IDeletionTrackable
    {
        public bool IsDeleted { get; set; }
    }
}