using System.Linq.Expressions;

namespace ApiDogsCrud.Contracts
{
    public interface ISpecification<T>
    {
        List<Expression<Func<T, bool>>> Criterias { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
    }
}