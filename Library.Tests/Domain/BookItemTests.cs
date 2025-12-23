using Library.Domain;
using Xunit;

namespace Library.Tests.Domain
{
    public class BookItemTests
    {
        [Fact]
        public void BookItem_CanBeCreated_WithValidEdition()
        {
            var item = new BookItem
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

            Assert.NotNull(item.Edition);
        }

        [Fact]
        public void BookItem_IsReadingRoomOnly_DefaultsToFalse()
        {
            var item = new BookItem
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

            Assert.False(item.IsReadingRoomOnly);
        }
    }
}
