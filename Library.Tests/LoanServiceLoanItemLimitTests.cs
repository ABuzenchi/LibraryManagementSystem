using System;
using System.Collections.Generic;
using Library.Domain;
using Library.Service;
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
            var service = new LoanService();

            Assert.Throws<ArgumentNullException>(() =>
                service.ValidateLoanItemLimit(null!, 2));
        }

        [Fact]
        public void Throws_When_Limit_Is_Zero()
        {
            var service = new LoanService();

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                service.ValidateLoanItemLimit(new List<BookItem>(), 0));
        }

        [Fact]
        public void Throws_When_Limit_Is_Negative()
        {
            var service = new LoanService();

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                service.ValidateLoanItemLimit(new List<BookItem>(), -1));
        }

        [Fact]
        public void DoesNotThrow_When_Items_Empty()
        {
            var service = new LoanService();

            var ex = Record.Exception(() =>
                service.ValidateLoanItemLimit(new List<BookItem>(), 3));

            Assert.Null(ex);
        }

        [Fact]
        public void DoesNotThrow_When_Items_Below_Limit()
        {
            var service = new LoanService();

            var items = new List<BookItem>
            {
                CreateItem()
            };

            var ex = Record.Exception(() =>
                service.ValidateLoanItemLimit(items, 2));

            Assert.Null(ex);
        }

        [Fact]
        public void DoesNotThrow_When_Items_Exactly_At_Limit()
        {
            var service = new LoanService();

            var items = new List<BookItem>
            {
                CreateItem(),
                CreateItem()
            };

            var ex = Record.Exception(() =>
                service.ValidateLoanItemLimit(items, 2));

            Assert.Null(ex);
        }

        [Fact]
        public void Throws_When_Items_Above_Limit()
        {
            var service = new LoanService();

            var items = new List<BookItem>
            {
                CreateItem(),
                CreateItem(),
                CreateItem()
            };

            Assert.Throws<InvalidOperationException>(() =>
                service.ValidateLoanItemLimit(items, 2));
        }

        [Fact]
        public void Throws_When_Limit_Is_One_And_More_Items()
        {
            var service = new LoanService();

            var items = new List<BookItem>
            {
                CreateItem(),
                CreateItem()
            };

            Assert.Throws<InvalidOperationException>(() =>
                service.ValidateLoanItemLimit(items, 1));
        }

        [Fact]
        public void DoesNotThrow_When_Limit_Is_Large()
        {
            var service = new LoanService();

            var items = new List<BookItem>
            {
                CreateItem(),
                CreateItem(),
                CreateItem()
            };

            var ex = Record.Exception(() =>
                service.ValidateLoanItemLimit(items, 100));

            Assert.Null(ex);
        }

        [Fact]
        public void DoesNotThrow_With_Different_Items_Same_Count()
        {
            var service = new LoanService();

            var items = new List<BookItem>
            {
                CreateItem(),
                CreateItem()
            };

            var ex = Record.Exception(() =>
                service.ValidateLoanItemLimit(items, 2));

            Assert.Null(ex);
        }
    }
}
