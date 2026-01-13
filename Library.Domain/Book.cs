// Copyright (c) Buzenchi Andreea

namespace Library.Domain
{
    /// <summary>
    /// Represents a book in the library.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Gets or sets the unique identifier of the book.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the book.
        /// </summary>
        required public string Title { get; set; }

        /// <summary>
        /// Gets or sets the domains of the book.
        /// </summary>
        public List<BookDomain> Domains { get; set; } = new ();
    }
}