using System.Linq.Expressions;

// Copyright(c) Cleudice Santos. All Rights Reserved. Licensed under the MIT License.  See License in the project root for license information.
namespace Cms.EntityFramework.Extensions.Data.Specifications;

/// <summary>
/// Defines a specification pattern interface for querying data.
/// </summary>
/// <typeparam name="T">The type of entity this specification applies to.</typeparam>
public interface ISpecification<T>
{
    /// <summary>
    /// Gets the filter criteria for the specification.
    /// </summary>
    Expression<Func<T, bool>>? Criteria { get; }

    /// <summary>
    /// Gets a list of include expressions for eager loading related entities.
    /// </summary>
    List<Expression<Func<T, object>>> Includes { get; }

    /// <summary>
    /// Gets a list of string-based include statements for eager loading related entities.
    /// </summary>
    List<string> IncludeStrings { get; }

    /// <summary>
    /// Gets the ordering expression for ascending order.
    /// </summary>
    Expression<Func<T, object>>? OrderBy { get; }

    /// <summary>
    /// Gets the ordering expression for descending order.
    /// </summary>
    Expression<Func<T, object>>? OrderByDescending { get; }

    /// <summary>
    /// Gets the number of entities to take in the result.
    /// </summary>
    int Take { get; }

    /// <summary>
    /// Gets the number of entities to skip before taking the result.
    /// </summary>
    int Skip { get; }

    /// <summary>
    /// Gets a value indicating whether paging is enabled for this specification.
    /// </summary>
    bool IsPagingEnabled { get; }
}
