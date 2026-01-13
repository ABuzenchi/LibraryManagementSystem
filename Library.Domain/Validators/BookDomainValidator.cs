// Copyright (c) Buzenchi Andreea

using FluentValidation;
using Library.Domain;

namespace Library.Domain.Validators
{
    /// <summary>
    /// Validator responsible for validating <see cref="BookDomain"/> entities.
    /// </summary>
    /// <remarks>
    /// Ensures that a book domain has a valid name and does not reference itself
    /// as a parent domain.
    /// </remarks>
    public class BookDomainValidator : AbstractValidator<BookDomain>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookDomainValidator"/> class
        /// and defines validation rules for <see cref="BookDomain"/>.
        /// </summary>
        public BookDomainValidator()
        {
            this.RuleFor(d => d.Name)
                .NotEmpty()
                .WithMessage("Domain name is required.");

            this.RuleFor(d => d)
                .Must(d => d.Parent == null || d.Parent.Id != d.Id)
                .WithMessage("A domain cannot be its own parent.");
        }
    }
}
