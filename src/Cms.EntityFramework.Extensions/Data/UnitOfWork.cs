namespace Cms.EntityFramework.Extensions.Data;

using Microsoft.EntityFrameworkCore;
using Cms.EntityFramework.Extensions.Core;

/// <summary>
/// Implements the Unit of Work pattern for managing database transactions.
/// </summary>
/// <remarks>
/// Initializes a new instance of the UnitOfWork class.
/// </remarks>
/// <param name="context">The DbContext to be used for database operations.</param>
public class UnitOfWork(DbContext context) : IUnitOfWork
{
    private readonly DbContext _context = context;

    /// <summary>
    /// Asynchronously saves all changes made in this unit of work to the database.
    /// </summary>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>The number of state entries written to the database.</returns>
    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);

    /// <summary>
    /// Asynchronously undoes all changes made in this unit of work that haven't been saved to the database.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task RollbackAsync()
    {
        foreach (var entry in _context.ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
                case EntityState.Modified:
                case EntityState.Deleted:
                    await entry.ReloadAsync();
                    break;
            }
        }
    }
}
