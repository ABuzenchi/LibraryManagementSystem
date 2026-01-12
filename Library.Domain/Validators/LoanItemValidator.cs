using FluentValidation;
using Library.Domain;

namespace Library.Domain.Validators
{
    public class LoanItemValidator : AbstractValidator<LoanItems>
    {
        public LoanItemValidator()
        {
            RuleFor(li => li.Loan)
                .NotNull()
                .WithMessage("Loan item must belong to a loan.");

            RuleFor(li => li.BookItem)
                .NotNull()
                .WithMessage("Loan item must reference a book item.");
        }
    }
}
