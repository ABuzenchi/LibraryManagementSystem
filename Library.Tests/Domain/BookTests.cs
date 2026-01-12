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
        public void AddDomain_AddsDomain_WhenValid()
        {
            var book = new Book { Title = "Test" };
            var domain = new BookDomain { Id = 1, Name = "IT" };

            book.AddDomain(domain, maxDomainsPerBook: 3);

            Assert.Single(book.Domains);
            Assert.Contains(domain, book.Domains);
        }


        [Fact]
        public void AddDomain_Throws_WhenMaxDomainsExceeded()
        {
            var book = new Book { Title = "Test" };

            book.AddDomain(new BookDomain { Id = 1, Name = "IT" }, 1);

            Assert.Throws<InvalidOperationException>(() =>
                book.AddDomain(new BookDomain { Id = 2, Name = "Math" }, 1));
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
        public void AddDomain_Throws_WhenAncestorDomainIsAdded()
        {
            var root = new BookDomain { Id = 1, Name = "Science" };
            var child = new BookDomain { Id = 2, Name = "IT", Parent = root };

            var book = new Book { Title = "Test" };

            book.AddDomain(child, 3);

            Assert.Throws<InvalidOperationException>(() =>
                book.AddDomain(root, 3));
        }


        [Fact]
        public void GetAllDomains_ReturnsInheritedDomains()
        {
            var root = new BookDomain { Id = 1, Name = "Science" };
            var child = new BookDomain { Id = 2, Name = "IT", Parent = root };

            var book = new Book { Title = "Test" };
            book.AddDomain(child, 3);

            var allDomains = book.GetAllDomains();

            Assert.Equal(2, allDomains.Count);
            Assert.Contains(root, allDomains);
            Assert.Contains(child, allDomains);
        }


    }
}
