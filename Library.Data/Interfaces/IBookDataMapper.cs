// Copyright (c) Buzenchi Andreea

using Library.Domain;

namespace Library.Data.Interfaces
{
    /// <summary>
    /// Defines data access operations for <see cref="Book"/> entities.
    /// Implements the Data Mapper pattern for books.
    /// </summary>
    public interface IBookDataMapper
    {
        /// <summary>
        /// Retrieves a book by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the book.</param>
        /// <returns>
        /// A <see cref="Book"/> instance corresponding to the given identifier.
        /// </returns>
        Book GetBookById(int id);

        /// <summary>
        /// Retrieves all books from the data source.
        /// </summary>
        /// <returns>
        /// A list of all <see cref="Book"/> entities.
        /// </returns>
        List<Book> GetAllBooks();

        /// <summary>
        /// Inserts a new book into the data source.
        /// </summary>
        /// <param name="book">The book entity to be inserted.</param>
        void InsertNewBook(Book book);

        /// <summary>
        /// Updates an existing book in the data source.
        /// </summary>
        /// <param name="book">The book entity containing updated information.</param>
        void UpdateBook(Book book);

        /// <summary>
        /// Deletes a book from the data source using its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the book to delete.</param>
        void DeleteBook(int id);
    }
}
