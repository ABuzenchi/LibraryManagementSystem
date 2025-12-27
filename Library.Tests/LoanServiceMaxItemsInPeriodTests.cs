using System;
using System.Collections.Generic;
using Library.Domain;
using Library.Service;
using Xunit;

namespace Library.Tests
{
    public class LoanServiceMaxItemsInPeriodTests
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

        private static Loan CreateLoan(Reader reader, DateTime date, int itemCount)
        {
            var loan = new Loan
            {
                Reader = reader,
                LoanDate = date,
                ReturnDueDate = date.AddDays(14)
            };

            for (int i = 0; i < itemCount; i++)
            {
                loan.LoanItems.Add(new LoanItems
                {
                    Loan = loan,
                    BookItem = CreateItem()
                });
            }

            return loan;
        }

        [Fact]
        public void Throws_When_Reader_Is_Null()
        {
            var service = new LoanService();

            Assert.Throws<ArgumentNullException>(() =>
                service.ValidateMaxItemsInPeriod(
                    null!,
                    DateTime.Today,
                    new List<Loan>(),
                    new List<BookItem>(),
                    7,
                    3));
        }

        [Fact]
        public void Throws_When_ExistingLoans_Is_Null()
        {
            var service = new LoanService();
            var reader = new Reader { Id = 1, Name = "Ana" };

            Assert.Throws<ArgumentNullException>(() =>
                service.ValidateMaxItemsInPeriod(
                    reader,
                    DateTime.Today,
                    null!,
                    new List<BookItem>(),
                    7,
                    3));
        }

        [Fact]
        public void Throws_When_NewItems_Is_Null()
        {
            var service = new LoanService();
            var reader = new Reader { Id = 1, Name = "Ana" };

            Assert.Throws<ArgumentNullException>(() =>
                service.ValidateMaxItemsInPeriod(
                    reader,
                    DateTime.Today,
                    new List<Loan>(),
                    null!,
                    7,
                    3));
        }

        [Fact]
        public void Throws_When_Period_Is_Zero()
        {
            var service = new LoanService();
            var reader = new Reader { Id = 1, Name = "Ana" };

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                service.ValidateMaxItemsInPeriod(
                    reader,
                    DateTime.Today,
                    new List<Loan>(),
                    new List<BookItem>(),
                    0,
                    3));
        }

        [Fact]
        public void Throws_When_MaxItems_Is_Zero()
        {
            var service = new LoanService();
            var reader = new Reader { Id = 1, Name = "Ana" };

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                service.ValidateMaxItemsInPeriod(
                    reader,
                    DateTime.Today,
                    new List<Loan>(),
                    new List<BookItem>(),
                    7,
                    0));
        }

        [Fact]
        public void DoesNotThrow_When_No_Loans_In_Period()
        {
            var service = new LoanService();
            var reader = new Reader { Id = 1, Name = "Ana" };

            var ex = Record.Exception(() =>
                service.ValidateMaxItemsInPeriod(
                    reader,
                    DateTime.Today,
                    new List<Loan>(),
                    new List<BookItem> { CreateItem() },
                    7,
                    2));

            Assert.Null(ex);
        }

        [Fact]
        public void DoesNotThrow_When_Exactly_At_Limit()
        {
            var service = new LoanService();
            var reader = new Reader { Id = 1, Name = "Ana" };
            var today = DateTime.Today;

            var loans = new List<Loan>
            {
                CreateLoan(reader, today.AddDays(-1), 1)
            };

            var ex = Record.Exception(() =>
                service.ValidateMaxItemsInPeriod(
                    reader,
                    today,
                    loans,
                    new List<BookItem> { CreateItem() },
                    7,
                    2));

            Assert.Null(ex);
        }

        [Fact]
        public void Throws_When_Limit_Exceeded()
        {
            var service = new LoanService();
            var reader = new Reader { Id = 1, Name = "Ana" };
            var today = DateTime.Today;

            var loans = new List<Loan>
            {
                CreateLoan(reader, today.AddDays(-1), 2)
            };

            Assert.Throws<InvalidOperationException>(() =>
                service.ValidateMaxItemsInPeriod(
                    reader,
                    today,
                    loans,
                    new List<BookItem> { CreateItem() },
                    7,
                    2));
        }

        [Fact]
        public void Ignores_Loans_Outside_Period()
        {
            var service = new LoanService();
            var reader = new Reader { Id = 1, Name = "Ana" };
            var today = DateTime.Today;

            var loans = new List<Loan>
            {
                CreateLoan(reader, today.AddDays(-10), 10)
            };

            var ex = Record.Exception(() =>
                service.ValidateMaxItemsInPeriod(
                    reader,
                    today,
                    loans,
                    new List<BookItem> { CreateItem() },
                    7,
                    1));

            Assert.Null(ex);
        }

        [Fact]
        public void Ignores_Loans_For_Other_Reader()
        {
            var service = new LoanService();
            var reader1 = new Reader { Id = 1, Name = "Ana" };
            var reader2 = new Reader { Id = 2, Name = "Ion" };

            var loans = new List<Loan>
            {
                CreateLoan(reader2, DateTime.Today, 10)
            };

            var ex = Record.Exception(() =>
                service.ValidateMaxItemsInPeriod(
                    reader1,
                    DateTime.Today,
                    loans,
                    new List<BookItem> { CreateItem() },
                    7,
                    1));

            Assert.Null(ex);
        }
    }
}
