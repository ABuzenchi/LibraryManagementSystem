// Copyright (c) Buzenchi Andreea

using System.Collections.Generic;
using Library.Domain;

namespace Library.Service.Interfaces
{
     /// <summary>
    /// Defines operations related to validating book domain assignments.
    /// </summary>
    public interface IBookDomainService
    {
        /// <summary>
        /// Validates that no domain in the given collection is an ancestor
        /// of another domain in the same collection.
        /// </summary>
        /// <param name="domains">
        /// The collection of domains assigned to a book.
        /// </param>
        void ValidateNoAncestorDomainConflict(IEnumerable<BookDomain> domains);

        /// <summary>
        /// Validates that the number of domains assigned to a book
        /// does not exceed the configured maximum.
        /// </summary>
        /// <param name="domains">
        /// The collection of domains assigned to a book.
        /// </param>
        void ValidateMaxDomainsPerBook(IEnumerable<BookDomain> domains);
    }
}