namespace Library.Domain
{
    /// <summary>
    /// Represents a domain of books
    /// Domains can have subdomains
    /// </summary>
    public class Domain
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
        /// Parent domain if this is a subdomain
        /// null if this is a main domain
        /// </summary>
        public Domain? Parent{get;set;}

        /// <summary>
        /// List of subdomains under this domain
        /// </summary>
        public List<Domain>Subdomains{get;set;}=new();

        /// <summary>
        /// list of the books associated with this domain.
        /// Many-to-many relationship with Book.
        /// </summary>
        public List<Book>Books{get;set;}=new();
    }
}