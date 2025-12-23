using System;
using System.Collections.Generic;
using Library.Domain;
using Library.Service;
using Xunit;

namespace Library.Tests
{
    public class LoanServicePeriodLimitTests
    {
        private static BookItem CreateItem(Book book)
        {
            return new BookItem
            {
                Edition = new Edition
                {
                    Book = book,
                    Publisher = "Test",
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
                    BookItem = CreateItem(new Book { Id = i + 1, Title = $"Book {i}" })
                });
            }

            return loan;
        }

        [Fact]
        public void Throws_WhenPeriodLimitIsExceeded()
        {
            var reader = new Reader { Id = 1, Name = "Ana" };
            var today = DateTime.Today;

            var loans = new List<Loan>
            {
                CreateLoan(reader, today.AddDays(-5), 2)
            };

            var newItems = new List<BookItem>
            {
                CreateItem(new Book { Id = 3, Title = "B3" })
            };

            var service = new LoanService();

            Assert.Throws<InvalidOperationException>(() =>
                service.ValidateMaxItemsInPeriod(
                    reader, today, loans, newItems,
                    periodInDays: 7, maxItemsInPeriod: 2));
        }

        [Fact]
        public void DoesNotThrow_WhenExactlyAtPeriodLimit()
        {
            var reader = new Reader { Id = 1, Name = "Ana" };
            var today = DateTime.Today;

            var loans = new List<Loan>
            {
                CreateLoan(reader, today.AddDays(-5), 1)
            };

            var newItems = new List<BookItem>
            {
                CreateItem(new Book { Id = 2, Title = "B2" })
            };

            var service = new LoanService();

            var ex = Record.Exception(() =>
                service.ValidateMaxItemsInPeriod(
                    reader, today, loans, newItems,
                    periodInDays: 7, maxItemsInPeriod: 2));

            Assert.Null(ex);
        }

        [Fact]
        public void IgnoresLoansOutsidePeriod()
        {
            var reader = new Reader { Id = 1, Name = "Ana" };
            var today = DateTime.Today;

            var loans = new List<Loan>
            {
                CreateLoan(reader, today.AddDays(-20), 10)
            };

            var newItems = new List<BookItem>
            {
                CreateItem(new Book { Id = 1, Title = "B1" })
            };

            var service = new LoanService();

            var ex = Record.Exception(() =>
                service.ValidateMaxItemsInPeriod(
                    reader, today, loans, newItems,
                    periodInDays: 7, maxItemsInPeriod: 2));

            Assert.Null(ex);
        }

        [Fact]
        public void Throws_WhenMultipleLoansInPeriodExceedLimit()
        {
            var reader = new Reader { Id = 1, Name = "Ana" };
            var today = DateTime.Today;

            var loans = new List<Loan>
            {
                CreateLoan(reader, today.AddDays(-3), 1),
                CreateLoan(reader, today.AddDays(-1), 1)
            };

            var newItems = new List<BookItem>
            {
                CreateItem(new Book { Id = 3, Title = "B3" })
            };

            var service = new LoanService();

            Assert.Throws<InvalidOperationException>(() =>
                service.ValidateMaxItemsInPeriod(
                    reader, today, loans, newItems,
                    periodInDays: 7, maxItemsInPeriod: 2));
        }
    }
}
