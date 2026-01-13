// Copyright (c) Buzenchi Andreea

using FluentValidation;
using Library.Domain;

namespace Library.Domain.Validators
{
    /// <summary>
    /// Validator responsible for validating <see cref="Book"/> entities.
    /// </summary>
    /// <remarks>
    /// Ensures that a book has a valid title and is assigned
    /// to at least one domain.
    /// </remarks>
    public class BookValidator : AbstractValidator<Book>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookValidator"/> class
        /// and defines validation rules for <see cref="Book"/>.
        /// </summary>
        public BookValidator()
        {
            this.RuleFor(b => b.Title)
                    .NotEmpty()
                    .WithMessage("Book title is required.");

            this.RuleFor(b => b.Domains)
                 .NotNull()
                 .NotEmpty()
                 .WithMessage("Book must belong to at least one domain.");
        }
    }
}
