// Copyright (c) Buzenchi Andreea

using FluentValidation;
using Library.Domain;

namespace Library.Domain.Validators
{
    /// <summary>
    /// Validator responsible for validating <see cref="LoanExtension"/> entities.
    /// </summary>
    /// <remarks>
    /// Ensures that a loan extension is associated with a valid loan,
    /// has a positive extension period, and does not specify a future date.
    /// </remarks>
    public class LoanExtensionValidator : AbstractValidator<LoanExtension>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoanExtensionValidator"/> class
        /// and defines validation rules for <see cref="LoanExtension"/>.
        /// </summary>
        public LoanExtensionValidator()
        {
            this.RuleFor(le => le.Loan)
                .NotNull()
                .WithMessage("Loan extension must belong to a loan.");

            this.RuleFor(le => le.DaysExtended)
                .GreaterThan(0)
                .WithMessage("Extension days must be greater than zero.");

            this.RuleFor(le => le.ExtensionDate)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Extension date cannot be in the future.");
        }
    }
}
