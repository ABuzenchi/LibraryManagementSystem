using System;
using System.Collections.Generic;
using Library.Domain;
using Library.Service;
using Library.Tests.TestHelpers;
using Xunit;

namespace Library.Tests
{
    public class LoanServiceAvailabilityTests
    {
        private static Book CreateBook() =>
            new Book { Id = 1, Title = "Test Book" };

        private static BookItem CreateItem(Book book, bool readingRoomOnly = false)
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
                },
                IsReadingRoomOnly = readingRoomOnly
            };
        }

        [Fact]
        public void Throws_WhenAllCopiesAreReadingRoomOnly()
        {
            var book = CreateBook();
            var items = new List<BookItem>
            {
                CreateItem(book, true),
                CreateItem(book, true)
            };

            var service = LoanServiceTestFactory.Create();

            Assert.Throws<InvalidOperationException>(() =>
                service.ValidateBookAvailabilityForLoan(
                    book,
                    items,
                    new List<BookItem>()));
        }

        [Fact]
        public void Throws_WhenAvailableCopiesBelowTenPercent()
        {
            var book = CreateBook();
            var items = new List<BookItem>();

            for (int i = 0; i < 10; i++)
            {
                items.Add(CreateItem(book));
            }

            var loaned = new List<BookItem>(items);

            var service = LoanServiceTestFactory.Create();

            Assert.Throws<InvalidOperationException>(() =>
                service.ValidateBookAvailabilityForLoan(
                    book,
                    items,
                    loaned));
        }

        [Fact]
        public void DoesNotThrow_WhenExactlyTenPercentAvailable()
        {
            var book = CreateBook();
            var items = new List<BookItem>();

            for (int i = 0; i < 10; i++)
            {
                items.Add(CreateItem(book));
            }

            var loaned = new List<BookItem>(items.Take(8));

            var service = LoanServiceTestFactory.Create();

            var exception = Record.Exception(() =>
                service.ValidateBookAvailabilityForLoan(
                    book,
                    items,
                    loaned));

            Assert.Null(exception);
        }

        [Fact]
        public void DoesNotThrow_WhenMoreThanTenPercentAvailable()
        {
            var book = CreateBook();
            var items = new List<BookItem>();

            for (int i = 0; i < 20; i++)
            {
                items.Add(CreateItem(book));
            }

            var loaned = new List<BookItem>(items.Take(15));

            var service = LoanServiceTestFactory.Create();

            var exception = Record.Exception(() =>
                service.ValidateBookAvailabilityForLoan(
                    book,
                    items,
                    loaned));

            Assert.Null(exception);
        }
    }
}
