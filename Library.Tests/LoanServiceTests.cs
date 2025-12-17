using System;
using System.Collections.Generic;
using Library.Domain;
using Library.Service;
using Xunit;

namespace Library.Tests
{
    public class LoanServiceTests
    {
        private static BookItem CreateValidBookItem()
        {
            return new BookItem
            {
                Edition = new Edition
                {
                    Book = new Book
                    {
                        Id = 1,
                        Title = "Test Book"
                    },
                    Publisher = "Test Publisher",
                    Year = 2024,
                    EditionNumber = 1,
                    Pages = 100
                },
                IsReadingRoomOnly = false
            };
        }

        private static Loan CreateLoan(
            Reader reader,
            DateTime loanDate,
            int numberOfItems)
        {
            var loan = new Loan
            {
                Reader = reader,
                LoanDate = loanDate,
                ReturnDueDate = loanDate.AddDays(14)
            };

            for (int i = 0; i < numberOfItems; i++)
            {
                loan.LoanItems.Add(new LoanItems
                {
                    Loan = loan,
                    BookItem = CreateValidBookItem()
                });
            }

            return loan;
        }

        [Fact]
        public void ValidateLoanItemsLimit_ThrowsException_WhenLimitIsExceeded()
        {
            var items = new List<BookItem>
            {
                CreateValidBookItem(),
                CreateValidBookItem(),
                CreateValidBookItem()
            };

            var service = new LoanService();

            Assert.Throws<InvalidOperationException>(() =>
                service.ValidateLoanItemLimit(items, maxItemsPerLoan: 2));
        }

        [Fact]
        public void ValidateLoanItemsLimit_DoesNotThrow_WhenWithinLimit()
        {
            var items = new List<BookItem>
            {
                CreateValidBookItem(),
                CreateValidBookItem()
            };

            var service = new LoanService();

            var exception = Record.Exception(() =>
                service.ValidateLoanItemLimit(items, maxItemsPerLoan: 2));

            Assert.Null(exception);
        }

        [Fact]
        public void ValidateDailyLoanLimit_ThrowsException_WhenDailyLimitIsExceeded()
        {
            var reader = new Reader { Id = 1, Name = "Ana" };
            var today = DateTime.Today;

            var existingLoans = new List<Loan>
            {
                CreateLoan(reader, today, 2)
            };

            var newItems = new List<BookItem>
            {
                CreateValidBookItem()
            };

            var service = new LoanService();

            Assert.Throws<InvalidOperationException>(() =>
                service.ValidateDailyLoanLimit(
                    reader,
                    today,
                    existingLoans,
                    newItems,
                    maxItemsPerDay: 2));
        }

        [Fact]
        public void ValidateDailyLoanLimit_DoesNotThrow_WhenWithinDailyLimit()
        {
            var reader = new Reader { Id = 1, Name = "Ana" };
            var today = DateTime.Today;

            var existingLoans = new List<Loan>
            {
                CreateLoan(reader, today, 1)
            };

            var newItems = new List<BookItem>
            {
                CreateValidBookItem()
            };

            var service = new LoanService();

            var exception = Record.Exception(() =>
                service.ValidateDailyLoanLimit(
                    reader,
                    today,
                    existingLoans,
                    newItems,
                    maxItemsPerDay: 2));

            Assert.Null(exception);
        }
    }
}
