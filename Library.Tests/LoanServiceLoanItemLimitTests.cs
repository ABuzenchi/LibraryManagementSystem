using System;
using System.Collections.Generic;
using Library.Domain;
using Library.Service;
using Library.Tests.TestHelpers;
using Xunit;

namespace Library.Tests
{
    public class LoanServiceLoanItemLimitTests
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
        public void Throws_When_Items_Null()
        {
            var service = LoanServiceTestFactory.Create(maxItemsPerLoan:2);

            Assert.Throws<ArgumentNullException>(() =>
                service.ValidateLoanItemLimit(null!));
        }

        [Fact]
        public void Throws_When_Limit_Is_Zero()
        {
            var service = LoanServiceTestFactory.Create(maxItemsPerLoan:0);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                service.ValidateLoanItemLimit(new List<BookItem>()));
        }

        [Fact]
        public void Throws_When_Limit_Is_Negative()
        {
            var service = LoanServiceTestFactory.Create(maxItemsPerLoan:-1);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                service.ValidateLoanItemLimit(new List<BookItem>()));
        }

        [Fact]
        public void DoesNotThrow_When_Items_Empty()
        {
            var service = LoanServiceTestFactory.Create(maxItemsPerLoan:3);

            var ex = Record.Exception(() =>
                service.ValidateLoanItemLimit(new List<BookItem>()));

            Assert.Null(ex);
        }

        [Fact]
        public void DoesNotThrow_When_Items_Below_Limit()
        {
            var service = LoanServiceTestFactory.Create(maxItemsPerLoan:2);

            var items = new List<BookItem>
            {
                CreateItem()
            };

            var ex = Record.Exception(() =>
                service.ValidateLoanItemLimit(items));

            Assert.Null(ex);
        }

        [Fact]
        public void DoesNotThrow_When_Items_Exactly_At_Limit()
        {
            var service = LoanServiceTestFactory.Create(maxItemsPerLoan:2);

            var items = new List<BookItem>
            {
                CreateItem(),
                CreateItem()
            };

            var ex = Record.Exception(() =>
                service.ValidateLoanItemLimit(items));

            Assert.Null(ex);
        }

        [Fact]
        public void Throws_When_Items_Above_Limit()
        {
            var service = LoanServiceTestFactory.Create(maxItemsPerLoan:2);

            var items = new List<BookItem>
            {
                CreateItem(),
                CreateItem(),
                CreateItem()
            };

            Assert.Throws<InvalidOperationException>(() =>
                service.ValidateLoanItemLimit(items));
        }

        [Fact]
        public void Throws_When_Limit_Is_One_And_More_Items()
        {
            var service = LoanServiceTestFactory.Create(maxItemsPerLoan:1);

            var items = new List<BookItem>
            {
                CreateItem(),
                CreateItem()
            };

            Assert.Throws<InvalidOperationException>(() =>
                service.ValidateLoanItemLimit(items));
        }

        [Fact]
        public void DoesNotThrow_When_Limit_Is_Large()
        {
            var service = LoanServiceTestFactory.Create(maxItemsPerLoan:100);

            var items = new List<BookItem>
            {
                CreateItem(),
                CreateItem(),
                CreateItem()
            };

            var ex = Record.Exception(() =>
                service.ValidateLoanItemLimit(items));

            Assert.Null(ex);
        }

        [Fact]
        public void DoesNotThrow_With_Different_Items_Same_Count()
        {
            var service = LoanServiceTestFactory.Create(maxItemsPerLoan:2);

            var items = new List<BookItem>
            {
                CreateItem(),
                CreateItem()
            };

            var ex = Record.Exception(() =>
                service.ValidateLoanItemLimit(items));

            Assert.Null(ex);
        }
    }
}
