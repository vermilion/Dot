namespace PlatformFramework.EFCore.Context.Hooks.Interfaces
{
    /// <summary>
    /// Hook execution position
    /// </summary>
    public enum HookPosition
    {
        /// <summary>
        /// Before SaveChanges
        /// </summary>
        Before,

        /// <summary>
        /// After SaveChanges
        /// </summary>
        After
    }
}