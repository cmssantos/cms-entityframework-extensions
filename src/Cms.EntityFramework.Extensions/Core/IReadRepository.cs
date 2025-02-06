using System.Linq.Expressions;
using Cms.EntityFramework.Extensions.Data.Specifications;

namespace Cms.EntityFramework.Extensions.Core;

/// <summary>
/// Generic interface for read operations in a repository.
/// </summary>
/// <typeparam name="T">The type of entity the repository works with.</typeparam>
public interface IReadRepository<T> where T : class
{
    /// <summary>
    /// Retrieves an entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity.</param>
    /// <returns>The entity if found, or null if it doesn't exist.</returns>
    Task<T?> GetByIdAsync(object id);

    /// <summary>
    /// Returns an IQueryable based on the provided specification.
    /// </summary>
    /// <param name="spec">The specification defining the search criteria.</param>
    /// <returns>An IQueryable that can be used for further querying.</returns>
    IQueryable<T> Find(ISpecification<T> spec);

    /// <summary>
    /// Checks if any entity satisfies the provided predicate.
    /// </summary>
    /// <param name="predicate">The predicate to check for existence.</param>
    /// <returns>True if any entity satisfies the predicate, otherwise False.</returns>
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Counts the number of entities that satisfy the provided predicate.
    /// </summary>
    /// <param name="predicate">The predicate to count the entities.</param>
    /// <returns>The number of entities that satisfy the predicate.</returns>
    Task<int> CountAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Returns a list of entities based on the provided specification.
    /// </summary>
    /// <param name="spec">The specification defining the search criteria.</param>
    /// <returns>A list of entities that satisfy the specification.</returns>
    Task<List<T>> ListAsync(ISpecification<T> spec);
}
