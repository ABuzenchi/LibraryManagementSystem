using FluentValidation.TestHelper;
using Library.Domain;
using Library.Domain.Validators;
using Xunit;

namespace Library.Tests.ValidatorTests
{
    public class ReaderValidatorTests
    {
        private readonly ReaderValidator _validator = new();

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var reader = new Reader
            {
                Name = "",
                Email = "test@test.com"
            };

            var result = _validator.TestValidate(reader);

            result.ShouldHaveValidationErrorFor(r => r.Name);
        }

        [Fact]
        public void Should_Have_Error_When_No_Contact_Info()
        {
            var reader = new Reader
            {
                Name = "John Doe",
                Email = null,
                Phone = null
            };

            var result = _validator.TestValidate(reader);

            result.ShouldHaveValidationErrorFor(r => r);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Email_Is_Present()
        {
            var reader = new Reader
            {
                Name = "John Doe",
                Email = "john@test.com"
            };

            var result = _validator.TestValidate(reader);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Should_Not_Have_Error_When_Phone_Is_Present()
        {
            var reader = new Reader
            {
                Name = "John Doe",
                Phone = "0712345678"
            };

            var result = _validator.TestValidate(reader);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
