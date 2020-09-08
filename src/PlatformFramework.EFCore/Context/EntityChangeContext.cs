using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

namespace PlatformFramework.EFCore.Context
{
    public class EntityChangeContext
    {
        public IEnumerable<string> EntityNames { get; }
        public IEnumerable<EntityEntry> EntityEntries { get; }

        public EntityChangeContext(IEnumerable<string> names, IEnumerable<EntityEntry> entries)
        {
            EntityNames = names;
            EntityEntries = entries;
        }
    }
}