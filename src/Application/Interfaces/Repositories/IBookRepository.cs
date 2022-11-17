using Domain.Models;

namespace Application.Interfaces.Repositories;

public interface IBookRepository
{
    Task<Book> GetBookById(Guid bookId);
    Task<IEnumerable<Book>> GetBooks();
}