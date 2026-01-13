// Copyright (c) Buzenchi Andreea

namespace Library.Domain
{
    /// <summary>
    /// Represents a specific edition of a book.
    /// </summary>
    public class Edition
    {
        /// <summary>
        /// The unique identifier of the edition.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the book this edition belongs to.
        /// one-to-mane relationship
        /// One book can have multiple editions.
        /// </summary>
        required public Book Book { get; set; }

        /// <summary>
        /// Gets or sets the publisher of this edition.
        /// </summary>
        required public string Publisher { get; set; }

        /// <summary>
        /// Gets or sets the year of the edition.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the number of the edition.
        /// </summary>
        public int EditionNumber { get; set; }

        /// <summary>
        /// Gets or sets the number of pages of this edition.
        /// </summary>
        public int Pages { get; set; }

        /// <summary>
        /// Gets or sets type of edition.
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// Gets or sets bookitems.
        /// </summary>
        public List<BookItem> BookItems { get; set; } = new ();
    }
}