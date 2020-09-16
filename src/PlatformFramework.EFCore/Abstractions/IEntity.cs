namespace PlatformFramework.EFCore.Abstractions
{
    /// <summary>
    /// Basic entity marker with Id field
    /// </summary>
    public interface IEntity
    {
        int Id { get; set; }
    }
}