using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using System.Linq;

namespace Dot.EFCore.UnitOfWork
{
    public static class DbSetExtensions
    {
        public static IQueryable<TSource> WithSpecification<TSource>(this IQueryable<TSource> source, ISpecification<TSource> specification, ISpecificationEvaluator? evaluator = null) where TSource : class
        {
            evaluator ??= SpecificationEvaluator.Default;
            return evaluator.GetQuery(source, specification);
        }
    }
}