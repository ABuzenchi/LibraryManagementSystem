// Copyright (c) Buzenchi Andreea

using Library.Domain.Exceptions;

namespace Library.Domain
{
    /// <summary>
    /// Represents a book in the library.
    /// </summary>
    public class Book
    {
        private readonly List<BookDomain> domains = new();

        /// <summary>
        /// Gets or sets the unique identifier of the book.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the book.
        /// </summary>
        required public string Title { get; set; }

        /// <summary>
        /// Gets the domains assigned to the book.
        /// </summary>
        public IReadOnlyCollection<BookDomain> Domains => this.domains.AsReadOnly();

        /// <summary>
        /// Sets the domains for the book, validating business rules.
        /// </summary>
        /// <param name="domains">
        /// The domains to assign to the book.
        /// </param>
        /// <param name="maxDomains">
        /// The maximum number of allowed domains.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when domains is null.
        /// </exception>
        /// <exception cref="MaxDomainsPerBookExceededException">
        /// Thrown when the number of domains exceeds the maximum allowed.
        /// </exception>
        /// <exception cref="DomainConflictException">
        /// Thrown when a parent–child domain conflict is detected.
        /// </exception>
        public void SetDomains(IEnumerable<BookDomain> domains, int maxDomains)
        {
            if (domains == null)
            {
                throw new ArgumentNullException(nameof(domains));
            }

            if (maxDomains <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxDomains));
            }

            var domainList = domains.ToList();

            if (domainList.Count > maxDomains)
            {
                throw new MaxDomainsPerBookExceededException(maxDomains);
            }

            foreach (var domain in domainList)
            {
                var parent = domain.Parent;
                while (parent != null)
                {
                    if (domainList.Any(d => d.Id == parent.Id))
                    {
                        throw new DomainConflictException(domain.Name, parent.Name);
                    }

                    parent = parent.Parent;
                }
            }

            this.domains.Clear();
            this.domains.AddRange(domainList);
        }
    }
}
