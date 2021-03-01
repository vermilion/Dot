using Mapster;
using System.Linq;

namespace PlatformFramework.Extensions
{
    public static class MappingExtensions
    {
        public static IQueryable<TDestination> Project<TDestination>(this IQueryable query)
        {
            return query.ProjectToType<TDestination>();
        }

        public static TDestination MapTo<TDestination>(this object source)
        {
            return source.Adapt<TDestination>();
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return source.Adapt(destination);
        }
    }
}