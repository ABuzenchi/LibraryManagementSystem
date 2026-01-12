using FluentValidation;
using Library.Domain;

namespace Library.Domain.Validators
{
    public class BookDomainValidator : AbstractValidator<BookDomain>
    {
        public BookDomainValidator()
        {
            RuleFor(d => d.Name)
                .NotEmpty()
                .WithMessage("Domain name is required.");

            RuleFor(d => d)
                .Must(d => d.Parent == null || d.Parent.Id != d.Id)
                .WithMessage("A domain cannot be its own parent.");
        }
    }
}
