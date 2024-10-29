using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specifications;

public class BaseSpecification<T> : ISpecification<T>
{
    public BaseSpecification() { }
    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    public Expression<Func<T, bool>>? Criteria { get; }
     //public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }
    public bool IsDistinct { get; private set; }
    public int Take { get; private set; }
    public int Skip { get; private set; }
    public bool IsPagingEnabled { get; private set; }

    public List<Expression<Func<T, object>>> Includes { get; } = [];

    public List<string> IncludeStrings { get; } = [];


    protected void AddInclude(Expression<Func<T, object>> includeExpressions)
    {
        Includes.Add(includeExpressions);
    }

    protected void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString); // For ThenInclude
    }

    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
    {
        OrderByDescending = orderByDescExpression;
    }

    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }

    protected void ApplyDistinct()
    {
        IsDistinct = true;
    }

}

public class BaseSpecification<T, TResult> : BaseSpecification<T>, ISpecification<T, TResult>
{
    public BaseSpecification() { }
    public BaseSpecification(Expression<Func<T, bool>> criteria) : base(criteria) { }

    public Expression<Func<T, TResult>>? Selector { get; private set; }

    protected void AddSelector(Expression<Func<T, TResult>> selector)
    {
        Selector = selector;
    }
}