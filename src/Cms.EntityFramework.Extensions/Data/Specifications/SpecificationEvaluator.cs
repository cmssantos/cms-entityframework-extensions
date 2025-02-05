using Microsoft.EntityFrameworkCore;

namespace Cms.EntityFramework.Extensions.Data.Specifications;

/// <summary>
/// Provides functionality to evaluate and apply specifications to queries.
/// </summary>
internal static class SpecificationEvaluator
{
    /// <summary>
    /// Applies a specification to a query, transforming it based on the specification's criteria.
    /// </summary>
    /// <typeparam name="T">The type of entity in the query.</typeparam>
    /// <param name="inputQuery">The initial IQueryable to transform.</param>
    /// <param name="specification">The specification to apply to the query.</param>
    /// <returns>An IQueryable with the specification applied.</returns>
    public static IQueryable<T> GetQuery<T>(IQueryable<T> inputQuery, ISpecification<T> specification) where T : class
    {
        var query = inputQuery;

        // Apply criteria
        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        // Include related entities
        query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

        // Include related entities (string-based)
        query = specification.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

        // Apply ordering
        if (specification.OrderBy != null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        else if (specification.OrderByDescending != null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }

        // Apply paging
        if (specification.IsPagingEnabled)
        {
            query = query.Skip(specification.Skip).Take(specification.Take);
        }

        return query;
    }
}
