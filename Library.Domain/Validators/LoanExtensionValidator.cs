using FluentValidation;
using Library.Domain;

namespace Library.Domain.Validators
{
    public class LoanExtensionValidator : AbstractValidator<LoanExtension>
    {
        public LoanExtensionValidator()
        {
            RuleFor(le => le.Loan)
                .NotNull()
                .WithMessage("Loan extension must belong to a loan.");

            RuleFor(le => le.DaysExtended)
                .GreaterThan(0)
                .WithMessage("Extension days must be greater than zero.");

            RuleFor(le => le.ExtensionDate)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Extension date cannot be in the future.");
        }
    }
}
