using Library.Domain;
using Xunit;

namespace Library.Tests.Domain
{
    public class EditionTests
    {
        [Fact]
        public void Edition_CanBeCreated_WithValidData()
        {
            var edition = new Edition
            {
                Book = new Book { Id = 1, Title = "Test" },
                Publisher = "Pub",
                Year = 2024,
                EditionNumber = 1,
                Pages = 200
            };

            Assert.Equal(200, edition.Pages);
        }

        [Fact]
        public void Edition_BelongsToBook()
        {
            var book = new Book { Id = 1, Title = "Test" };

            var edition = new Edition
            {
                Book = book,
                Publisher = "Pub",
                Year = 2024,
                EditionNumber = 1,
                Pages = 200
            };

            Assert.Equal(book, edition.Book);
        }

        [Fact]
        public void Edition_BookItems_IsInitialized()
        {
            var edition = new Edition
            {
                Book = new Book { Id = 1, Title = "Test" },
                Publisher = "Pub",
                Year = 2024,
                EditionNumber = 1,
                Pages = 200
            };

            Assert.NotNull(edition.BookItems);
        }
    }
}
