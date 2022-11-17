using Xunit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Application.Interfaces.Repositories;
using Domain.Models;
using Domain.Exceptions;
using System.Linq;

namespace Intrastructure.UnitTests;

public class BookRepositoryTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly IBookRepository _repository;

    public BookRepositoryTests()
    {
        _context = Helpers.CreateDbContext();
        _repository = new BookRepository(_context);
        Assert.NotNull(_repository);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    [Fact]
    public async Task GetBooks_Success()
    {
        // Arrange
        // Act
        IEnumerable<Book> books = await _repository.GetBooks();

        // Assert
        Assert.NotNull(books);
        Assert.NotEmpty(books);
        Assert.Equal(3, books.Count());
    }

    [Fact]
    public async Task GetBookById_Success()
    {
        // Arrange
        int bookId = 1;
        Book dbBook = _context.Books.Find(bookId);

        // Act
        Book book = await _repository.GetBookById(bookId);

        // Assert
        Assert.NotNull(book);
        Assert.Equal(dbBook.Name, book.Name);
        Assert.Equal(dbBook.Description, book.Description);
        Assert.Equal(dbBook.Price, book.Price);
    }

    [Fact]
    public async Task GetBookById_Failure_BookNotFound()
    {
        // Arrange
        int notExistentBookId = 10;

        // Act
        // Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            await _repository.GetBookById(notExistentBookId));
    }
}