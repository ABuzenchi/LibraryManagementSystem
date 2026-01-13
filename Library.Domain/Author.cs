// Copyright (c) Buzenchi Andreea

namespace Library.Domain
{
    /// <summary>
    /// Represents an author of books.
    /// </summary>
    public class Author
    {
        /// <summary>
        /// Gets or sets the unique identifier of the author.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets full name of the author.
        /// </summary>
        required public string Name { get; set; }

        /// <summary>
        /// Gets or sets list of books written by this author.
        /// Many-to-many relationship with Book.
        /// </summary>
        public List<Book> Books { get; set; } = new ();
    }
}