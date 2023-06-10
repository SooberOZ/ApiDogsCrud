using ApiDogsCrud.Models;

namespace ApiDogsCrud.Contracts
{
    public interface IDogService<DogDto>
    {
        public Task<IEnumerable<DogDto>> GetDogsAsync(DogsFilter filter);
        public Task CreateDogAsync(DogDto dog);
    }
}
