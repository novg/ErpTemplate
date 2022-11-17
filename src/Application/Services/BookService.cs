using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Models;
using AutoMapper;
using Domain.Models;

namespace Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;
    private readonly IMapper _mapper;

    public BookService(IBookRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BookDto> GetBookById(int bookId)
    {
        Book book = await _repository.GetBookById(bookId);
        return _mapper.Map<BookDto>(book);
    }

    public async Task<IEnumerable<BookDto>> GetBooks()
    {
        IEnumerable<Book> books = await _repository.GetBooks();
        return _mapper.Map<IEnumerable<BookDto>>(books);
    }
}