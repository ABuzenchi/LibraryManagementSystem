namespace Library.Domain
{
    /// <summary>
    /// Represents an author of books
    /// </summary>
    public class Author
    {
        /// <summary>
        /// The unique identifier of the author
        /// </summary>
        public int Id{get;set;}

        /// <summary>
        /// Full name of the author
        /// </summary>
        public required string Name{get;set;}
        /// <summary>
        /// List of books written by this author
        /// Many-to-many relationship with Book.
        /// </summary>
        public List<Book>Books{get;set;}=new();
    }
}