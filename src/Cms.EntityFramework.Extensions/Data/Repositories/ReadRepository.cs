using System.Linq.Expressions;
using Cms.EntityFramework.Extensions.Core;
using Cms.EntityFramework.Extensions.Data.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Cms.EntityFramework.Extensions.Data.Repositories;

/// <summary>
/// Implements the IReadRepository interface for read operations using Entity Framework Core.
/// </summary>
/// <typeparam name="T">The type of entity this repository works with.</typeparam>
/// <remarks>
/// Initializes a new instance of the ReadRepository class.
/// </remarks>
/// <param name="context">The DbContext to be used for database operations.</param>
internal class ReadRepository<T>(DbContext context) : IReadRepository<T> where T : class
{
    private readonly DbContext _context = context;

    /// <summary>
    /// Retrieves an entity by its id.
    /// </summary>
    /// <param name="id">The id of the entity to retrieve.</param>
    /// <returns>The entity if found, otherwise null.</returns>
    public async Task<T?> GetByIdAsync(object id)
        => await _context.Set<T>().FindAsync(id);

    /// <summary>
    /// Finds entities based on the given specification.
    /// </summary>
    /// <param name="spec">The specification to apply.</param>
    /// <returns>An IQueryable of entities matching the specification.</returns>
    public IQueryable<T> Find(ISpecification<T> spec)
        => ApplySpecification(spec).AsNoTracking();

    /// <summary>
    /// Checks if any entity matches the given predicate.
    /// </summary>
    /// <param name="predicate">The condition to check.</param>
    /// <returns>True if any entity matches, otherwise false.</returns>
    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        => await _context.Set<T>().AsNoTracking().AnyAsync(predicate);

    /// <summary>
    /// Counts entities that match the given predicate.
    /// </summary>
    /// <param name="predicate">The condition to count.</param>
    /// <returns>The number of entities matching the predicate.</returns>
    public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        => await _context.Set<T>().AsNoTracking().CountAsync(predicate);

    /// <summary>
    /// Retrieves a list of entities based on the given specification.
    /// </summary>
    /// <param name="spec">The specification to apply.</param>
    /// <returns>A list of entities matching the specification.</returns>
    public async Task<List<T>> ListAsync(ISpecification<T> spec)
        => await ApplySpecification(spec).AsNoTracking().ToListAsync();

    /// <summary>
    /// Applies the given specification to the entity set.
    /// </summary>
    /// <param name="spec">The specification to apply.</param>
    /// <returns>An IQueryable with the specification applied.</returns>
    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        => SpecificationEvaluator.GetQuery(_context.Set<T>().AsQueryable(), spec);
}

