using FluentValidation;
using Library.Domain;

namespace Library.Domain.Validators
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(b => b.Title)
                .NotEmpty()
                .WithMessage("Book title is required.");

            RuleFor(b => b.Domains)
                .NotNull()
                .NotEmpty()
                .WithMessage("Book must belong to at least one domain.");
        }
    }
}
