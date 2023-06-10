using ApiDogsCrud.Contracts;
using ApiDogsCrud.DataLayer.Entity;
using ApiDogsCrud.DataLayer.Specification;
using FluentValidation;

namespace ApiDogsCrud.BusinessLogic.Validators
{
    public class DogValidator : AbstractValidator<Dog>
    {
        private readonly IRepository<Dog> _repository;
        public DogValidator(IRepository<Dog> repository)
        {
            _repository = repository;
            RuleFor(dog => dog.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Dog name must be provided.")
                .MustAsync(async (x, _) => !await _repository.ExistAsync(new GetDogByNameSpecification(x)))
                .WithMessage("Dog with the provided name already exists.");

            RuleFor(dog => dog.Color)
                .NotEmpty()
                .WithMessage("Dog color must be provided.");

            RuleFor(dog => dog.TailLength)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Dog tail length must be a non-negative value.");

            RuleFor(dog => dog.Weight)
               .GreaterThanOrEqualTo(1)
               .WithMessage("Dog weight must be a non-negative value or null.");
        }
    }
}