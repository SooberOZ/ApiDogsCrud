﻿using ApiDogsCrud.Contracts;
using System.Linq.Expressions;

namespace ApiDogsCrud.DataLayer.Specification
{
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        public List<Expression<Func<T, bool>>> Criterias { get; private set; } = new List<Expression<Func<T, bool>>>();
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }
        public Expression<Func<T, object>> GroupBy { get; private set; }

        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; }

        protected void ApplyIncludeList(IEnumerable<Expression<Func<T, object>>> includes) =>
         Includes.AddRange(includes);

        protected void AddInclude(Expression<Func<T, object>> includeExpression) =>
         Includes.Add(includeExpression);

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression) =>
         OrderBy = orderByExpression;

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression) =>
         OrderByDescending = orderByDescendingExpression;

        protected void AddCriteria(Expression<Func<T, bool>> criteria) =>
         Criterias.Add(criteria);

        protected void AddCriteria(IEnumerable<Expression<Func<T, bool>>> criteria) =>
         Criterias.AddRange(criteria);

        protected void AddGroupBy(Expression<Func<T, object>> groupByExpression) =>
         GroupBy = groupByExpression;

        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }
}