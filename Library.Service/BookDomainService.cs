namespace Library.Service
{
    using System;
    using Library.Domain;
    using System.Collections.Generic;
    using System.Linq;
    using Library.Service.Interfaces;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    using Library.Service.Logging;
    using Library.Service.Configuration;
    using Library.Domain.Exceptions;

    public class BookDomainService : IBookDomainService
    {
        private readonly ILogger<BookDomainService> logger;
        private readonly LibraryRulesSettings rules;

        public BookDomainService(ILoggerFactoryProvider loggerProvider, LibraryRulesSettings rules)
        {
            logger = loggerProvider.CreateLogger<BookDomainService>();

            this.rules = rules ?? throw new ArgumentNullException(nameof(rules));
        }
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

        public void ValidateMaxDomainsPerBook(IEnumerable<BookDomain> domains)
        {
            logger.LogInformation(
            "Validating max domains per book. MaxAllowed={MaxAllowed}",
            rules.MaxDomainsPerBook);

            if (domains == null)
            {
                logger.LogWarning("Domains collection is null");
                throw new ArgumentNullException(nameof(domains));

            }

            if (rules.MaxDomainsPerBook <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rules.MaxDomainsPerBook), "Maximum allowed domains must be greater than zero");
            }

            var count = domains.Count();

            if (count > rules.MaxDomainsPerBook)
            {
                logger.LogWarning("Domain limit exceeded.Count={Count}, MaxAllowed={MaxAllowed}", domains.Count(), rules.MaxDomainsPerBook);
                throw new MaxDomainsPerBookExceededException(rules.MaxDomainsPerBook);

            }
        }
    }
}