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
        public int Id{get;set;}
        
        /// <summary>
        /// The title of the book
        /// </summary>
        public required string Title{get;set;}

        public List<BookDomain>Domains{get;set;}=new();
    }
}