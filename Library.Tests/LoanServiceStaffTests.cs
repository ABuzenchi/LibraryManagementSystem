using System;
using System.Collections.Generic;
using Library.Domain;
using Library.Service;
using Library.Tests.TestHelpers;
using Xunit;

namespace Library.Tests
{
    public class LoanServiceStaffTests
    {
        private static BookItem CreateItem()
        {
            return new BookItem
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
        }

        [Fact]
        public void Staff_Ignores_DailyLoanLimit()
        {
            var reader = new Reader
            {
                Id = 1,
                Name = "Ana",
                IsStaff = true
            };

            var today = DateTime.Today;

            var loan = new Loan
            {
                Reader = reader,
                LoanDate = today,
                ReturnDueDate = today.AddDays(14)
            };

            loan.LoanItems.Add(new LoanItems
            {
                Loan = loan,
                BookItem = CreateItem()
            });

            var existingLoans = new List<Loan> { loan };

            var newItems = new List<BookItem>
            {
                CreateItem(),
                CreateItem()
            };

            var service = LoanServiceTestFactory.Create(maxItemsPerDay:1);

            var exception = Record.Exception(() =>
                service.ValidateDailyLoanLimit(
                    reader,
                    today,
                    existingLoans,
                    newItems));

            Assert.Null(exception);
        }

        [Fact]
        public void Staff_Has_Smaller_Reborrow_Delta()
        {
            var reader = new Reader
            {
                Id = 1,
                Name = "Ana",
                IsStaff = true
            };

            var book = new Book { Id = 1, Title = "Test" };

            var previousLoan = new Loan
            {
                Reader = reader,
                LoanDate = DateTime.Today.AddDays(-5),
                ReturnDueDate = DateTime.Today.AddDays(10)
            };

            previousLoan.LoanItems.Add(new LoanItems
            {
                Loan = previousLoan,
                BookItem = CreateItem()
            });

            var service = LoanServiceTestFactory.Create(reborrowDeltaDays:10);

            var exception = Record.Exception(() =>
                service.ValidateBookReborrowDelta(
                    reader,
                    book,
                    DateTime.Today,
                    new List<Loan> { previousLoan }));

            Assert.Null(exception);
        }

        [Fact]
        public void Staff_Can_Borrow_More_Items_In_Period()
        {
            var reader = new Reader
            {
                Id = 1,
                Name = "Ana",
                IsStaff = true
            };

            var today = DateTime.Today;

            var loan = new Loan
            {
                Reader = reader,
                LoanDate = today.AddDays(-1),
                ReturnDueDate = today.AddDays(10)
            };

            loan.LoanItems.Add(new LoanItems
            {
                Loan = loan,
                BookItem = CreateItem()
            });

            loan.LoanItems.Add(new LoanItems
            {
                Loan = loan,
                BookItem = CreateItem()
            });

            var existingLoans = new List<Loan> { loan };

            var newItems = new List<BookItem>
            {
                CreateItem(),
                CreateItem()
            };

            var service = LoanServiceTestFactory.Create(periodDays:7,maxItemsInPeriod:2);

            var exception = Record.Exception(() =>
                service.ValidateMaxItemsInPeriod(
                    reader,
                    today,
                    existingLoans,
                    newItems));

            Assert.Null(exception);
        }

        [Fact]
        public void Staff_Can_Extend_Loan_More_Times()
        {
            var reader = new Reader
            {
                Id = 1,
                Name = "Ana",
                IsStaff = true
            };

            var loan = new Loan
            {
                Id = 1,
                Reader = reader,
                LoanDate = DateTime.Today,
                ReturnDueDate = DateTime.Today.AddDays(14)
            };

            var extensions = new List<LoanExtension>
            {
                new LoanExtension
                {
                    Loan = loan,
                    DaysExtended = 7,
                    ExtensionDate = DateTime.Today
                },
                new LoanExtension
                {
                    Loan = loan,
                    DaysExtended = 7,
                    ExtensionDate = DateTime.Today
                }
            };

            var service =LoanServiceTestFactory.Create(maxLoanExtensions:2);

            var exception = Record.Exception(() =>
                service.ValidateLoanExtensionLimit(
                    loan,
                    extensions));

            Assert.Null(exception);
        }
    }
}
