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

        [Fact]
        public void ValidateMaxDomainsPerBook_Throws_When_Domains_Is_Null()
        {
            var service = new BookDomainService();

            Assert.Throws<ArgumentNullException>(() =>
                service.ValidateMaxDomainsPerBook(null!, 2));
        }

        [Fact]
        public void ValidateMaxDomainsPerBook_Throws_When_Limit_Is_Zero()
        {
            var service = new BookDomainService();

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                service.ValidateMaxDomainsPerBook(
                    new List<BookDomain>(),
                    0));
        }

        [Fact]
        public void ValidateMaxDomainsPerBook_Throws_When_Limit_Is_Negative()
        {
            var service = new BookDomainService();

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                service.ValidateMaxDomainsPerBook(
                    new List<BookDomain>(),
                    -1));
        }

        [Fact]
        public void ValidateMaxDomainsPerBook_DoesNotThrow_When_List_Is_Empty()
        {
            var service = new BookDomainService();

            var ex = Record.Exception(() =>
                service.ValidateMaxDomainsPerBook(
                    new List<BookDomain>(),
                    3));

            Assert.Null(ex);
        }

        [Fact]
        public void ValidateMaxDomainsPerBook_DoesNotThrow_When_Exactly_At_Limit()
        {
            var domains = new List<BookDomain>
    {
        new BookDomain { Id = 1, Name = "D1" },
        new BookDomain { Id = 2, Name = "D2" },
        new BookDomain { Id = 3, Name = "D3" }
    };

            var service = new BookDomainService();

            var ex = Record.Exception(() =>
                service.ValidateMaxDomainsPerBook(domains, 3));

            Assert.Null(ex);
        }

        [Fact]
        public void ValidateMaxDomainsPerBook_Counts_Distinct_Instances()
        {
            var domains = new List<BookDomain>
    {
        new BookDomain { Id = 1, Name = "D1" },
        new BookDomain { Id = 1, Name = "D1 duplicate" }
    };

            var service = new BookDomainService();

            Assert.Throws<InvalidOperationException>(() =>
                service.ValidateMaxDomainsPerBook(domains, 1));
        }

        [Fact]
        public void ValidateMaxDomainsPerBook_Allows_Single_Domain()
        {
            var domains = new List<BookDomain>
    {
        new BookDomain { Id = 1, Name = "D1" }
    };

            var service = new BookDomainService();

            var ex = Record.Exception(() =>
                service.ValidateMaxDomainsPerBook(domains, 1));

            Assert.Null(ex);
        }

        [Fact]
        public void ValidateMaxDomainsPerBook_Handles_Large_Limit()
        {
            var domains = new List<BookDomain>
    {
        new BookDomain { Id = 1, Name = "D1" },
        new BookDomain { Id = 2, Name = "D2" }
    };

            var service = new BookDomainService();

            var ex = Record.Exception(() =>
                service.ValidateMaxDomainsPerBook(domains, 100));

            Assert.Null(ex);
        }

    }
}
