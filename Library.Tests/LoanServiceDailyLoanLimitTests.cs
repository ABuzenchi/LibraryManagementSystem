using System;
using System.Collections.Generic;
using Library.Domain;
using Library.Service;
using Library.Tests.TestHelpers;
using Xunit;

namespace Library.Tests
{
    public class LoanServiceDailyLoanLimitTests
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
            var service = LoanServiceTestFactory.Create();

            Assert.Throws<ArgumentNullException>(() =>
                service.ValidateDailyLoanLimit(
                    null!,
                    DateTime.Today,
                    new List<Loan>(),
                    new List<BookItem>()));
        }

        [Fact]
        public void Throws_When_ExistingLoans_Null()
        {
            var service = LoanServiceTestFactory.Create();
            var reader = new Reader { Id = 1, Name = "Ana" };

            Assert.Throws<ArgumentNullException>(() =>
                service.ValidateDailyLoanLimit(
                    reader,
                    DateTime.Today,
                    null!,
                    new List<BookItem>()));
        }

        [Fact]
        public void Throws_When_NewItems_Null()
        {
            var service = LoanServiceTestFactory.Create();
            var reader = new Reader { Id = 1, Name = "Ana" };

            Assert.Throws<ArgumentNullException>(() =>
                service.ValidateDailyLoanLimit(
                    reader,
                    DateTime.Today,
                    new List<Loan>(),
                    null!));
        }

        [Fact]
        public void Throws_When_Limit_Is_Zero()
        {
            var service = LoanServiceTestFactory.Create(maxItemsPerDay:0);
            var reader = new Reader { Id = 1, Name = "Ana" };

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                service.ValidateDailyLoanLimit(
                    reader,
                    DateTime.Today,
                    new List<Loan>(),
                    new List<BookItem>()));
        }

        [Fact]
        public void DoesNotThrow_When_No_Loans_Today()
        {
            var service = LoanServiceTestFactory.Create(maxItemsPerDay:2);
            var reader = new Reader { Id = 1, Name = "Ana" };

            var ex = Record.Exception(() =>
                service.ValidateDailyLoanLimit(
                    reader,
                    DateTime.Today,
                    new List<Loan>(),
                    new List<BookItem> { CreateItem() }));

            Assert.Null(ex);
        }

        [Fact]
        public void DoesNotThrow_When_Exactly_At_Daily_Limit()
        {
            var service = LoanServiceTestFactory.Create(maxItemsPerDay:2);
            var reader = new Reader { Id = 1, Name = "Ana" };
            var today = DateTime.Today;

            var loans = new List<Loan>
            {
                CreateLoan(reader, today, 1)
            };

            var ex = Record.Exception(() =>
                service.ValidateDailyLoanLimit(
                    reader,
                    today,
                    loans,
                    new List<BookItem> { CreateItem() }));

            Assert.Null(ex);
        }

        [Fact]
        public void Throws_When_Daily_Limit_Exceeded()
        {
            var service = LoanServiceTestFactory.Create(maxItemsPerDay:2);
            var reader = new Reader { Id = 1, Name = "Ana" };
            var today = DateTime.Today;

            var loans = new List<Loan>
            {
                CreateLoan(reader, today, 2)
            };

            Assert.Throws<InvalidOperationException>(() =>
                service.ValidateDailyLoanLimit(
                    reader,
                    today,
                    loans,
                    new List<BookItem> { CreateItem() }));
        }

        [Fact]
        public void Ignores_Loans_From_Other_Day()
        {
            var service = LoanServiceTestFactory.Create(maxItemsPerDay:1);
            var reader = new Reader { Id = 1, Name = "Ana" };
            var yesterday = DateTime.Today.AddDays(-1);

            var loans = new List<Loan>
            {
                CreateLoan(reader, yesterday, 10)
            };

            var ex = Record.Exception(() =>
                service.ValidateDailyLoanLimit(
                    reader,
                    DateTime.Today,
                    loans,
                    new List<BookItem> { CreateItem() }));

            Assert.Null(ex);
        }

        [Fact]
        public void Ignores_Loans_For_Other_Reader()
        {
            var service = LoanServiceTestFactory.Create(maxItemsPerDay:1);
            var reader1 = new Reader { Id = 1, Name = "Ana" };
            var reader2 = new Reader { Id = 2, Name = "Ion" };

            var loans = new List<Loan>
            {
                CreateLoan(reader2, DateTime.Today, 10)
            };

            var ex = Record.Exception(() =>
                service.ValidateDailyLoanLimit(
                    reader1,
                    DateTime.Today,
                    loans,
                    new List<BookItem> { CreateItem() }));

            Assert.Null(ex);
        }

        [Fact]
        public void DoesNotThrow_When_NewItems_Empty()
        {
            var service = LoanServiceTestFactory.Create(maxItemsPerDay:1);
            var reader = new Reader { Id = 1, Name = "Ana" };

            var ex = Record.Exception(() =>
                service.ValidateDailyLoanLimit(
                    reader,
                    DateTime.Today,
                    new List<Loan>(),
                    new List<BookItem>()));

            Assert.Null(ex);
        }
    }
}
