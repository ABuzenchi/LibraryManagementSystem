using System;
using System.Collections.Generic;
using Library.Domain;
using Library.Service;
using Xunit;

namespace Library.Tests
{
    public class LoanServiceEdgeCaseTests
    {
        private static BookItem CreateItem()
        {
            return new BookItem
            {
                Edition = new Edition
                {
                    Book = new Book { Id = 1, Title = "Edge Book" },
                    Publisher = "Edge",
                    Year = 2024,
                    EditionNumber = 1,
                    Pages = 100
                }
            };
        }

        private static Loan CreateLoan(Reader reader, DateTime date, int items)
        {
            var loan = new Loan
            {
                Reader = reader,
                LoanDate = date,
                ReturnDueDate = date.AddDays(14)
            };

            for (int i = 0; i < items; i++)
            {
                loan.LoanItems.Add(new LoanItems
                {
                    Loan = loan,
                    BookItem = CreateItem()
                });
            }

            return loan;
        }

        // 1
        [Fact]
        public void DailyLoanLimit_Allows_DateTime_MinValue()
        {
            var service = new LoanService();
            var reader = new Reader { Id = 1, Name = "Ana" };

            var ex = Record.Exception(() =>
                service.ValidateDailyLoanLimit(
                    reader,
                    DateTime.MinValue,
                    new List<Loan>(),
                    new List<BookItem>(),
                    1));

            Assert.Null(ex);
        }

        // 2
        [Fact]
        public void DailyLoanLimit_Allows_DateTime_MaxValue()
        {
            var service = new LoanService();
            var reader = new Reader { Id = 1, Name = "Ana" };

            var ex = Record.Exception(() =>
                service.ValidateDailyLoanLimit(
                    reader,
                    DateTime.MaxValue,
                    new List<Loan>(),
                    new List<BookItem>(),
                    1));

            Assert.Null(ex);
        }

        // 3
        [Fact]
        public void MaxItemsInPeriod_Allows_Period_One_Day()
        {
            var service = new LoanService();
            var reader = new Reader { Id = 1, Name = "Ana" };

            var loans = new List<Loan>
            {
                CreateLoan(reader, DateTime.Today, 1)
            };

            var ex = Record.Exception(() =>
                service.ValidateMaxItemsInPeriod(
                    reader,
                    DateTime.Today,
                    loans,
                    new List<BookItem>(),
                    periodInDays: 1,
                    maxItemsInPeriod: 1));

            Assert.Null(ex);
        }

        // 4
        [Fact]
        public void MaxItemsInPeriod_Allows_Large_Period()
        {
            var service = new LoanService();
            var reader = new Reader { Id = 1, Name = "Ana" };

            var ex = Record.Exception(() =>
                service.ValidateMaxItemsInPeriod(
                    reader,
                    DateTime.Today,
                    new List<Loan>(),
                    new List<BookItem>(),
                    periodInDays: 365,
                    maxItemsInPeriod: 100));

            Assert.Null(ex);
        }

        // 5
        [Fact]
        public void LoanItemLimit_Allows_IntMaxValue_Limit()
        {
            var service = new LoanService();

            var ex = Record.Exception(() =>
                service.ValidateLoanItemLimit(
                    new List<BookItem> { CreateItem(), CreateItem() },
                    int.MaxValue));

            Assert.Null(ex);
        }

        // 6
        [Fact]
        public void DailyLoanLimit_Allows_Zero_New_Items()
        {
            var service = new LoanService();
            var reader = new Reader { Id = 1, Name = "Ana" };

            var ex = Record.Exception(() =>
                service.ValidateDailyLoanLimit(
                    reader,
                    DateTime.Today,
                    new List<Loan>(),
                    new List<BookItem>(),
                    1));

            Assert.Null(ex);
        }

        // 7
        [Fact]
        public void ReborrowDelta_Allows_DateTime_MinValue_LoanDate()
        {
            var service = new LoanService();
            var reader = new Reader { Id = 1, Name = "Ana" };
            var book = new Book { Id = 1, Title = "Test" };

            var ex = Record.Exception(() =>
                service.ValidateBookReborrowDelta(
                    reader,
                    book,
                    DateTime.MinValue,
                    new List<Loan>(),
                    10));

            Assert.Null(ex);
        }

        // 8
        [Fact]
        public void ReborrowDelta_Allows_DateTime_MaxValue_LoanDate()
        {
            var service = new LoanService();
            var reader = new Reader { Id = 1, Name = "Ana" };
            var book = new Book { Id = 1, Title = "Test" };

            var ex = Record.Exception(() =>
                service.ValidateBookReborrowDelta(
                    reader,
                    book,
                    DateTime.MaxValue,
                    new List<Loan>(),
                    10));

            Assert.Null(ex);
        }

        // 9
        [Fact]
        public void ExtensionLimit_Allows_Large_Limit()
        {
            var service = new LoanService();
            var loan = new Loan
            {
                Id = 1,
                Reader = new Reader { Id = 1, Name = "Ana" },
                LoanDate = DateTime.Today,
                ReturnDueDate = DateTime.Today.AddDays(14)
            };

            var ex = Record.Exception(() =>
                service.ValidateLoanExtensionLimit(
                    loan,
                    new List<LoanExtension>(),
                    int.MaxValue));

            Assert.Null(ex);
        }

        // 10
        [Fact]
        public void BookAvailability_Allows_Large_Number_Of_Copies()
        {
            var service = new LoanService();
            var book = new Book { Id = 1, Title = "Big Book" };

            var allItems = new List<BookItem>();
            for (int i = 0; i < 100; i++)
            {
                allItems.Add(CreateItem());
            }

            var loaned = new List<BookItem>(allItems.GetRange(0, 50));

            var ex = Record.Exception(() =>
                service.ValidateBookAvailabilityForLoan(
                    book,
                    allItems,
                    loaned));

            Assert.Null(ex);
        }

        // 11
        [Fact]
        public void MaxItemsInPeriod_Ignores_Loans_In_Future()
        {
            var service = new LoanService();
            var reader = new Reader { Id = 1, Name = "Ana" };

            var loans = new List<Loan>
            {
                CreateLoan(reader, DateTime.Today.AddDays(10), 10)
            };

            var ex = Record.Exception(() =>
                service.ValidateMaxItemsInPeriod(
                    reader,
                    DateTime.Today,
                    loans,
                    new List<BookItem>(),
                    7,
                    1));

            Assert.Null(ex);
        }

        // 12
        [Fact]
        public void DailyLoanLimit_Allows_Many_Past_Loans()
        {
            var service = new LoanService();
            var reader = new Reader { Id = 1, Name = "Ana" };

            var loans = new List<Loan>();
            for (int i = 1; i <= 30; i++)
            {
                loans.Add(CreateLoan(reader, DateTime.Today.AddDays(-i), 10));
            }

            var ex = Record.Exception(() =>
                service.ValidateDailyLoanLimit(
                    reader,
                    DateTime.Today,
                    loans,
                    new List<BookItem> { CreateItem() },
                    1));

            Assert.Null(ex);
        }
    }
}
