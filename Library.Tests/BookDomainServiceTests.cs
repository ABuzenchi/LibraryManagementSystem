using Library.Domain;
using Library.Service;
using Library.Service.Configuration;

namespace Library.Tests
{

    public class BookDomainServiceTests
    {
        private static BookDomainService CreateService(int maxDomainsPerBook)
        {
            var rules = new LibraryRulesSettings
            {
                MaxDomainsPerBook = maxDomainsPerBook
            };

            var loggerProvider = new TestLoggerFactoryProvider();

            return new BookDomainService(loggerProvider, rules);
        }

        [Fact]
        public void ValidateMaxDomainsPerBook_ThrowsException_WhenLimitIsExceeded()
        {
            var domains = new List<BookDomain>
            {
                new BookDomain { Id = 1, Name = "D1" },
                new BookDomain { Id = 2, Name = "D2" },
                new BookDomain { Id = 3, Name = "D3" }
            };

            var service = CreateService(maxDomainsPerBook: 2);

            Assert.Throws<InvalidOperationException>(() =>
                service.ValidateMaxDomainsPerBook(domains));
        }

        [Fact]
        public void ValidateMaxDomainsPerBook_DoesNotThrow_WhenWithinLimit()
        {
            var domains = new List<BookDomain>
            {
                new BookDomain { Id = 1, Name = "D1" },
                new BookDomain { Id = 2, Name = "D2" }
            };

            var service = CreateService(maxDomainsPerBook: 2);

            var exception = Record.Exception(() =>
                service.ValidateMaxDomainsPerBook(domains));

            Assert.Null(exception);
        }

        [Fact]
        public void ValidateMaxDomainsPerBook_Throws_When_Domains_Is_Null()
        {
            var service = CreateService(maxDomainsPerBook: 2);

            Assert.Throws<ArgumentNullException>(() =>
                service.ValidateMaxDomainsPerBook(null!));
        }


        [Fact]
        public void ValidateMaxDomainsPerBook_Throws_When_Limit_Is_Zero()
        {
            var service = CreateService(maxDomainsPerBook: 0);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                service.ValidateMaxDomainsPerBook(
                    new List<BookDomain>()));
        }

        [Fact]
        public void ValidateMaxDomainsPerBook_Throws_When_Limit_Is_Negative()
        {
            var service = CreateService(maxDomainsPerBook: -1);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                service.ValidateMaxDomainsPerBook(
                    new List<BookDomain>()));
        }

        [Fact]
        public void ValidateMaxDomainsPerBook_DoesNotThrow_When_List_Is_Empty()
        {
            var service = CreateService(maxDomainsPerBook: 3); ;

            var ex = Record.Exception(() =>
                service.ValidateMaxDomainsPerBook(
                    new List<BookDomain>()));

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

            var service = CreateService(maxDomainsPerBook: 3); ;

            var ex = Record.Exception(() =>
                service.ValidateMaxDomainsPerBook(domains));

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

            var service = CreateService(maxDomainsPerBook: 1);

            Assert.Throws<InvalidOperationException>(() =>
                service.ValidateMaxDomainsPerBook(domains));
        }

        [Fact]
        public void ValidateMaxDomainsPerBook_Allows_Single_Domain()
        {
            var domains = new List<BookDomain>
    {
        new BookDomain { Id = 1, Name = "D1" }
    };

            var service = CreateService(maxDomainsPerBook: 1);

            var ex = Record.Exception(() =>
                service.ValidateMaxDomainsPerBook(domains));

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

            var service = CreateService(maxDomainsPerBook: 100);

            var ex = Record.Exception(() =>
                service.ValidateMaxDomainsPerBook(domains));

            Assert.Null(ex);
        }

    }
}
