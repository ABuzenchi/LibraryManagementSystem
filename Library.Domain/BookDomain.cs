// Copyright (c) Buzenchi Andreea

namespace Library.Domain
{
    /// <summary>
    /// Represents a domain of books
    /// Domains can have subdomains.
    /// </summary>
    public class BookDomain
    {
        /// <summary>
        /// Gets or sets the unique identifier of the domain.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the domain.
        /// </summary>
        required public string Name { get; set; }

        /// <summary>
        /// Gets or sets null if this is a root domain.
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Gets or sets parent domain.
        /// </summary>
        public BookDomain? Parent { get; set; }

        /// <summary>
        /// Gets or sets list of subdomains under this domain.
        /// </summary>
        public List<BookDomain> Subdomains { get; set; } = new ();

        /// <summary>
        /// Gets or sets list of the books associated with this domain.
        /// Many-to-many relationship with Book.
        /// </summary>
        public List<Book> Books { get; set; } = new ();
    }
}