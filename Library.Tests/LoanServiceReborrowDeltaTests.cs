using System;
using System.Collections.Generic;
using Library.Domain;
using Library.Service;
using Library.Tests.TestHelpers;
using Xunit;

namespace Library.Tests
{
    public class LoanServiceReborrowDeltaTests
    {
        private static BookItem CreateItem(Book book)
        {
            return new BookItem
            {
                Edition = new Edition
                {
                    Book = book,
                    Publisher = "Pub",
                    Year = 2024,
                    EditionNumber = 1,
                    Pages = 100
                }
            };
        }

        private static Loan CreateLoan(Reader reader, DateTime date, Book book)
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
        public void Throws_When_Reader_Is_Null()
        {
            var service = LoanServiceTestFactory.Create(reborrowDeltaDays: 10);
            var book = new Book { Id = 1, Title = "Test" };

            Assert.Throws<ArgumentNullException>(() =>
                service.ValidateBookReborrowDelta(
                    null!,
                    book,
                    DateTime.Today,
                    new List<Loan>()));
        }

        [Fact]
        public void Throws_When_Book_Is_Null()
        {
            var service = LoanServiceTestFactory.Create(reborrowDeltaDays: 10);
            var reader = new Reader { Id = 1, Name = "Ana" };

            Assert.Throws<ArgumentNullException>(() =>
                service.ValidateBookReborrowDelta(
                    reader,
                    null!,
                    DateTime.Today,
                    new List<Loan>()));
        }

        [Fact]
        public void Throws_When_PreviousLoans_Is_Null()
        {
            var service = LoanServiceTestFactory.Create(reborrowDeltaDays: 10);
            var reader = new Reader { Id = 1, Name = "Ana" };
            var book = new Book { Id = 1, Title = "Test" };

            Assert.Throws<ArgumentNullException>(() =>
                service.ValidateBookReborrowDelta(
                    reader,
                    book,
                    DateTime.Today,
                    null!));
        }

        [Fact]
        public void Throws_When_Delta_Is_Zero()
        {
            var service = LoanServiceTestFactory.Create(reborrowDeltaDays: 0);
            var reader = new Reader { Id = 1, Name = "Ana" };
            var book = new Book { Id = 1, Title = "Test" };

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                service.ValidateBookReborrowDelta(
                    reader,
                    book,
                    DateTime.Today,
                    new List<Loan>()));
        }

        [Fact]
        public void DoesNotThrow_When_Book_Was_Never_Borrowed()
        {
            var service = LoanServiceTestFactory.Create(reborrowDeltaDays: 10);
            var reader = new Reader { Id = 1, Name = "Ana" };
            var book = new Book { Id = 1, Title = "Test" };

            var ex = Record.Exception(() =>
                service.ValidateBookReborrowDelta(
                    reader,
                    book,
                    DateTime.Today,
                    new List<Loan>()));

            Assert.Null(ex);
        }

        [Fact]
        public void DoesNotThrow_When_Reborrow_After_Delta()
        {
            var service = LoanServiceTestFactory.Create(reborrowDeltaDays: 10);
            var reader = new Reader { Id = 1, Name = "Ana" };
            var book = new Book { Id = 1, Title = "Test" };

            var loans = new List<Loan>
            {
                CreateLoan(reader, DateTime.Today.AddDays(-11), book)
            };

            var ex = Record.Exception(() =>
                service.ValidateBookReborrowDelta(
                    reader,
                    book,
                    DateTime.Today,
                    loans));

            Assert.Null(ex);
        }

        [Fact]
        public void Throws_When_Reborrow_Too_Soon()
        {
            var service = LoanServiceTestFactory.Create(reborrowDeltaDays: 10);
            var reader = new Reader { Id = 1, Name = "Ana" };
            var book = new Book { Id = 1, Title = "Test" };

            var loans = new List<Loan>
            {
                CreateLoan(reader, DateTime.Today.AddDays(-3), book)
            };

            Assert.Throws<InvalidOperationException>(() =>
                service.ValidateBookReborrowDelta(
                    reader,
                    book,
                    DateTime.Today,
                    loans));
        }

        [Fact]
        public void Ignores_Loans_For_Different_Book()
        {
            var service = LoanServiceTestFactory.Create(reborrowDeltaDays: 10);
            var reader = new Reader { Id = 1, Name = "Ana" };

            var book1 = new Book { Id = 1, Title = "Test1" };
            var book2 = new Book { Id = 2, Title = "Test2" };

            var loans = new List<Loan>
            {
                CreateLoan(reader, DateTime.Today.AddDays(-2), book2)
            };

            var ex = Record.Exception(() =>
                service.ValidateBookReborrowDelta(
                    reader,
                    book1,
                    DateTime.Today,
                    loans));

            Assert.Null(ex);
        }

        [Fact]
        public void Ignores_Loans_For_Different_Reader()
        {
            var service = LoanServiceTestFactory.Create(reborrowDeltaDays: 10);

            var reader1 = new Reader { Id = 1, Name = "Ana" };
            var reader2 = new Reader { Id = 2, Name = "Ion" };
            var book = new Book { Id = 1, Title = "Test" };

            var loans = new List<Loan>
            {
                CreateLoan(reader2, DateTime.Today.AddDays(-2), book)
            };

            var ex = Record.Exception(() =>
                service.ValidateBookReborrowDelta(
                    reader1,
                    book,
                    DateTime.Today,
                    loans));

            Assert.Null(ex);
        }

        [Fact]
        public void Uses_Most_Recent_Loan()
        {
            var service = LoanServiceTestFactory.Create(reborrowDeltaDays: 10);
            var reader = new Reader { Id = 1, Name = "Ana" };
            var book = new Book { Id = 1, Title = "Test" };

            var loans = new List<Loan>
            {
                CreateLoan(reader, DateTime.Today.AddDays(-20), book),
                CreateLoan(reader, DateTime.Today.AddDays(-3), book)
            };

            Assert.Throws<InvalidOperationException>(() =>
                service.ValidateBookReborrowDelta(
                    reader,
                    book,
                    DateTime.Today,
                    loans));
        }
    }
}
