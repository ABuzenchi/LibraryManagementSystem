// Copyright (c) Buzenchi Andreea

using FluentValidation;
using Library.Domain;

namespace Library.Domain.Validators
{
    /// <summary>
    /// Validator responsible for validating <see cref="Author"/> entities.
    /// </summary>
    /// <remarks>
    /// Ensures that all required author fields respect the defined domain rules.
    /// </remarks>
    public class AuthorValidator : AbstractValidator<Author>
    {
         /// <summary>
        /// Initializes a new instance of the <see cref="AuthorValidator"/> class
        /// and defines validation rules for an <see cref="Author"/>.
        /// </summary>
        public AuthorValidator()
        {
            this.RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("Author name is required.");
        }
    }
}
