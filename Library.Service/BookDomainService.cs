// Copyright (c) Buzenchi Andreea

using Library.Domain;
using Library.Domain.Exceptions;
using Library.Service.Configuration;
using Library.Service.Interfaces;
using Library.Service.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Library.Service
{
    /// <summary>
    /// Provides domain-related validation logic for books.
    /// </summary>
    public class BookDomainService : IBookDomainService
    {
        private readonly ILogger<BookDomainService> logger;
        private readonly LibraryRulesSettings rules;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookDomainService"/> class.
        /// </summary>
        /// <param name="loggerProvider">
        /// The logger factory provider.
        /// </param>
        /// <param name="rules">
        /// The library business rules settings.
        /// </param>
        public BookDomainService(ILoggerFactoryProvider loggerProvider, LibraryRulesSettings rules)
        {
            logger = loggerProvider.CreateLogger<BookDomainService>();

            this.rules = rules ?? throw new ArgumentNullException(nameof(rules));
        }

        /// <summary>
        /// Validates that no domain in the given collection
        /// is an ancestor of another domain in the same collection.
        /// </summary>
        /// <param name="domains">
        /// The collection of domains assigned to a book.
        /// </param>
        public void ValidateNoAncestorDomainConflict(IEnumerable<BookDomain> domains)
        {
            if (domains == null)
            {
                throw new ArgumentNullException(nameof(domains));

            }

            var domainList = domains.ToList();

            foreach (var domain in domainList)
            {
                var currentParent = domain.Parent;
                while (currentParent != null)
                {
                    if (domainList.Any(d => d.Id == currentParent.Id))
                    {
                        throw new DomainConflictException(domain.Name, currentParent.Name);

                    }

                    currentParent = currentParent.Parent;
                }
            }
        }

        public void AssignDomainsToBook(Book book, IEnumerable<BookDomain> domains)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            this.logger.LogInformation(
                "Assigning domains to book {BookId}",
                book.Id);

            book.SetDomains(domains, this.rules.MaxDomainsPerBook);
            this.logger.LogInformation(
            "Successfully assigned {Count} domains to book {BookId}",
            domains.Count(),
            book.Id);
        }

        /// <summary>
        /// Validates that the number of domains assigned to a book
        /// does not exceed the configured maximum.
        /// </summary>
        /// <param name="domains">
        /// The collection of domains assigned to a book.
        /// </param>
        public void ValidateMaxDomainsPerBook(IEnumerable<BookDomain> domains)
        {
            this.logger.LogInformation(
            "Validating max domains per book. MaxAllowed={MaxAllowed}",
            this.rules.MaxDomainsPerBook);

            if (domains == null)
            {
                this.logger.LogWarning("Domains collection is null");
                throw new ArgumentNullException(nameof(domains));

            }

            if (this.rules.MaxDomainsPerBook <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(this.rules.MaxDomainsPerBook), "Maximum allowed domains must be greater than zero");
            }

            var count = domains.Count();

            if (count > this.rules.MaxDomainsPerBook)
            {
                this.logger.LogWarning("Domain limit exceeded.Count={Count}, MaxAllowed={MaxAllowed}", domains.Count(), this.rules.MaxDomainsPerBook);
                throw new MaxDomainsPerBookExceededException(this.rules.MaxDomainsPerBook);
            }
        }
    }
}