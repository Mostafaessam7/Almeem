using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class SpecificationEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
    {
        var query = inputQuery;

        // Apply Criteria
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }

        // Apply Sorting
        if (spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }
        else if (spec.OrderByDescending != null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }

        // Apply Distinct
        if (spec.IsDistinct)
        {
            query = query.Distinct();
        }

        // Apply Paging
        if (spec.IsPagingEnabled)
        {
            query = query.Skip(spec.Skip).Take(spec.Take);
        }

        // Apply Includes
        query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

        return query;
    }

    public static IQueryable<TResult> GetQuery<TSpec, TResult>(IQueryable<T> inputQuery, ISpecification<T, TResult> spec)
    {
        var query = GetQuery(inputQuery, (ISpecification<T>)spec);

        if (spec.Selector != null)
        {
            return query.Select(spec.Selector);
        }

        return query.Cast<TResult>();
    }
}

