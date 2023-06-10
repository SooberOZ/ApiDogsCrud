using ApiDogsCrud.Models;

namespace ApiDogsCrud.Contracts
{
    public interface IDogService
    {
        Task<IEnumerable<DogDto>> GetDogsAsync(DogsFilter filter);
        Task CreateDogAsync(DogDto dog);
    }
}
