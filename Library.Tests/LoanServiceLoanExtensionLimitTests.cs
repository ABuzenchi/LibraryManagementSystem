using System;
using System.Collections.Generic;
using Library.Domain;
using Library.Service;
using Library.Tests.TestHelpers;
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
            var service = LoanServiceTestFactory.Create(maxLoanExtensions: 2);

            Assert.Throws<ArgumentNullException>(() =>
                service.ValidateLoanExtensionLimit(
                    null!,
                    new List<LoanExtension>()));
        }

        [Fact]
        public void Throws_When_Extensions_Is_Null()
        {
            var service = LoanServiceTestFactory.Create(maxLoanExtensions: 2);
            var loan = CreateLoan(new Reader { Id = 1, Name = "Ana" });

            Assert.Throws<ArgumentNullException>(() =>
                service.ValidateLoanExtensionLimit(
                    loan,
                    null!));
        }

        [Fact]
        public void Throws_When_Limit_Is_Zero()
        {
            var service = LoanServiceTestFactory.Create(maxLoanExtensions: 0);
            var loan = CreateLoan(new Reader { Id = 1, Name = "Ana" });

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                service.ValidateLoanExtensionLimit(
                    loan,
                    new List<LoanExtension>()));
        }

        [Fact]
        public void DoesNotThrow_When_No_Extensions()
        {
            var service = LoanServiceTestFactory.Create(maxLoanExtensions: 2);
            var loan = CreateLoan(new Reader { Id = 1, Name = "Ana" });

            var ex = Record.Exception(() =>
                service.ValidateLoanExtensionLimit(
                    loan,
                    new List<LoanExtension>()));

            Assert.Null(ex);
        }

        [Fact]
        public void DoesNotThrow_When_Below_Limit()
        {
            var service = LoanServiceTestFactory.Create(maxLoanExtensions: 2);
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
                    extensions));

            Assert.Null(ex);
        }

        [Fact]
        public void Throws_When_At_Limit()
        {
            var service = LoanServiceTestFactory.Create(maxLoanExtensions: 2);
            var loan = CreateLoan(new Reader { Id = 1, Name = "Ana" });

            var extensions = new List<LoanExtension>
            {
                new LoanExtension { Loan = loan, DaysExtended = 7, ExtensionDate = DateTime.Today },
                new LoanExtension { Loan = loan, DaysExtended = 7, ExtensionDate = DateTime.Today }
            };

            Assert.Throws<InvalidOperationException>(() =>
                service.ValidateLoanExtensionLimit(
                    loan,
                    extensions));
        }

        [Fact]
        public void Counts_Only_Extensions_For_Same_Loan()
        {
            var service = LoanServiceTestFactory.Create(maxLoanExtensions:1);

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
                    extensions));

            Assert.Null(ex);
        }


        [Fact]
        public void Throws_When_Extensions_Exceed_Limit()
        {
            var service = LoanServiceTestFactory.Create(maxLoanExtensions:2);
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
                    extensions));
        }
    }
}
