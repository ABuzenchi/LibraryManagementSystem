namespace Library.Tests.Domain
{
    using Library.Domain;
    using Library.Domain.Exceptions;
    using Xunit;
    public class BookTests
    {
        [Fact]
        public void Book_CanBeCreated_WithValidTitle()
        {
            var book = new Book { Id = 1, Title = "Clean Code" };
            Assert.Equal("Clean Code", book.Title);
        }

        [Fact]
        public void Book_Domains_IsInitialized()
        {
            var book = new Book { Id = 1, Title = "Test" };
            Assert.NotNull(book.Domains);
        }

        [Fact]
        public void Book_SetDomains_AssignsSingleDomain()
        {
            var book = new Book { Id = 1, Title = "Test" };

            book.SetDomains(
                new[]
                {
            new BookDomain { Id = 1, Name = "IT" }
                },
                maxDomains: 5
            );

            Assert.Single(book.Domains);
        }


        [Fact]
        public void Book_SetDomains_AssignsMultipleDomains()
        {
            var book = new Book { Id = 1, Title = "Test" };

            book.SetDomains(
                new[]
                {
            new BookDomain { Id = 1, Name = "IT" },
            new BookDomain { Id = 2, Name = "Math" }
                },
                maxDomains: 5
            );

            Assert.Equal(2, book.Domains.Count);
        }


        [Fact]
        public void Book_Id_Defaults_To_Zero()
        {
            var book = new Book { Title = "Test" };
            Assert.Equal(0, book.Id);
        }

        [Fact]
        public void Book_Title_CanBeChanged_AfterCreation()
        {
            var book = new Book { Id = 1, Title = "Old" };
            book.Title = "New";
            Assert.Equal("New", book.Title);
        }

        [Fact]
        public void Book_SetDomains_CanClearDomains()
        {
            var book = new Book { Id = 1, Title = "Test" };

            book.SetDomains(
                new[]
                {
            new BookDomain { Id = 1, Name = "IT" }
                },
                maxDomains: 5
            );

            book.SetDomains(Array.Empty<BookDomain>(), maxDomains: 5);

            Assert.Empty(book.Domains);
        }

    }
}
