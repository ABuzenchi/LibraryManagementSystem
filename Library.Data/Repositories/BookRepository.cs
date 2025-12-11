using Library.Domain;
using Npgsql;
using Dapper;

namespace Library.Data.Repositories
{
    public class BookRepository
    {
        private readonly NpgsqlConnection _connection;

        public BookRepository(NpgsqlConnection connection)
        {
            _connection=connection;
        }

        /// <summary>
        /// Gets a single book by its Id
        /// </summary>
        /// <param name="id">The id of the book</param>
        /// <returns>Return a book oject or throws KeyNotFoundException if book was not found</returns>
        public Book? GetBookById(int id)
        {
            var sql="SELECT * FROM book WHERE id=@Id";
            var book= _connection.QuerySingleOrDefault<Book>(sql, new { Id = id }) ?? throw new KeyNotFoundException($"Book with id {id} not found.");
            return book;
        }

        /// <summary>
        /// Get all books
        /// </summary>
        /// <returns>A list of books objects</returns>
        public List<Book>GetAllBooks()
        {
            var sql="SELECT * FROM book";
            return _connection.Query<Book>(sql).AsList();
        }

        /// <summary>
        /// Inserts a new book into the database
        /// </summary>
        /// <param name="book">The book to add</param>
        public void InsertNewBook(Book book)
        {
            var sql="INSERT INTO book (title) VALUES (@Title) RETURNING id";
            book.Id=_connection.ExecuteScalar<int>(sql,book);
        }

        /// <summary>
        /// Update an existing book
        /// </summary>
        /// <param name="book">The book to update</param>
        public void UpdateBook(Book book)
        {
            var sql = "UPDATE book SET title = @Title WHERE id = @Id";
            var affectedRows = _connection.Execute(sql, book);
            if (affectedRows == 0)
                throw new KeyNotFoundException($"Book with id {book.Id} not found for update.");
        }

        /// <summary>
        /// Delete a book by its ID
        /// </summary>
        /// <param name="id">The Id of the book to delete</param>
        public void DeleteBook(int id)
        {
            var sql = "DELETE FROM book WHERE id = @Id";
            var affectedRows = _connection.Execute(sql, new { Id = id });
            if (affectedRows == 0)
                throw new KeyNotFoundException($"Book with id {id} not found for deletion.");
        }
    }
}