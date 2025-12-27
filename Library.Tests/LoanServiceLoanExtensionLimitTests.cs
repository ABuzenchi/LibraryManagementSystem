using System;
using System.Collections.Generic;
using Library.Domain;
using Library.Service;
using Xunit;

namespace Library.Tests
{
    public class LoanServiceLoanExtensionLimitTests
    {
        private static Loan CreateLoan(Reader reader)
        {
            return new Loan
            {
                Id = 1,
                Reader = reader,
                LoanDate = DateTime.Today,
                ReturnDueDate = DateTime.Today.AddDays(14)
            };
        }

        [Fact]
        public void Throws_When_Loan_Is_Null()
        {
            var service = new LoanService();

            Assert.Throws<ArgumentNullException>(() =>
                service.ValidateLoanExtensionLimit(
                    null!,
                    new List<LoanExtension>(),
                    2));
        }

        [Fact]
        public void Throws_When_Extensions_Is_Null()
        {
            var service = new LoanService();
            var loan = CreateLoan(new Reader { Id = 1, Name = "Ana" });

            Assert.Throws<ArgumentNullException>(() =>
                service.ValidateLoanExtensionLimit(
                    loan,
                    null!,
                    2));
        }

        [Fact]
        public void Throws_When_Limit_Is_Zero()
        {
            var service = new LoanService();
            var loan = CreateLoan(new Reader { Id = 1, Name = "Ana" });

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                service.ValidateLoanExtensionLimit(
                    loan,
                    new List<LoanExtension>(),
                    0));
        }

        [Fact]
        public void DoesNotThrow_When_No_Extensions()
        {
            var service = new LoanService();
            var loan = CreateLoan(new Reader { Id = 1, Name = "Ana" });

            var ex = Record.Exception(() =>
                service.ValidateLoanExtensionLimit(
                    loan,
                    new List<LoanExtension>(),
                    2));

            Assert.Null(ex);
        }

        [Fact]
        public void DoesNotThrow_When_Below_Limit()
        {
            var service = new LoanService();
            var loan = CreateLoan(new Reader { Id = 1, Name = "Ana" });

            var extensions = new List<LoanExtension>
            {
                new LoanExtension
                {
                    Loan = loan,
                    DaysExtended = 7,
                    ExtensionDate = DateTime.Today
                }
            };

            var ex = Record.Exception(() =>
                service.ValidateLoanExtensionLimit(
                    loan,
                    extensions,
                    2));

            Assert.Null(ex);
        }

        [Fact]
        public void Throws_When_At_Limit()
        {
            var service = new LoanService();
            var loan = CreateLoan(new Reader { Id = 1, Name = "Ana" });

            var extensions = new List<LoanExtension>
            {
                new LoanExtension { Loan = loan, DaysExtended = 7, ExtensionDate = DateTime.Today },
                new LoanExtension { Loan = loan, DaysExtended = 7, ExtensionDate = DateTime.Today }
            };

            Assert.Throws<InvalidOperationException>(() =>
                service.ValidateLoanExtensionLimit(
                    loan,
                    extensions,
                    2));
        }

        [Fact]
        public void Counts_Only_Extensions_For_Same_Loan()
        {
            var service = new LoanService();

            var loan1 = new Loan
            {
                Id = 1,
                Reader = new Reader { Id = 1, Name = "Ana" },
                LoanDate = DateTime.Today,
                ReturnDueDate = DateTime.Today.AddDays(14)
            };

            var loan2 = new Loan
            {
                Id = 2,
                Reader = new Reader { Id = 2, Name = "Ion" },
                LoanDate = DateTime.Today,
                ReturnDueDate = DateTime.Today.AddDays(14)
            };

            var extensions = new List<LoanExtension>
    {
        new LoanExtension { Loan = loan2, DaysExtended = 7, ExtensionDate = DateTime.Today },
        new LoanExtension { Loan = loan2, DaysExtended = 7, ExtensionDate = DateTime.Today }
    };

            var ex = Record.Exception(() =>
                service.ValidateLoanExtensionLimit(
                    loan1,
                    extensions,
                    1));

            Assert.Null(ex);
        }


        [Fact]
        public void Throws_When_Extensions_Exceed_Limit()
        {
            var service = new LoanService();
            var loan = CreateLoan(new Reader { Id = 1, Name = "Ana" });

            var extensions = new List<LoanExtension>
            {
                new LoanExtension { Loan = loan, DaysExtended = 7, ExtensionDate = DateTime.Today },
                new LoanExtension { Loan = loan, DaysExtended = 7, ExtensionDate = DateTime.Today },
                new LoanExtension { Loan = loan, DaysExtended = 7, ExtensionDate = DateTime.Today }
            };

            Assert.Throws<InvalidOperationException>(() =>
                service.ValidateLoanExtensionLimit(
                    loan,
                    extensions,
                    2));
        }
    }
}
