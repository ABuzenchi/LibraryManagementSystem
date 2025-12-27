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

    public class BookDomainService : IBookDomainService
    {
        private readonly ILogger<BookDomainService> logger;

        public BookDomainService()
        {
            logger = NullLogger<BookDomainService>.Instance;
        }

        public BookDomainService(ILoggerFactoryProvider loggerProvider)
        {
            logger = loggerProvider.CreateLogger<BookDomainService>();
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
                        throw new InvalidOperationException(
                            $"Domain conflict: '{domain.Name}' is a subdomain of '{currentParent.Name}'.");
                    }

                    currentParent = currentParent.Parent;
                }
            }
        }

        public void ValidateMaxDomainsPerBook(IEnumerable<BookDomain> domains, int maxAllowedDomains)
        {
            logger.LogInformation(
            "Validating max domains per book. MaxAllowed={MaxAllowed}",
            maxAllowedDomains);

            if (domains == null)
            {
                logger.LogWarning("Domains collection is null");
                throw new ArgumentNullException(nameof(domains));

            }

            if (maxAllowedDomains <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxAllowedDomains), "Maximum allowed domains must be greater than zero");
            }

            var count = domains.Count();

            if (count > maxAllowedDomains)
            {
                logger.LogWarning("Domain limit exceeded.Count={Count}, MaxAllowed={MaxAllowed}",domains.Count(),maxAllowedDomains);
                throw new InvalidOperationException($"A book cannot be assigned to more than {maxAllowedDomains} domains.");
            }
        }
    }
}