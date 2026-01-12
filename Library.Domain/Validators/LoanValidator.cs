using FluentValidation;
using Library.Domain;

namespace Library.Domain.Validators
{
    public class LoanValidator : AbstractValidator<Loan>
    {
        public LoanValidator()
        {
            RuleFor(l => l.Reader)
                .NotNull()
                .WithMessage("Loan must have a reader.");

            RuleFor(l => l.LoanDate)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Loan date cannot be in the future.");

            RuleFor(l => l.ReturnDueDate)
                .GreaterThan(l => l.LoanDate)
                .WithMessage("Return due date must be after loan date.");
        }
    }
}
