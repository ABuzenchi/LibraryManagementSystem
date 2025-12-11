namespace Library.Domain
{
    /// <summary>
    /// Represents a copy of a book edition.
    /// </summary>
    public class BookItem
    {
        /// <summary>
        /// The unique identifier of the book item.
        /// </summary>
        public int Id{get;set;}
        
        /// <summary>
        /// The edition of this book item.
        /// One-to-many relationship
        /// One edition can gave multiple copies
        /// </summary>
        public required Edition Edition{get;set;}

        /// <summary>
        /// Indicates whether this book item is only for reading in the library.
        /// </summary>
        public bool IsReadingRoomOnly{get;set;}

    }
}