using Xunit;
using System;
using System.Threading.Tasks;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Application.Interfaces.Repositories;
using Domain.Models;
using Domain.Enums;
using System.Linq;

namespace Intrastructure.UnitTests.Repositories;

public class OrderRepositoryTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly IOrderRepository _repository;

    public OrderRepositoryTests()
    {
        _context = Helpers.CreateDbContext();
        _repository = new OrderRepository(_context);
        Assert.NotNull(_repository);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    [Fact]
    public async Task CreateOrder_Success()
    {
        // Arrange
        int bookId = 1;
        Book? book = await _context.Books.FindAsync(bookId);
        book!.Count = 3;

        Order order = new()
        {
            Number = "20221101",
            ClientType = ClientType.Api,
        };

        order.Books.Add(book);

        // Act
        Order newOrder = await _repository.CreateOrder(order);

        // Assert
        Assert.NotNull(newOrder);
        Assert.Equal(order.Number, newOrder.Number);
        Assert.Equal(order.ClientType, newOrder.ClientType);

        Assert.NotEmpty(newOrder.Books);
        Assert.Equal(order.Books.Count, newOrder.Books.Count);
        Assert.Equal(book.Id, newOrder.Books.First().Id);
        Assert.Equal(book.Name, newOrder.Books.First().Name);
    }
}