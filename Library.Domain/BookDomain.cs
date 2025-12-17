namespace Library.Domain
{
    /// <summary>
    /// Represents a domain of books
    /// Domains can have subdomains
    /// </summary>
    public class BookDomain
    {
        /// <summary>
        /// The unique identifier of the domain.
        /// </summary>
        public int Id{get;set;}

        /// <summary>
        /// The name of the domain.
        /// </summary>
        public required string Name{get;set;}

        /// <summary>
        /// Null if this is a root domain
        /// </summary>
        public int? ParentId{get;set;}

        /// <summary>
        /// Parent domain
        /// </summary>
        public BookDomain? Parent{get;set;}

        /// <summary>
        /// List of subdomains under this domain
        /// </summary>
        public List<BookDomain>Subdomains{get;set;}=new();

        /// <summary>
        /// list of the books associated with this domain.
        /// Many-to-many relationship with Book.
        /// </summary>
        public List<Book>Books{get;set;}=new();
    }
}