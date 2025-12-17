using System;
using System.Collections.Generic;
using Library.Domain;
using Library.Service;
using Xunit;

namespace Library.Tests
{
    public class LoanServiceDomainRuleTests
    {
        private static BookDomain CreateDomain(int id, string name)
            => new BookDomain { Id = id, Name = name };

        private static BookItem CreateBookItemWithDomains(params BookDomain[] domains)
        {
            return new BookItem
            {
                Edition = new Edition
                {
                    Book = new Book
                    {
                        Title = "Test Book",
                        Domains = new List<BookDomain>(domains)
                    },
                    Publisher = "Test Publisher",
                    Year = 2024,
                    EditionNumber = 1,
                    Pages = 100
                }
            };
        }

        [Fact]
        public void ValidateDistinctDomainsForLoan_ThrowsException_WhenThreeItemsFromSameDomain()
        {
            var domain = CreateDomain(1, "Informatics");

            var items = new List<BookItem>
            {
                CreateBookItemWithDomains(domain),
                CreateBookItemWithDomains(domain),
                CreateBookItemWithDomains(domain)
            };

            var service = new LoanService();

            Assert.Throws<InvalidOperationException>(() =>
                service.ValidateDistinctDomainsForLoan(items));
        }

        [Fact]
        public void ValidateDistinctDomainsForLoan_DoesNotThrow_WhenTwoDomainsPresent()
        {
            var d1 = CreateDomain(1, "Informatics");
            var d2 = CreateDomain(2, "Mathematics");

            var items = new List<BookItem>
            {
                CreateBookItemWithDomains(d1),
                CreateBookItemWithDomains(d1),
                CreateBookItemWithDomains(d2)
            };

            var service = new LoanService();

            var exception = Record.Exception(() =>
                service.ValidateDistinctDomainsForLoan(items));

            Assert.Null(exception);
        }

        [Fact]
        public void ValidateDistinctDomainsForLoan_DoesNotThrow_WhenLessThanThreeItems()
        {
            var domain = CreateDomain(1, "Informatics");

            var items = new List<BookItem>
            {
                CreateBookItemWithDomains(domain),
                CreateBookItemWithDomains(domain)
            };

            var service = new LoanService();

            var exception = Record.Exception(() =>
                service.ValidateDistinctDomainsForLoan(items));

            Assert.Null(exception);
        }
    }
}
