using Cms.EntityFramework.Extensions.Core;
using Microsoft.EntityFrameworkCore;

namespace Cms.EntityFramework.Extensions.Data.Repositories;

/// <summary>
/// Implements the IWriteRepository interface for write operations using Entity Framework Core.
/// </summary>
/// <typeparam name="T">The type of entity this repository works with.</typeparam>
/// <remarks>
/// Initializes a new instance of the WriteRepository class.
/// </remarks>
/// <param name="context">The DbContext to be used for database operations.</param>
internal class WriteRepository<T>(DbContext context) : IWriteRepository<T> where T : class
{
    private readonly DbContext _context = context;

    /// <summary>
    /// Asynchronously adds a new entity to the database.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    public async Task AddAsync(T entity)
        => await _context.Set<T>().AddAsync(entity);

    /// <summary>
    /// Asynchronously adds a range of new entities to the database.
    /// </summary>
    /// <param name="entities">The collection of entities to add.</param>
    public async Task AddRangeAsync(IEnumerable<T> entities)
        => await _context.Set<T>().AddRangeAsync(entities);

    /// <summary>
    /// Removes an existing entity from the database.
    /// </summary>
    /// <param name="entity">The entity to remove.</param>
    public void Remove(T entity)
        => _context.Set<T>().Remove(entity);

    /// <summary>
    /// Asynchronously removes an entity from the database by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity to remove.</param>
    public async Task RemoveByIdAsync(object id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
        }
    }

    /// <summary>
    /// Updates an existing entity in the database.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    public void Update(T entity)
        => _context.Set<T>().Update(entity);
}
