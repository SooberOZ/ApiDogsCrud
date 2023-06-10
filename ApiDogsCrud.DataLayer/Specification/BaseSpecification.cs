using ApiDogsCrud.Contracts;
using System.Linq.Expressions;

namespace ApiDogsCrud.DataLayer.Specification
{
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        public List<Expression<Func<T, bool>>> Criterias { get; private set; } = new List<Expression<Func<T, bool>>>();
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; }


        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression) =>
         OrderBy = orderByExpression;

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression) =>
         OrderByDescending = orderByDescendingExpression;

        protected void AddCriteria(Expression<Func<T, bool>> criteria) =>
         Criterias.Add(criteria);

        protected void AddCriteria(IEnumerable<Expression<Func<T, bool>>> criteria) =>
         Criterias.AddRange(criteria);

        protected void AddPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }
}