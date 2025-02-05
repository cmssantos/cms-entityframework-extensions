# Cms.EntityFramework.Extensions

## Overview

Cms.EntityFramework.Extensions is a lightweight, flexible library that extends Entity Framework Core functionality. It implements the Repository and Unit of Work patterns, providing a robust foundation for data access in .NET applications.

## Features

-   Generic Repository pattern implementation
-   Unit of Work pattern for transaction management
-   Specification pattern for complex queries
-   Separation of read and write operations
-   Asynchronous operations support

## Installation

Install the package via NuGet:

```
dotnet add package Cms.EntityFramework.Extensions
```

## Usage

### Configuring Services

Add the following to your `Startup.cs` or `Program.cs`:

```
services.AddEntityFrameworkExtensions();
```

### Repositories

The library provides `IReadRepository<T>` and `IWriteRepository<T>` interfaces for read and write operations respectively.

#### Read Repository

```csharp
public interface IReadRepository<T> where T : class
{
    Task<T?> GetByIdAsync(object id);
    IQueryable<T> Find(ISpecification<T> spec);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    Task<int> CountAsync(Expression<Func<T, bool>> predicate);
    Task<List<T>> ListAsync(ISpecification<T> spec);
}
```

#### Write Repository

```csharp
public interface IWriteRepository<T> where T : class
{
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    void Update(T entity);
    void Remove(T entity);
    Task RemoveByIdAsync(object id);
}
```

### Unit of Work

The `IUnitOfWork` interface provides methods for committing or rolling back changes:

```csharp
public interface IUnitOfWork
{
    Task<int> CommitAsync(CancellationToken cancellationToken = default);
    Task RollbackAsync();
}
```

### Specifications

Use the `ISpecification<T>` interface to create complex, reusable queries:

```csharp
public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    List<string> IncludeStrings { get; }
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }
    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }
}
```

## Example

```csharp
public class ProductService
{
    private readonly IReadRepository<Product> _readRepo;
    private readonly IWriteRepository<Product> _writeRepo;
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IReadRepository<Product> readRepo, IWriteRepository<Product> writeRepo, IUnitOfWork unitOfWork)
    {
        _readRepo = readRepo;
        _writeRepo = writeRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _readRepo.GetByIdAsync(id);
    }

    public async Task AddProductAsync(Product product)
    {
        await _writeRepo.AddAsync(product);
        await _unitOfWork.CommitAsync();
    }
}
```

## Limitations

-   The `RollbackAsync` method in `UnitOfWork` can only undo changes that haven't been committed to the database.
-   Custom repositories with additional methods should be implemented separately if needed.

## Contributing

Contributions to Cms.EntityFramework.Extensions are welcome. Please submit pull requests with any enhancements, bug fixes or suggestions.

## License

This project is licensed under the APACHE License - see the LICENSE file for details.
