namespace Cms.EntityFramework.Extensions.Core;

/// <summary>
/// Represents a unit of work for managing transactions in database operations.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Commits all changes made in a container transaction to the database.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous commit operation. The task result contains
    /// the number of state entries written to the database.
    /// </returns>
    Task<int> CommitAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Discards all changes made in this unit of work.
    /// </summary>
    /// <returns>A task that represents the asynchronous rollback operation.</returns>
    Task RollbackAsync();
}
