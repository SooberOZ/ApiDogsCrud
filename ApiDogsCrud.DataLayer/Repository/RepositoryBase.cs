using ApiDogsCrud.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApiDogsCrud.DataLayer.Repository
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        private readonly DogDBContext _dbContext;

        public RepositoryBase(DogDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> GetDogsAsync(ISpecification<T> spec)
        {
            var query = GetQuery(_dbContext.Set<T>(), spec);
            var entities = await query.ToListAsync();
            return entities;
        }

        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<T> FindOneAsync(ISpecification<T> spec)
        {
            var specificationResult = GetQuery(_dbContext.Set<T>(), spec);

            return await specificationResult.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistAsync(ISpecification<T> spec)
        {
            return (await FindOneAsync(spec)) is not null;
        }

        private IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
        {
            var query = inputQuery;

            if (specification.Criterias != null && specification.Criterias.Any())
            {
                var combinedCriteria = specification.Criterias.Aggregate(
                    (c1, c2) => Expression.Lambda<Func<T, bool>>(
                        Expression.AndAlso(c1.Body, c2.Body), c1.Parameters));
                query = query.Where(combinedCriteria);
            }

            query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            if (specification.GroupBy != null)
            {
                query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
            }

            if (specification.IsPagingEnabled)
            {
                query = query
                    .Skip(specification.Skip - 1)
                    .Take(specification.Take);
            }

            return query;
        }
    }
}