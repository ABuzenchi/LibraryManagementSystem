namespace Library.Service.Interfaces
{
    using Library.Domain;
    using System.Collections.Generic;

    public interface IBookDomainService
    {
        void ValidateNoAncestorDomainConflict(IEnumerable<BookDomain> domains);
        void ValidateMaxDomainsPerBook(IEnumerable<BookDomain>domains, int maxAllowedDomains);
    }
}