using FluentValidation.TestHelper;
using Library.Domain;
using Library.Domain.Validators;

namespace Library.Tests.ValidatorTests
{
    public class BookValidatorTests
    {
        private readonly BookValidator _validator = new();

        private static Book CreateValidBook()
        {
            var book = new Book
            {
                Id = 1,
                Title = "Test Book"
            };

            book.SetDomains(
                new[]
                {
            new BookDomain
            {
                Id = 1,
                Name = "IT"
            }
                },
                maxDomains: 5
            );

            return book;
        }


        [Fact]
        public void Should_Have_Error_When_Title_Is_Empty()
        {
            var book = CreateValidBook();
            book.Title = "";

            var result = _validator.TestValidate(book);

            result.ShouldHaveValidationErrorFor(b => b.Title);
        }

        [Fact]
        public void Should_Have_Error_When_Domains_Is_Empty()
        {
            var book = new Book
            {
                Id = 1,
                Title = "Test Book"
            };

            var result = _validator.TestValidate(book);

            result.ShouldHaveValidationErrorFor(b => b.Domains);
        }

        [Fact]
        public void Should_Not_Have_Errors_When_Book_Is_Valid()
        {
            var book = CreateValidBook();

            var result = _validator.TestValidate(book);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
