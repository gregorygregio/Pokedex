using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Application.Entities
{
    public abstract class BaseEntity<T> : AbstractValidator<T>
    {
        public Guid id { get; set; }

        public ValidationResult ValidationResult { get; protected set; }

        public BaseEntity()
        {
            ValidationResult = new ValidationResult();
        }

        public abstract bool IsValid();
    }
}
