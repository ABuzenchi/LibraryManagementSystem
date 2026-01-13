// Copyright (c) Buzenchi Andreea

using FluentValidation;
using Library.Domain;

namespace Library.Domain.Validators
{
    /// <summary>
    /// Validator responsible for validating <see cref="LoanItems"/> entities.
    /// </summary>
    /// <remarks>
    /// Ensures that a loan item is associated with a valid loan
    /// and references a valid book item.
    /// </remarks>
    public class LoanItemValidator : AbstractValidator<LoanItems>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoanItemValidator"/> class
        /// and defines validation rules for <see cref="LoanItems"/>.
        /// </summary>
        public LoanItemValidator()
        {
            this.RuleFor(li => li.Loan)
                .NotNull()
                .WithMessage("Loan item must belong to a loan.");

            this.RuleFor(li => li.BookItem)
                .NotNull()
                .WithMessage("Loan item must reference a book item.");
        }
    }
}
