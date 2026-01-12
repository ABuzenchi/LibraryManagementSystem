using FluentValidation;
using Library.Domain;

namespace Library.Domain.Validators
{
    public class AuthorValidator : AbstractValidator<Author>
    {
        public AuthorValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("Author name is required.");
        }
    }
}
