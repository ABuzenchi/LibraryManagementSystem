using Library.Domain;
using Library.Service;

namespace Library.Tests
{
    public class BookDomainServiceTests
    {
        [Fact]
        public void ValidateMaxDomainsPerBook_ThrowsException_WhenLimitIsExceeded()
        {
            var domains = new List<BookDomain>
            {
                new BookDomain { Id = 1, Name = "D1" },
                new BookDomain { Id = 2, Name = "D2" },
                new BookDomain { Id = 3, Name = "D3" }
            };

            var service = new BookDomainService();

            Assert.Throws<InvalidOperationException>(() =>
                service.ValidateMaxDomainsPerBook(domains, maxAllowedDomains: 2));
        }

        [Fact]
        public void ValidateMaxDomainsPerBook_DoesNotThrow_WhenWithinLimit()
        {
            var domains = new List<BookDomain>
            {
                new BookDomain { Id = 1, Name = "D1" },
                new BookDomain { Id = 2, Name = "D2" }
            };

            var service = new BookDomainService();

            var exception = Record.Exception(() =>
                service.ValidateMaxDomainsPerBook(domains, maxAllowedDomains: 2));

            Assert.Null(exception);
        }
    }
}
