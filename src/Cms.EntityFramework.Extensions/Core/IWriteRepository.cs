// Copyright(c) Cleudice Santos. All Rights Reserved. Licensed under the MIT License.  See License in the project root for license information.
namespace Cms.EntityFramework.Extensions.Core;

/// <summary>
/// Generic interface for write operations in a repository.
/// </summary>
/// <typeparam name="T">The type of entity the repository works with.</typeparam>
public interface IWriteRepository<in T> where T : class
{
    /// <summary>
    /// Asynchronously adds a new entity to the repository.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>A task that represents the asynchronous add operation.</returns>
    Task AddAsync(T entity);

    /// <summary>
    /// Asynchronously adds a range of new entities to the repository.
    /// </summary>
    /// <param name="entities">The collection of entities to add.</param>
    /// <returns>A task that represents the asynchronous add operation.</returns>
    Task AddRangeAsync(IEnumerable<T> entities);

    /// <summary>
    /// Updates an existing entity in the repository.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    void Update(T entity);

    /// <summary>
    /// Removes an existing entity from the repository.
    /// </summary>
    /// <param name="entity">The entity to remove.</param>
    void Remove(T entity);

    /// <summary>
    /// Asynchronously removes an entity from the repository by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity to remove.</param>
    /// <returns>A task that represents the asynchronous remove operation.</returns>
    Task RemoveByIdAsync(object id);
}
