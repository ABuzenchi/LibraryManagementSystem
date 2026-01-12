using FluentValidation.TestHelper;
using Library.Domain;
using Library.Domain.Validators;
using Xunit;

namespace Library.Tests.ValidatorTests
{
    public class LoanValidatorTests
    {
        private readonly LoanValidator _validator = new();

        private static Loan CreateValidLoan()
        {
            return new Loan
            {
                Id = 1,
                Reader = new Reader
                {
                    Id = 1,
                    Name = "John Doe",
                    Email = "john@test.com"
                },
                LoanDate = DateTime.Today,
                ReturnDueDate = DateTime.Today.AddDays(7)
            };
        }

        [Fact]
        public void Should_Have_Error_When_Reader_Is_Null()
        {
            var loan = CreateValidLoan();
            loan.Reader = null!;

            var result = _validator.TestValidate(loan);

            result.ShouldHaveValidationErrorFor(l => l.Reader);
        }

        [Fact]
        public void Should_Have_Error_When_LoanDate_Is_In_Future()
        {
            var loan = CreateValidLoan();
            loan.LoanDate = DateTime.Today.AddDays(1);

            var result = _validator.TestValidate(loan);

            result.ShouldHaveValidationErrorFor(l => l.LoanDate);
        }

        [Fact]
        public void Should_Have_Error_When_ReturnDueDate_Is_Before_LoanDate()
        {
            var loan = CreateValidLoan();
            loan.ReturnDueDate = loan.LoanDate.AddDays(-1);

            var result = _validator.TestValidate(loan);

            result.ShouldHaveValidationErrorFor(l => l.ReturnDueDate);
        }

        [Fact]
        public void Should_Not_Have_Errors_When_Loan_Is_Valid()
        {
            var loan = CreateValidLoan();

            var result = _validator.TestValidate(loan);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
