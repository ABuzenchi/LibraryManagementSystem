namespace Library.Domain
{
    /// <summary>
    /// Represents a specific edition of a book
    /// </summary>
    public class Edition
    {
        /// <summary>
        /// The unique identifier of the edition.
        /// </summary>
        public int Id{get;set;}

        /// <summary>
        /// The book this edition belongs to.
        /// one-to-mane relationship
        /// One book can have multiple editions;
        /// </summary>
        public required Book Book{get;set;}

        /// <summary>
        /// The publisher of this edition
        /// </summary>
        public required string Publisher{get;set;}

        /// <summary>
        /// The year of the edition.
        /// </summary>
        public int Year{get;set;}

        /// <summary>
        /// The number of the edition
        /// </summary>
        public int EditionNumber{get;set;}

        /// <summary>
        /// The number of pages of this edition.
        /// </summary>
        public int Pages{get;set;}

        /// <summary>
        /// Type of edition
        /// </summary>
        public string? Type{get;set;}

        public List<BookItem>BookItems{get;set;}=new();

    }
}