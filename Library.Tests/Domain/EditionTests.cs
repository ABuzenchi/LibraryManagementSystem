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

        [Fact]
        public void Edition_Publisher_IsSetCorrectly()
        {
            var edition = new Edition
            {
                Book = new Book { Id = 1, Title = "Test" },
                Publisher = "Test Publisher",
                Year = 2024,
                EditionNumber = 1,
                Pages = 150
            };

            Assert.Equal("Test Publisher", edition.Publisher);
        }

        [Fact]
        public void Edition_Year_IsSetCorrectly()
        {
            var edition = new Edition
            {
                Book = new Book { Id = 1, Title = "Test" },
                Publisher = "Pub",
                Year = 2020,
                EditionNumber = 1,
                Pages = 150
            };

            Assert.Equal(2020, edition.Year);
        }

        [Fact]
        public void Edition_EditionNumber_IsSetCorrectly()
        {
            var edition = new Edition
            {
                Book = new Book { Id = 1, Title = "Test" },
                Publisher = "Pub",
                Year = 2024,
                EditionNumber = 3,
                Pages = 150
            };

            Assert.Equal(3, edition.EditionNumber);
        }

        [Fact]
        public void Edition_Pages_CanBeChanged()
        {
            var edition = new Edition
            {
                Book = new Book { Id = 1, Title = "Test" },
                Publisher = "Pub",
                Year = 2024,
                EditionNumber = 1,
                Pages = 100
            };

            edition.Pages = 300;

            Assert.Equal(300, edition.Pages);
        }

        [Fact]
        public void Edition_BookItems_CanAdd_Item()
        {
            var edition = new Edition
            {
                Book = new Book { Id = 1, Title = "Test" },
                Publisher = "Pub",
                Year = 2024,
                EditionNumber = 1,
                Pages = 200
            };

            var item = new BookItem
            {
                Edition = edition
            };

            edition.BookItems.Add(item);

            Assert.Single(edition.BookItems);
        }

        [Fact]
        public void Edition_BookItems_CanAdd_Multiple_Items()
        {
            var edition = new Edition
            {
                Book = new Book { Id = 1, Title = "Test" },
                Publisher = "Pub",
                Year = 2024,
                EditionNumber = 1,
                Pages = 200
            };

            edition.BookItems.Add(new BookItem { Edition = edition });
            edition.BookItems.Add(new BookItem { Edition = edition });

            Assert.Equal(2, edition.BookItems.Count);
        }

        [Fact]
        public void Edition_Id_Defaults_To_Zero()
        {
            var edition = new Edition
            {
                Book = new Book { Id = 1, Title = "Test" },
                Publisher = "Pub",
                Year = 2024,
                EditionNumber = 1,
                Pages = 200
            };

            Assert.Equal(0, edition.Id);
        }

    }
}
