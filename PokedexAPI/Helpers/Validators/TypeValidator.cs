using FluentValidation;
using PokedexAPI.Models;

namespace PokedexAPI.Helpers.Validators
{
    public class TypeValidator : AbstractValidator<Type>
    {
        public TypeValidator()
        {
            RuleFor(x => x.type)
                .MaximumLength(36).NotNull();
        }
    }
}