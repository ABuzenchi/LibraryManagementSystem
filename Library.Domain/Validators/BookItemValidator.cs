// Copyright (c) Buzenchi Andreea

using FluentValidation;
using Library.Domain;

namespace Library.Domain.Validators
{
    /// <summary>
    /// Validator responsible for validating <see cref="BookItem"/> entities.
    /// </summary>
    /// <remarks>
    /// Ensures that a book item is properly associated with a valid
    /// <see cref="Edition"/> instance.
    /// </remarks>
    public class BookItemValidator : AbstractValidator<BookItem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookItemValidator"/> class
        /// and defines validation rules for <see cref="BookItem"/>.
        /// </summary>
        public BookItemValidator()
        {
            this.RuleFor(bi => bi.Edition)
                .NotNull()
                .WithMessage("Book item must reference an edition.");
        }
    }
}
