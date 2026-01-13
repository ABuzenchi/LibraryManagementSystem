// Copyright (c) Buzenchi Andreea

using FluentValidation;
using Library.Domain;

namespace Library.Domain.Validators
{
    /// <summary>
    /// Validator responsible for validating <see cref="Edition"/> entities.
    /// </summary>
    /// <remarks>
    /// Ensures that an edition is properly associated with a book and
    /// contains valid publishing and structural information.
    /// </remarks>
    public class EditionValidator : AbstractValidator<Edition>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditionValidator"/> class
        /// and defines validation rules for <see cref="Edition"/>.
        /// </summary>
        public EditionValidator()
        {
            this.RuleFor(e => e.Book)
                .NotNull()
                .WithMessage("Edition must belong to a book.");

            this.RuleFor(e => e.Publisher)
                .NotEmpty()
                .WithMessage("Publisher is required.");

            this.RuleFor(e => e.Year)
                .GreaterThan(0)
                .LessThanOrEqualTo(DateTime.Now.Year)
                .WithMessage("Edition year must be valid.");

            this.RuleFor(e => e.Pages)
                .GreaterThan(0)
                .WithMessage("Pages must be greater than zero.");

            this.RuleFor(e => e.EditionNumber)
                .GreaterThan(0)
                .WithMessage("Edition number must be greater than zero.");
        }
    }
}
