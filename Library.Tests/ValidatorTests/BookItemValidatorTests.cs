using FluentValidation.TestHelper;
using Library.Domain;
using Library.Domain.Validators;

namespace Library.Tests.ValidatorTests
{
    public class BookItemValidatorTests
    {
        private readonly BookItemValidator validator = new();

        [Fact]
        public void Should_Have_Error_When_Edition_Is_Null()
        {
            var item = new BookItem { Edition = null! };

            var result = validator.TestValidate(item);

            result.ShouldHaveValidationErrorFor(i => i.Edition);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Edition_Is_Set()
        {
            var item = new BookItem
            {
                Edition = new Edition
                {
                    Book = new Book { Id = 1, Title = "Test" },
                    Publisher = "Pub",
                    Year = 2024,
                    EditionNumber = 1,
                    Pages = 100
                }
            };

            var result = validator.TestValidate(item);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
