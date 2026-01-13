// Copyright (c) Buzenchi Andreea

using FluentValidation;
using Library.Domain;

namespace Library.Domain.Validators
{
    /// <summary>
    /// Validator responsible for validating <see cref="Loan"/> entities.
    /// </summary>
    /// <remarks>
    /// Ensures that a loan is associated with a reader, has a valid loan date,
    /// and specifies a return due date after the loan date.
    /// </remarks>
    public class LoanValidator : AbstractValidator<Loan>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoanValidator"/> class
        /// and defines validation rules for <see cref="Loan"/>.
        /// </summary>
        public LoanValidator()
        {
            this.RuleFor(l => l.Reader)
                .NotNull()
                .WithMessage("Loan must have a reader.");

            this.RuleFor(l => l.LoanDate)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Loan date cannot be in the future.");

            this.RuleFor(l => l.ReturnDueDate)
                .GreaterThan(l => l.LoanDate)
                .WithMessage("Return due date must be after loan date.");
        }
    }
}
