using System;
using System.Collections.Generic;
using Library.Domain;
using Library.Service;
using Library.Tests.TestHelpers;
using Xunit;

namespace Library.Tests
{
    public class LoanServiceDeltaTests
    {
        private static Book CreateBook(int id) =>
            new Book { Id = id, Title = $"Book {id}" };

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

        private static Loan CreateLoan(
            Reader reader,
            DateTime date,
            Book book)
        {
            var loan = new Loan
            {
                Reader = reader,
                LoanDate = date,
                ReturnDueDate = date.AddDays(14)
            };

            loan.LoanItems.Add(new LoanItems
            {
                Loan = loan,
                BookItem = CreateItem(book)
            });

            return loan;
        }

        [Fact]
        public void Throws_WhenReborrowedWithinDelta()
        {
            var reader = new Reader { Id = 1, Name = "Ana" };
            var book = CreateBook(1);

            var previousLoans = new List<Loan>
            {
                CreateLoan(reader, DateTime.Today.AddDays(-5), book)
            };

            var service = LoanServiceTestFactory.Create(reborrowDeltaDays:10);
            Assert.Throws<InvalidOperationException>(() =>
                service.ValidateBookReborrowDelta(
                    reader,
                    book,
                    DateTime.Today,
                    previousLoans));
        }

        [Fact]
        public void DoesNotThrow_WhenReborrowedAfterDelta()
        {
            var reader = new Reader { Id = 1, Name = "Ana" };
            var book = CreateBook(1);

            var previousLoans = new List<Loan>
            {
                CreateLoan(reader, DateTime.Today.AddDays(-15), book)
            };

            var service = LoanServiceTestFactory.Create(reborrowDeltaDays:10);

            var exception = Record.Exception(() =>
                service.ValidateBookReborrowDelta(
                    reader,
                    book,
                    DateTime.Today,
                    previousLoans));

            Assert.Null(exception);
        }

        [Fact]
        public void DoesNotThrow_WhenBookWasNeverBorrowed()
        {
            var reader = new Reader { Id = 1, Name = "Ana" };
            var book = CreateBook(1);

            var service = LoanServiceTestFactory.Create(reborrowDeltaDays:10);

            var exception = Record.Exception(() =>
                service.ValidateBookReborrowDelta(
                    reader,
                    book,
                    DateTime.Today,
                    new List<Loan>()));

            Assert.Null(exception);
        }

        [Fact]
        public void DoesNotThrow_WhenDifferentBookWasBorrowed()
        {
            var reader = new Reader { Id = 1, Name = "Ana" };
            var book1 = CreateBook(1);
            var book2 = CreateBook(2);

            var previousLoans = new List<Loan>
            {
                CreateLoan(reader, DateTime.Today.AddDays(-2), book2)
            };

            var service = LoanServiceTestFactory.Create(reborrowDeltaDays:10);

            var exception = Record.Exception(() =>
                service.ValidateBookReborrowDelta(
                    reader,
                    book1,
                    DateTime.Today,
                    previousLoans));

            Assert.Null(exception);
        }
    }
}
