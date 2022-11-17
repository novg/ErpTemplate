using Application.Models;

namespace Application.Interfaces.Services;

public interface IBookService
{
    Task<BookDto> GetBookById(int bookId);
    Task<IEnumerable<BookDto>> GetBooks();
}