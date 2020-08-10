using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace PlatformFramework.EFCore.Context.Hooks.Interfaces
{
    /// <summary>
    /// A 'hook' usable for calling at certain point in an entities life cycle.
    /// </summary>
    public interface IEntityHook
    {
        /// <summary>
        /// Gets the entity state(s) to listen for.
        /// </summary>
        EntityState HookState { get; }

        /// <summary>
        /// Executes the logic in the hook.
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <param name="metadata">The metadata</param>
        Task BeforeSaveChanges(object entity, HookEntityMetadata metadata);

        /// <summary>
        /// Executes the logic in the hook.
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <param name="metadata">The metadata</param>
        Task AfterSaveChanges(object entity, HookEntityMetadata metadata);
    }
}