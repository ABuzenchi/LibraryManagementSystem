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

        [Fact]
        public void BookItem_CanBeMarked_AsReadingRoomOnly()
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
                },
                IsReadingRoomOnly = true
            };

            Assert.True(item.IsReadingRoomOnly);
        }

        [Fact]
        public void BookItem_Edition_Book_IsCorrect()
        {
            var book = new Book { Id = 1, Title = "Test" };

            var item = new BookItem
            {
                Edition = new Edition
                {
                    Book = book,
                    Publisher = "Pub",
                    Year = 2024,
                    EditionNumber = 1,
                    Pages = 100
                }
            };

            Assert.Equal(book, item.Edition.Book);
        }

        [Fact]
        public void BookItem_Id_Defaults_To_Zero()
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

            Assert.Equal(0, item.Id);
        }

        [Fact]
        public void Multiple_BookItems_Can_Share_Same_Edition()
        {
            var edition = new Edition
            {
                Book = new Book { Id = 1, Title = "Test" },
                Publisher = "Pub",
                Year = 2024,
                EditionNumber = 1,
                Pages = 100
            };

            var item1 = new BookItem { Edition = edition };
            var item2 = new BookItem { Edition = edition };

            Assert.Equal(item1.Edition, item2.Edition);
        }

        [Fact]
        public void BookItem_Edition_Is_Not_Null_After_Creation()
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
        public void BookItem_Edition_CanBeRead_AfterCreation()
        {
            var edition = new Edition
            {
                Book = new Book { Id = 1, Title = "Test" },
                Publisher = "Pub",
                Year = 2024,
                EditionNumber = 1,
                Pages = 100
            };

            var item = new BookItem { Edition = edition };

            Assert.Equal(edition, item.Edition);
        }

    }
}
