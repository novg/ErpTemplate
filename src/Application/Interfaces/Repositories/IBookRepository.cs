using Domain.Models;

namespace Application.Interfaces.Repositories;

public interface IBookRepository
{
    Task<Book> GetBookById(int bookId);
    Task<IEnumerable<Book>> GetBooks();
}