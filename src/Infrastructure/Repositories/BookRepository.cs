using Application.Interfaces.Repositories;
using Domain.Exceptions;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _context;

    public BookRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Book> GetBookById(Guid bookId)
    {
        Book? book = await _context.Books
            .FindAsync(bookId);

        if (book == null)
        {
            throw new EntityNotFoundException();
        }

        return book;
    }

    public async Task<IEnumerable<Book>> GetBooks()
    {
        return await _context.Books
            .AsNoTracking()
            .ToListAsync();
    }
}