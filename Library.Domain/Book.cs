namespace Library.Domain
{
    /// <summary>
    /// Represents a book in the library
    /// </summary>
    public class Book
    {
        /// <summary>
        /// The unique identifier of the book
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The title of the book
        /// </summary>
        public required string Title { get; set; }

        private readonly List<BookDomain> _domains = new();
        public IReadOnlyCollection<BookDomain> Domains => _domains.AsReadOnly();
        public void AddDomain(
    BookDomain domain,
    int maxDomainsPerBook)
        {
            if (domain == null)
                throw new ArgumentNullException(nameof(domain));

            if (_domains.Count >= maxDomainsPerBook)
                throw new InvalidOperationException(
                    $"A book cannot have more than {maxDomainsPerBook} domains.");

            // strămoș / descendent interzis
            foreach (var existing in _domains)
            {
                if (existing.IsAncestorOf(domain) || domain.IsAncestorOf(existing))
                {
                    throw new InvalidOperationException(
                        "Cannot assign both a domain and its ancestor/descendant to the same book.");
                }
            }

            if (!_domains.Contains(domain))
                _domains.Add(domain);
        }
        /// <summary>
        /// Returns all domains of the book, including inherited parent domains.
        /// </summary>
        public IReadOnlyCollection<BookDomain> GetAllDomains()
        {
            return _domains
                .SelectMany(d => new[] { d }.Concat(d.GetAncestors()))
                .DistinctBy(d => d.Id)
                .ToList()
                .AsReadOnly();
        }

    }
}