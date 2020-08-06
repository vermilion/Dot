using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace PlatformFramework.EFCore.Context.Extensions
{
    public static class DbContextExtensions
    {
        /// <summary>
        /// Using the ChangeTracker to find names of the changed entities.
        /// </summary>
        public static IEnumerable<string> FindEntityNames(this IEnumerable<EntityEntry> entryList)
        {
            var typesList = new List<Type>();
            foreach (var type in entryList.Select(entry => entry.Entity.GetType()))
            {
                typesList.Add(type);
                typesList.AddRange(type.FindBaseTypes().Where(t => t != typeof(object)).ToList());
            }

            var changedEntityNames = typesList
                .Select(type => type.FullName)
                .Distinct()
                .ToArray();

            return changedEntityNames;
        }

        /// <summary>
        /// Using the ChangeTracker to find types of the changed entities.
        /// </summary>
        public static IEnumerable<Type> FindChangedEntityTypes(this DbContext dbContext)
        {
            return dbContext.FindChangedEntries()
                .Select(dbEntityEntry => dbEntityEntry.Entity.GetType());
        }

        /// <summary>
        /// Find the base types of the given type, recursively.
        /// </summary>
        private static IEnumerable<Type> FindBaseTypes(this Type type)
        {
            if (type.GetTypeInfo().BaseType == null) return type.GetInterfaces();

            return Enumerable.Repeat(type.GetTypeInfo().BaseType, 1)
                .Concat(type.GetInterfaces())
                .Concat(type.GetInterfaces().SelectMany(FindBaseTypes))
                .Concat(type.GetTypeInfo().BaseType.FindBaseTypes());
        }

        public static IReadOnlyList<EntityEntry> FindChangedEntries(this DbContext context)
        {
            return context.ChangeTracker
                .Entries()
                .Where(x =>
                    x.State == EntityState.Added ||
                    x.State == EntityState.Modified ||
                    x.State == EntityState.Deleted)
                .ToList();
        }
    }
}