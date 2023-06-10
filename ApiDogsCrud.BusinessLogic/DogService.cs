using ApiDogsCrud.Contracts;
using ApiDogsCrud.DataLayer.Entity;
using ApiDogsCrud.DataLayer.Specification;
using ApiDogsCrud.Models;
using AutoMapper;
using FluentValidation;

namespace ApiDogsCrud.BusinessLogic
{
    public class DogService : IDogService
    {
        private readonly IRepository<Dog> _dogRepository;
        private readonly IValidator<Dog> _validator;
        private readonly IValidator<DogsFilter> _filterValidator;
        private readonly IMapper _mapper;

        public DogService(IRepository<Dog> dogRepository, IValidator<Dog> validator, IValidator<DogsFilter> filterValidator, IMapper mapper)
        {
            _dogRepository = dogRepository;
            _validator = validator;
            _filterValidator = filterValidator;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DogDto>> GetDogsAsync(DogsFilter filter)
        {
            await _filterValidator.ValidateAndThrowAsync(filter);

            var specification = new GetDogByFilterSpecification(filter);
            var dogs = await _dogRepository.GetAsync(specification);

            var dogDtos = _mapper.Map<IEnumerable<DogDto>>(dogs);

            return dogDtos;
        }

        public async Task CreateDogAsync(DogDto dogDto)
        {
            var dog = _mapper.Map<Dog>(dogDto);

            await _validator.ValidateAndThrowAsync(dog);

            await _dogRepository.AddAsync(dog);
        }
    }
}