using FluentValidation.TestHelper;
using Library.Domain;
using Library.Domain.Validators;

namespace Library.Tests.ValidatorTests
{
    public class EditionValidatorTests
    {
        private readonly EditionValidator _validator = new();

        private static Edition CreateValidEdition()
        {
            return new Edition
            {
                Id = 1,
                Book = new Book
                {
                    Id = 1,
                    Title = "Test Book",
                    Domains =
                    {
                        new BookDomain
                        {
                            Id = 1,
                            Name = "IT"
                        }
                    }
                },
                Publisher = "Test Publisher",
                Year = DateTime.Now.Year,
                EditionNumber = 1,
                Pages = 250,
                Type = "Paperback"
            };
        }

        [Fact]
        public void Should_Have_Error_When_Book_Is_Null()
        {
            var edition = CreateValidEdition();
            edition.Book = null!;

            var result = _validator.TestValidate(edition);

            result.ShouldHaveValidationErrorFor(e => e.Book);
        }

        [Fact]
        public void Should_Have_Error_When_Publisher_Is_Empty()
        {
            var edition = CreateValidEdition();
            edition.Publisher = "";

            var result = _validator.TestValidate(edition);

            result.ShouldHaveValidationErrorFor(e => e.Publisher);
        }

        [Fact]
        public void Should_Have_Error_When_Year_Is_In_Future()
        {
            var edition = CreateValidEdition();
            edition.Year = DateTime.Now.Year + 1;

            var result = _validator.TestValidate(edition);

            result.ShouldHaveValidationErrorFor(e => e.Year);
        }

        [Fact]
        public void Should_Have_Error_When_Pages_Is_Zero()
        {
            var edition = CreateValidEdition();
            edition.Pages = 0;

            var result = _validator.TestValidate(edition);

            result.ShouldHaveValidationErrorFor(e => e.Pages);
        }

        [Fact]
        public void Should_Have_Error_When_EditionNumber_Is_Zero()
        {
            var edition = CreateValidEdition();
            edition.EditionNumber = 0;

            var result = _validator.TestValidate(edition);

            result.ShouldHaveValidationErrorFor(e => e.EditionNumber);
        }

        [Fact]
        public void Should_Not_Have_Errors_When_Edition_Is_Valid()
        {
            var edition = CreateValidEdition();

            var result = _validator.TestValidate(edition);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
