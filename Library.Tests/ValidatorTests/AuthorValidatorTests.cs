using FluentValidation.TestHelper;
using Library.Domain;
using Library.Domain.Validators;

namespace Library.Tests.ValidatorTests
{
    public class AuthorValidatorTests
    {
        private readonly AuthorValidator validator = new();

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var author = new Author { Name = string.Empty };

            var result = validator.TestValidate(author);

            result.ShouldHaveValidationErrorFor(a => a.Name);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Name_Is_Valid()
        {
            var author = new Author { Name = "Test Author" };

            var result = validator.TestValidate(author);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
