using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Models;
using Application.Services;
using AutoMapper;
using Domain.Enums;
using Domain.Models;
using Moq;
using Xunit;

namespace Application.UnitTests.Services;

public class OrderServiceTests
{
    private readonly Mock<IOrderRepository> _repository;
    private readonly Mock<IFileReaderFactory> _factory;
    private readonly Mock<IFileReader> _reader;
    private readonly IMapper _mapper;

    public OrderServiceTests()
    {
        _repository = new Mock<IOrderRepository>();
        _factory = new Mock<IFileReaderFactory>();
        _reader = new Mock<IFileReader>();
        _mapper = Helpers.CreateMapper();
    }

    [Fact]
    public async Task CreateOrder_Success()
    {
        // Arrange
        BookInput book = new()
        {
            Id = 1,
        };

        OrderInput orderInput = new();
        orderInput.Books.Add(book);

        Order order = _mapper.Map<Order>(orderInput);

        _repository
            .Setup(repo => repo.CreateOrder(order))
            .ReturnsAsync(order);

        OrderService service = new(_repository.Object, _factory.Object, _mapper);

        // Act
        await service.CreateOrder(orderInput, ClientType.Api);

        // Assert
    }

    [Fact]
    public async Task CreateOrderFromFile_Success()
    {
        // Arrange
        Stream stream = new MemoryStream();
        string fileName = "file.csv";

        Order order = new();

        _repository
            .Setup(repo => repo.CreateOrder(order))
            .ReturnsAsync(order);

        _reader
            .Setup(reader => reader.Read(stream))
            .ReturnsAsync(order);

        _factory
            .Setup(factory => factory.GetFileReader(Path.GetExtension(fileName)))
            .Returns(_reader.Object);

        OrderService service = new(_repository.Object, _factory.Object, _mapper);

        // Act
        await service.CreateOrderFromFile(fileName, stream, ClientType.Api);

        // Assert
    }

    [Fact]
    public async Task GetOrderById_Success()
    {
        // Arrange
        Book book = new()
        {
            Id = 1,
            Name = "SICP",
            Description = "Structure and Interpretation of Computer Programs MIT cource",
            Price = 100
        };

        int orderId = 1;

        Order order = new()
        {
            Id = orderId,
            ClientType = ClientType.Api,
            Number = "20221101",
        };
        order.Books.Add(book);

        _repository
            .Setup(repo => repo.GetOrderById(orderId))
            .ReturnsAsync(order);

        OrderService service = new(_repository.Object, _factory.Object, _mapper);

        // Act
        OrderDto orderDto = await service.GetOrderById(orderId);

        // Assert
        Assert.NotNull(orderDto);
        Assert.Equal(orderId, orderDto.Id);
    }

    [Fact]
    public async Task GetOrders_Success()
    {
        // Arrange
        Book book = new()
        {
            Id = 1,
            Name = "SICP",
            Description = "Structure and Interpretation of Computer Programs MIT cource",
            Price = 100
        };

        int orderId = 1;

        Order order = new()
        {
            Id = orderId,
            ClientType = ClientType.Api,
            Number = "20221101",
        };
        order.Books.Add(book);

        IEnumerable<Order> orders = new List<Order>() { order };

        _repository
            .Setup(repo => repo.GetOrders())
            .ReturnsAsync(orders);

        OrderService service = new(_repository.Object, _factory.Object, _mapper);

        // Act
        IEnumerable<OrderDto> orderDtos = await service.GetOrders();

        // Assert
        Assert.NotNull(orderDtos);
        Assert.NotEmpty(orderDtos);
    }
}