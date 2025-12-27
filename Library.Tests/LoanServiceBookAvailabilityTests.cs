using System;
using System.Collections.Generic;
using Library.Domain;
using Library.Service;
using Xunit;

namespace Library.Tests
{
    public class LoanServiceBookAvailabilityTests
    {
        private static Book CreateBook()
        {
            return new Book { Id = 1, Title = "Test" };
        }

        private static BookItem CreateItem(Book book, bool readingRoomOnly = false)
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
                },
                IsReadingRoomOnly = readingRoomOnly
            };
        }

        [Fact]
        public void Throws_When_Book_Is_Null()
        {
            var service = new LoanService();

            Assert.Throws<ArgumentNullException>(() =>
                service.ValidateBookAvailabilityForLoan(
                    null!,
                    new List<BookItem>(),
                    new List<BookItem>()));
        }

        [Fact]
        public void Throws_When_AllItems_Is_Null()
        {
            var service = new LoanService();
            var book = CreateBook();

            Assert.Throws<ArgumentNullException>(() =>
                service.ValidateBookAvailabilityForLoan(
                    book,
                    null!,
                    new List<BookItem>()));
        }

        [Fact]
        public void Throws_When_LoanedItems_Is_Null()
        {
            var service = new LoanService();
            var book = CreateBook();

            Assert.Throws<ArgumentNullException>(() =>
                service.ValidateBookAvailabilityForLoan(
                    book,
                    new List<BookItem>(),
                    null!));
        }

        [Fact]
        public void Throws_When_No_Copies_Exist()
        {
            var service = new LoanService();
            var book = CreateBook();

            Assert.Throws<InvalidOperationException>(() =>
                service.ValidateBookAvailabilityForLoan(
                    book,
                    new List<BookItem>(),
                    new List<BookItem>()));
        }

        [Fact]
        public void Throws_When_All_Copies_Are_ReadingRoomOnly()
        {
            var service = new LoanService();
            var book = CreateBook();

            var items = new List<BookItem>
            {
                CreateItem(book, true),
                CreateItem(book, true)
            };

            Assert.Throws<InvalidOperationException>(() =>
                service.ValidateBookAvailabilityForLoan(
                    book,
                    items,
                    new List<BookItem>()));
        }

        [Fact]
        public void DoesNotThrow_When_Enough_Copies_Available()
        {
            var service = new LoanService();
            var book = CreateBook();

            var allItems = new List<BookItem>
            {
                CreateItem(book),
                CreateItem(book),
                CreateItem(book),
                CreateItem(book),
                CreateItem(book),
                CreateItem(book),
                CreateItem(book),
                CreateItem(book),
                CreateItem(book),
                CreateItem(book)
            };

            var loanedItems = new List<BookItem>
            {
                allItems[0],
                allItems[1]
            };

            var ex = Record.Exception(() =>
                service.ValidateBookAvailabilityForLoan(
                    book,
                    allItems,
                    loanedItems));

            Assert.Null(ex);
        }

        [Fact]
        public void Throws_When_Available_Copies_Below_TenPercent()
        {
            var service = new LoanService();
            var book = CreateBook();

            var allItems = new List<BookItem>();
            for (int i = 0; i < 10; i++)
            {
                allItems.Add(CreateItem(book));
            }

            // TOATE exemplarele sunt împrumutate → 0% disponibile
            var loanedItems = new List<BookItem>(allItems);

            Assert.Throws<InvalidOperationException>(() =>
                service.ValidateBookAvailabilityForLoan(
                    book,
                    allItems,
                    loanedItems));
        }


        [Fact]
        public void DoesNotThrow_When_Available_Exactly_TenPercent()
        {
            var service = new LoanService();
            var book = CreateBook();

            var allItems = new List<BookItem>();
            for (int i = 0; i < 10; i++)
            {
                allItems.Add(CreateItem(book));
            }

            var loanedItems = new List<BookItem>
            {
                allItems[0],
                allItems[1],
                allItems[2],
                allItems[3],
                allItems[4],
                allItems[5],
                allItems[6],
                allItems[7],
                allItems[8]
            };

            var ex = Record.Exception(() =>
                service.ValidateBookAvailabilityForLoan(
                    book,
                    allItems,
                    loanedItems));

            Assert.Null(ex);
        }
    }
}
