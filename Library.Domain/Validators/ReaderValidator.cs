using FluentValidation;
using Library.Domain;

namespace Library.Domain.Validators
{
    public class ReaderValidator : AbstractValidator<Reader>
    {
        public ReaderValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .WithMessage("Reader name is required.");

            RuleFor(r => r)
                .Must(r =>
                    !string.IsNullOrWhiteSpace(r.Email) ||
                    !string.IsNullOrWhiteSpace(r.Phone))
                .WithMessage("Reader must have at least an email or a phone number.");
        }
    }
}
