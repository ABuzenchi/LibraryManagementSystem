using Library.Domain;
using Xunit;

namespace Library.Tests.Domain
{
    public class BookDomainTests
    {
        [Fact]
        public void BookDomain_CanBeCreated_WithName()
        {
            var domain = new BookDomain { Id = 1, Name = "IT" };
            Assert.Equal("IT", domain.Name);
        }

        [Fact]
        public void BookDomain_Subdomains_IsInitialized()
        {
            var domain = new BookDomain { Id = 1, Name = "IT" };
            Assert.NotNull(domain.Subdomains);
        }

        [Fact]
        public void BookDomain_Books_IsInitialized()
        {
            var domain = new BookDomain { Id = 1, Name = "IT" };
            Assert.NotNull(domain.Books);
        }

        [Fact]
        public void BookDomain_CanHave_ParentDomain()
        {
            var parent = new BookDomain { Id = 1, Name = "Science" };
            var child = new BookDomain { Id = 2, Name = "IT", Parent = parent };

            Assert.Equal(parent, child.Parent);
        }

        [Fact]
        public void BookDomain_CanAdd_Subdomain()
        {
            var parent = new BookDomain { Id = 1, Name = "Science" };
            var child = new BookDomain { Id = 2, Name = "IT" };

            parent.Subdomains.Add(child);

            Assert.Single(parent.Subdomains);
        }

        [Fact]
        public void BookDomain_CanAdd_Multiple_Subdomains()
        {
            var parent = new BookDomain { Id = 1, Name = "Science" };

            parent.Subdomains.Add(new BookDomain { Id = 2, Name = "IT" });
            parent.Subdomains.Add(new BookDomain { Id = 3, Name = "Math" });

            Assert.Equal(2, parent.Subdomains.Count);
        }

        [Fact]
        public void BookDomain_CanAdd_Book()
        {
            var domain = new BookDomain { Id = 1, Name = "IT" };
            var book = new Book { Id = 1, Title = "Clean Code" };

            domain.Books.Add(book);

            Assert.Single(domain.Books);
        }

        [Fact]
        public void BookDomain_CanAdd_Multiple_Books()
        {
            var domain = new BookDomain { Id = 1, Name = "IT" };

            domain.Books.Add(new Book { Id = 1, Title = "Clean Code" });
            domain.Books.Add(new Book { Id = 2, Title = "Algorithms" });

            Assert.Equal(2, domain.Books.Count);
        }

        [Fact]
        public void BookDomain_Id_Defaults_To_Zero()
        {
            var domain = new BookDomain { Name = "IT" };
            Assert.Equal(0, domain.Id);
        }

        [Fact]
        public void BookDomain_Parent_CanBe_Null()
        {
            var domain = new BookDomain { Id = 1, Name = "IT" };
            Assert.Null(domain.Parent);
        }
    }
}
