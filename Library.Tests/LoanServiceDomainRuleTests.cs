using System;
using System.Collections.Generic;
using Library.Domain;
using Library.Service;
using Library.Tests.TestHelpers;
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

            var service = LoanServiceTestFactory.Create();

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

            var service = LoanServiceTestFactory.Create();

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

            var service = LoanServiceTestFactory.Create();

            var exception = Record.Exception(() =>
                service.ValidateDistinctDomainsForLoan(items));

            Assert.Null(exception);
        }

        [Fact]
        public void ValidateDistinctDomainsForLoan_Throws_When_Items_Is_Null()
        {
            var service = LoanServiceTestFactory.Create();

            Assert.Throws<ArgumentNullException>(() =>
                service.ValidateDistinctDomainsForLoan(null!));
        }

        [Fact]
        public void ValidateDistinctDomainsForLoan_Throws_When_No_Domains_Assigned()
        {
            var items = new List<BookItem>
    {
        CreateBookItemWithDomains(),
        CreateBookItemWithDomains(),
        CreateBookItemWithDomains()
    };

            var service = LoanServiceTestFactory.Create();

            Assert.Throws<InvalidOperationException>(() =>
                service.ValidateDistinctDomainsForLoan(items));
        }

        [Fact]
        public void ValidateDistinctDomainsForLoan_Throws_When_DomainIds_Are_Duplicate()
        {
            var d1 = new BookDomain { Id = 1, Name = "IT" };
            var d2 = new BookDomain { Id = 1, Name = "IT copy" };

            var items = new List<BookItem>
    {
        CreateBookItemWithDomains(d1),
        CreateBookItemWithDomains(d2),
        CreateBookItemWithDomains(d1)
    };

            var service = LoanServiceTestFactory.Create();

            Assert.Throws<InvalidOperationException>(() =>
                service.ValidateDistinctDomainsForLoan(items));
        }

        [Fact]
        public void ValidateDistinctDomainsForLoan_Throws_When_Four_Items_Same_Domain()
        {
            var domain = CreateDomain(1, "IT");

            var items = new List<BookItem>
    {
        CreateBookItemWithDomains(domain),
        CreateBookItemWithDomains(domain),
        CreateBookItemWithDomains(domain),
        CreateBookItemWithDomains(domain)
    };

            var service = LoanServiceTestFactory.Create();

            Assert.Throws<InvalidOperationException>(() =>
                service.ValidateDistinctDomainsForLoan(items));
        }

        [Fact]
        public void ValidateDistinctDomainsForLoan_DoesNotThrow_When_Four_Items_Two_Domains()
        {
            var d1 = CreateDomain(1, "IT");
            var d2 = CreateDomain(2, "Math");

            var items = new List<BookItem>
    {
        CreateBookItemWithDomains(d1),
        CreateBookItemWithDomains(d1),
        CreateBookItemWithDomains(d2),
        CreateBookItemWithDomains(d2)
    };

            var service = LoanServiceTestFactory.Create();

            var ex = Record.Exception(() =>
                service.ValidateDistinctDomainsForLoan(items));

            Assert.Null(ex);
        }
        [Fact]
        public void ValidateDistinctDomainsForLoan_DoesNotThrow_When_More_Than_Two_Domains()
        {
            var d1 = CreateDomain(1, "IT");
            var d2 = CreateDomain(2, "Math");
            var d3 = CreateDomain(3, "Physics");

            var items = new List<BookItem>
    {
        CreateBookItemWithDomains(d1),
        CreateBookItemWithDomains(d2),
        CreateBookItemWithDomains(d3),
        CreateBookItemWithDomains(d1),
        CreateBookItemWithDomains(d2)
    };

            var service = LoanServiceTestFactory.Create();

            var ex = Record.Exception(() =>
                service.ValidateDistinctDomainsForLoan(items));

            Assert.Null(ex);
        }
        [Fact]
        public void ValidateDistinctDomainsForLoan_Allows_Minimum_Valid_Combination()
        {
            var d1 = CreateDomain(1, "IT");
            var d2 = CreateDomain(2, "Math");

            var items = new List<BookItem>
    {
        CreateBookItemWithDomains(d1),
        CreateBookItemWithDomains(d1),
        CreateBookItemWithDomains(d2)
    };

            var service = LoanServiceTestFactory.Create();

            var ex = Record.Exception(() =>
                service.ValidateDistinctDomainsForLoan(items));

            Assert.Null(ex);
        }

    }
}
