using FluentValidation.TestHelper;
using Library.Domain;
using Library.Domain.Validators;
using Xunit;

namespace Library.Tests.ValidatorTests
{
    public class BookDomainValidatorTests
    {
        private readonly BookDomainValidator validator = new();

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var domain = new BookDomain { Name = string.Empty };

            var result = validator.TestValidate(domain);

            result.ShouldHaveValidationErrorFor(d => d.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Domain_Is_Own_Parent()
        {
            var domain = new BookDomain { Id = 1, Name = "IT" };
            domain.Parent = domain;

            var result = validator.TestValidate(domain);

            result.ShouldHaveValidationErrorFor(d => d);
        }

        [Fact]
        public void Should_Not_Have_Error_For_Valid_Domain()
        {
            var parent = new BookDomain { Id = 1, Name = "Science" };
            var child = new BookDomain { Id = 2, Name = "IT", Parent = parent };

            var result = validator.TestValidate(child);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
