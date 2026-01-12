using FluentValidation;
using Library.Domain;

namespace Library.Domain.Validators
{
    public class BookItemValidator : AbstractValidator<BookItem>
    {
        public BookItemValidator()
        {
            RuleFor(bi => bi.Edition)
                .NotNull()
                .WithMessage("Book item must reference an edition.");
        }
    }
}
