// Copyright (c) Buzenchi Andreea

using Dapper;
using Library.Data.Interfaces;
using Library.Domain;
using Npgsql;

namespace Library.Data.Mappers
{
    /// <summary>
    /// Provides data access operations for <see cref="Book"/> entities.
    /// Implements the Data Mapper pattern using a PostgreSQL database.
    /// </summary>
    public class BookDataMapper : IBookDataMapper
    {
        private readonly NpgsqlConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookDataMapper"/> class.
        /// </summary>
        /// <param name="connection">
        /// The PostgreSQL database connection used to execute queries.
        /// </param>
        public BookDataMapper(NpgsqlConnection connection)
        {
            this.connection = connection;
        }

        /// <summary>
        /// Retrieves a single book by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the book.</param>
        /// <returns>
        /// A <see cref="Book"/> instance corresponding to the specified identifier.
        /// </returns>
        /// <exception cref="KeyNotFoundException">
        /// Thrown when no book with the specified identifier is found.
        /// </exception>
        public Book GetBookById(int id)
        {
            var sql = "SELECT * FROM book WHERE id=@Id";
            var book = this.connection.QuerySingleOrDefault<Book>(sql, new { Id = id }) ?? throw new KeyNotFoundException($"Book with id {id} not found.");
            return book;
        }

        /// <summary>
        /// Get all books.
        /// </summary>
        /// <returns>A list of books objects.</returns>
        public List<Book> GetAllBooks()
        {
            var sql = "SELECT * FROM book";
            return this.connection.Query<Book>(sql).AsList();
        }

        /// <summary>
        /// Inserts a new book into the database.
        /// </summary>
        /// <param name="book">The book to add.</param>
        public void InsertNewBook(Book book)
        {
            var sql = "INSERT INTO book (title) VALUES (@Title) RETURNING id";
            book.Id = this.connection.ExecuteScalar<int>(sql, book);
        }

        /// <summary>
        /// Update an existing book.
        /// </summary>
        /// <param name="book">The book to update.</param>
        public void UpdateBook(Book book)
        {
            var sql = "UPDATE book SET title = @Title WHERE id = @Id";
            var affectedRows = this.connection.Execute(sql, book);
            if (affectedRows == 0)
            {
                throw new KeyNotFoundException($"Book with id {book.Id} not found for update.");
            }
        }

        /// <summary>
        /// Delete a book by its ID.
        /// </summary>
        /// <param name="id">The Id of the book to delete.</param>
        public void DeleteBook(int id)
        {
            var sql = "DELETE FROM book WHERE id = @Id";
            var affectedRows = this.connection.Execute(sql, new { Id = id });
            if (affectedRows == 0)
            {
                throw new KeyNotFoundException($"Book with id {id} not found for deletion.");
            }
        }
    }
}