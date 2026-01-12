using Library.Domain;
using Xunit;

namespace Library.Tests.Domain
{
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
        public void Book_Domains_CanAddDomain()
        {
            var book = new Book { Id = 1, Title = "Test" };
            book.Domains.Add(new BookDomain { Id = 1, Name = "IT" });
            Assert.Single(book.Domains);
        }

        [Fact]
        public void Book_CanHaveMultipleDomains()
        {
            var book = new Book { Id = 1, Title = "Test" };
            book.Domains.Add(new BookDomain { Id = 1, Name = "IT" });
            book.Domains.Add(new BookDomain { Id = 2, Name = "Math" });
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
        public void Book_Domains_CanBeCleared()
        {
            var book = new Book { Id = 1, Title = "Test" };
            book.Domains.Add(new BookDomain { Id = 1, Name = "IT" });
            book.Domains.Clear();
            Assert.Empty(book.Domains);
        }

        [Fact]
        public void Book_Domains_Allows_DuplicateDomainReferences()
        {
            var book = new Book { Id = 1, Title = "Test" };
            var domain = new BookDomain { Id = 1, Name = "IT" };

            book.Domains.Add(domain);
            book.Domains.Add(domain);

            Assert.Equal(2, book.Domains.Count);
        }

    }
}
