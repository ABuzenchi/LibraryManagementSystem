// Copyright (c) Buzenchi Andreea

namespace Library.Domain
{
    /// <summary>
    /// Represents a copy of a book edition.
    /// </summary>
    public class BookItem
    {
        /// <summary>
        /// Gets or sets the unique identifier of the book item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the edition of this book item.
        /// One-to-many relationship
        /// One edition can gave multiple copies
        /// </summary>
        required public Edition Edition { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether indicates whether this book item is only for reading in the library.
        /// </summary>
        public bool IsReadingRoomOnly { get; set; }
    }
}