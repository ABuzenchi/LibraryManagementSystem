using Library.Domain;

namespace Library.Data.Interfaces
{
    public interface IBookDataMapper
    {
        Book GetBookById(int id);
        List<Book> GetAllBooks();
        void InsertNewBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(int id);
    }
}
