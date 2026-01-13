using FluentValidation.TestHelper;
using Library.Domain;
using Library.Domain.Validators;
using Xunit;

namespace Library.Tests.ValidatorTests
{
    public class LoanItemValidatorTests
    {
        private readonly LoanItemValidator validator = new();

        private static Loan CreateValidLoan()
        {
            return new Loan
            {
                Reader = new Reader { Id = 1, Name = "Ana" },
                LoanDate = DateTime.Today,
                ReturnDueDate = DateTime.Today.AddDays(7)
            };
        }

        private static BookItem CreateValidBookItem()
        {
            return new BookItem
            {
                Edition = new Edition
                {
                    Book = new Book { Id = 1, Title = "Test Book" },
                    Publisher = "Test Publisher",
                    Year = DateTime.Now.Year,
                    EditionNumber = 1,
                    Pages = 100
                }
            };
        }

        [Fact]
        public void Should_Have_Error_When_Loan_Is_Null()
        {
            var item = new LoanItems
            {
                Loan = null!,
                BookItem = CreateValidBookItem()
            };

            var result = validator.TestValidate(item);

            result.ShouldHaveValidationErrorFor(li => li.Loan);
        }

        [Fact]
        public void Should_Have_Error_When_BookItem_Is_Null()
        {
            var item = new LoanItems
            {
                Loan = CreateValidLoan(),
                BookItem = null!
            };

            var result = validator.TestValidate(item);

            result.ShouldHaveValidationErrorFor(li => li.BookItem);
        }

        [Fact]
        public void Should_Not_Have_Error_For_Valid_LoanItem()
        {
            var item = new LoanItems
            {
                Loan = CreateValidLoan(),
                BookItem = CreateValidBookItem()
            };

            var result = validator.TestValidate(item);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
