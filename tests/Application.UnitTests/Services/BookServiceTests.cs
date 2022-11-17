using System.Collections.Generic;
using Application.Interfaces.Repositories;
using Application.Models;
using Application.Services;
using AutoMapper;
using Domain.Models;
using Moq;
using Xunit;

namespace Application.UnitTests;

public class BookServiceTests
{

    private readonly Mock<IBookRepository> _repository;
    private readonly IMapper _mapper;

    public BookServiceTests()
    {
        _repository = new Mock<IBookRepository>();
        _mapper = Helpers.CreateMapper();
    }

    [Fact]
    public async void GetBookById_Success()
    {
        int bookId = 1;
        // Arrange
        Book book = new()
        {
            Id = bookId,
            Name = "SICP",
            Description = "Structure and Interpretation of Computer Programs MIT cource",
            Price = 100
        };

        _repository
            .Setup(repo => repo.GetBookById(bookId))
            .ReturnsAsync(book);

        BookService service = new(_repository.Object, _mapper);

        // Act
        BookDto bookDto = await service.GetBookById(bookId);

        // Assert
        Assert.NotNull(bookDto);
        Assert.Equal(book.Id, bookDto.Id);
        Assert.Equal(book.Name, bookDto.Name);
        Assert.Equal(book.Description, bookDto.Description);
    }

    [Fact]
    public async void GetBooks_Success()
    {
        IEnumerable<Book> books = new List<Book>()
        {
            new Book { Id = 1, Name = "SICP", Description = "Structure and Interpretation of Computer Programs MIT cource", Price = 100 },
            new Book { Id = 2, Name = "Getting Clojure", Description = "Describe Clojure programming language", Price = 200 },
            new Book { Id = 3, Name = "Web Development with Clojure", Description = "Describe Web Development with Clojure programming language", Price = 300 }
        };

        _repository
            .Setup(repo => repo.GetBooks())
            .ReturnsAsync(books);

        BookService service = new(_repository.Object, _mapper);

        // Act
        IEnumerable<BookDto> booksDto = await service.GetBooks();

        // Assert
        Assert.NotNull(booksDto);
        Assert.NotEmpty(booksDto);
    }
}