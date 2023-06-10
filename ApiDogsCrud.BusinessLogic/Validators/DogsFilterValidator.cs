using ApiDogsCrud.Models;
using FluentValidation;

namespace ApiDogsCrud.BusinessLogic.Validators
{
    public class DogsFilterValidator : AbstractValidator<DogsFilter>
    {
        public DogsFilterValidator()
        {
            RuleFor(filter => filter.PageNumber)
                .GreaterThan(0)
                .WithMessage("Page number must be greater than 0.");

            RuleFor(filter => filter.PageSize)
                .GreaterThan(0)
                .WithMessage("Page size must be greater than 0.");

            RuleFor(filter => filter.Direction)
                .IsInEnum()
                .WithMessage("Invalid sort direction.");

            RuleFor(filter => filter.OrderBy)
                .IsInEnum()
                .WithMessage("Invalid sort field.");
        }
    }
}