using FluentValidation.TestHelper;
using Library.Domain;
using Library.Domain.Validators;
using Xunit;

namespace Library.Tests.ValidatorTests
{
    public class LoanExtensionValidatorTests
    {
        private readonly LoanExtensionValidator _validator;

        public LoanExtensionValidatorTests()
        {
            _validator = new LoanExtensionValidator();
        }

        private static Loan CreateValidLoan()
        {
            return new Loan
            {
                Id = 1,
                Reader = new Reader
                {
                    Id = 1,
                    Name = "Test Reader",
                    Email = "test@test.com"
                },
                LoanDate = DateTime.Today,
                ReturnDueDate = DateTime.Today.AddDays(7)
            };
        }

        [Fact]
        public void Should_Have_Error_When_Loan_Is_Null()
        {
            var extension = new LoanExtension
            {
                Loan = null!,
                DaysExtended = 5,
                ExtensionDate = DateTime.Today
            };

            var result = _validator.TestValidate(extension);

            result.ShouldHaveValidationErrorFor(le => le.Loan);
        }

        [Fact]
        public void Should_Have_Error_When_DaysExtended_Is_Zero()
        {
            var extension = new LoanExtension
            {
                Loan = CreateValidLoan(),
                DaysExtended = 0,
                ExtensionDate = DateTime.Today
            };

            var result = _validator.TestValidate(extension);

            result.ShouldHaveValidationErrorFor(le => le.DaysExtended);
        }

        [Fact]
        public void Should_Have_Error_When_ExtensionDate_Is_In_Future()
        {
            var extension = new LoanExtension
            {
                Loan = CreateValidLoan(),
                DaysExtended = 3,
                ExtensionDate = DateTime.Today.AddDays(1)
            };

            var result = _validator.TestValidate(extension);

            result.ShouldHaveValidationErrorFor(le => le.ExtensionDate);
        }

        [Fact]
        public void Should_Not_Have_Errors_When_LoanExtension_Is_Valid()
        {
            var extension = new LoanExtension
            {
                Loan = CreateValidLoan(),
                DaysExtended = 3,
                ExtensionDate = DateTime.Today
            };

            var result = _validator.TestValidate(extension);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
