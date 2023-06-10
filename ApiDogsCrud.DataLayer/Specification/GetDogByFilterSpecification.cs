using ApiDogsCrud.DataLayer.Entity;
using ApiDogsCrud.Models;
using ApiDogsCrud.Models.Enums;
using System.Linq.Expressions;

namespace ApiDogsCrud.DataLayer.Specification
{
    public class GetDogByFilterSpecification: BaseSpecification<Dog>
    {
        public GetDogByFilterSpecification(DogsFilter filter)
        {
            ApplySorting(filter.Direction, filter.OrderBy);
            ApplyPaging(filter.PageSize * (filter.PageNumber - 1), filter.PageSize);
        }
        private void ApplySorting(SortDirection sortDirection, FulfillmentDogSortFields orderBy)
        {
            Expression<Func<Dog, object>> orderByExpression = GetOrderByExpression(orderBy);

            if (sortDirection == SortDirection.Ascending)
                AddOrderBy(orderByExpression);
            else
                AddOrderByDescending(orderByExpression);
        }
        private Expression<Func<Dog, object>> GetOrderByExpression(FulfillmentDogSortFields orderBy)
        {
            switch (orderBy)
            {
                case FulfillmentDogSortFields.Name:
                    return x => x.Name;
                case FulfillmentDogSortFields.Color:
                    return x => x.Color;
                case FulfillmentDogSortFields.TailLength:
                    return x => x.TailLength;
                case FulfillmentDogSortFields.Weight:
                    return x => x.Weight;
                default:
                    return x => x.Name;
            }
        }
    }
}