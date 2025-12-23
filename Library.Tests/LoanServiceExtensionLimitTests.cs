using System;
using System.Collections.Generic;
using Library.Domain;
using Library.Service;
using Xunit;

namespace Library.Tests
{
    public class LoanServiceExtensionLimitTests
    {
        private static Loan CreateLoan(int id)
        {
            return new Loan
            {
                Id = id,
                Reader = new Reader { Id = 1, Name = "Ana" },
                LoanDate = DateTime.Today,
                ReturnDueDate = DateTime.Today.AddDays(14)
            };
        }

        private static LoanExtension CreateExtension(Loan loan)
        {
            return new LoanExtension
            {
                Loan = loan,
                DaysExtended = 7,
                ExtensionDate = DateTime.Today
            };
        }

        [Fact]
        public void Throws_WhenExtensionLimitIsExceeded()
        {
            var loan = CreateLoan(1);

            var extensions = new List<LoanExtension>
            {
                CreateExtension(loan),
                CreateExtension(loan)
            };

            var service = new LoanService();

            Assert.Throws<InvalidOperationException>(() =>
                service.ValidateLoanExtensionLimit(
                    loan,
                    extensions,
                    maxExtensions: 2));
        }

        [Fact]
        public void DoesNotThrow_WhenBelowExtensionLimit()
        {
            var loan = CreateLoan(1);

            var extensions = new List<LoanExtension>
            {
                CreateExtension(loan)
            };

            var service = new LoanService();

            var exception = Record.Exception(() =>
                service.ValidateLoanExtensionLimit(
                    loan,
                    extensions,
                    maxExtensions: 2));

            Assert.Null(exception);
        }

        [Fact]
        public void DoesNotThrow_WhenNoExtensionsExist()
        {
            var loan = CreateLoan(1);

            var service = new LoanService();

            var exception = Record.Exception(() =>
                service.ValidateLoanExtensionLimit(
                    loan,
                    new List<LoanExtension>(),
                    maxExtensions: 2));

            Assert.Null(exception);
        }

        [Fact]
        public void IgnoresExtensionsForOtherLoans()
        {
            var loan1 = CreateLoan(1);
            var loan2 = CreateLoan(2);

            var extensions = new List<LoanExtension>
            {
                CreateExtension(loan2),
                CreateExtension(loan2)
            };

            var service = new LoanService();

            var exception = Record.Exception(() =>
                service.ValidateLoanExtensionLimit(
                    loan1,
                    extensions,
                    maxExtensions: 2));

            Assert.Null(exception);
        }

        [Fact]
        public void Throws_WhenMaxExtensionsIsZero()
        {
            var loan = CreateLoan(1);
            var service = new LoanService();

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                service.ValidateLoanExtensionLimit(
                    loan,
                    new List<LoanExtension>(),
                    maxExtensions: 0));
        }
    }
}
