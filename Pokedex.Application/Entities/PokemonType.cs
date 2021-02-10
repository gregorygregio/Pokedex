using FluentValidation;
using System;
using System.Text;
using System.Collections.Generic;

namespace Pokedex.Application.Entities
{
    public class PokemonType : BaseEntity<PokemonType>
    {
        public string Type { get; set; }

        public override bool IsValid()
        {
            ValidateType();

            ValidationResult = Validate(this);

            return ValidationResult.IsValid; ;
        }

        public void ValidateType()
        {
            RuleFor(t => t.Type)
                .NotNull().WithMessage("The Type must be informed")
                .MaximumLength(36).WithMessage("The Type shouldn't have more than 36 characters");
        }
    }
}
