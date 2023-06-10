namespace ApiDogsCrud.Contracts
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAsync(ISpecification<T> spec);
        Task AddAsync(T item);
        Task<T> FindOneAsync(ISpecification<T> spec);
        Task<bool> ExistAsync(ISpecification<T> spec);
    }
}