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
    }
}
