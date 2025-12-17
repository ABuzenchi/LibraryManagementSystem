namespace Library.Service
{
    using System;
    using Library.Domain;
    using System.Collections.Generic;
    using System.Linq;
    using Library.Service.Interfaces;

    public class BookDomainService : IBookDomainService
    {
        public void ValidateNoAncestorDomainConflict(IEnumerable<BookDomain> domains)
        {
            if(domains==null)
            {
                throw new ArgumentNullException(nameof(domains));

            }

            var domainList=domains.ToList();

            foreach(var domain in domainList)
            {
                var currentParent=domain.Parent;
                while(currentParent!=null)
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

        public void ValidateMaxDomainsPerBook( IEnumerable<BookDomain> domains, int maxAllowedDomains)
        {
            if(domains==null)
            {
                throw new ArgumentNullException(nameof(domains));

            }

            if(maxAllowedDomains<=0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxAllowedDomains),"Maximum allowed domains must be greater than zero");
            }

            var count=domains.Count();

            if(count>maxAllowedDomains)
            {
                throw new InvalidOperationException($"A book cannot be assigned to more than {maxAllowedDomains} domains.");
            }
        }
    }
}