using FluentValidation;
using Library.Domain;

namespace Library.Domain.Validators
{
    public class EditionValidator : AbstractValidator<Edition>
    {
        public EditionValidator()
        {
            RuleFor(e => e.Book)
                .NotNull()
                .WithMessage("Edition must belong to a book.");

            RuleFor(e => e.Publisher)
                .NotEmpty()
                .WithMessage("Publisher is required.");

            RuleFor(e => e.Year)
                .GreaterThan(0)
                .LessThanOrEqualTo(DateTime.Now.Year)
                .WithMessage("Edition year must be valid.");

            RuleFor(e => e.Pages)
                .GreaterThan(0)
                .WithMessage("Pages must be greater than zero.");

            RuleFor(e => e.EditionNumber)
                .GreaterThan(0)
                .WithMessage("Edition number must be greater than zero.");
        }
    }
}
