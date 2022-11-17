using System.Collections.Generic;
using System.Threading.Tasks;
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
    private readonly IMapper _mapper;

    public OrderServiceTests()
    {
        _repository = new Mock<IOrderRepository>();
        _mapper = Helpers.CreateMapper();
    }

    [Fact]
    public async Task CreateOrder_Success()
    {
        // Arrange
        BookDto book = new()
        {
            Id = 1,
            Name = "SICP",
            Description = "Structure and Interpretation of Computer Programs MIT cource",
            Price = 100
        };

        OrderDto orderDto = new()
        {
            Id = 1,
            ClientType = ClientType.Api,
            Number = "20221101",
        };
        orderDto.Books.Add(book);

        Order order = _mapper.Map<Order>(orderDto);

        _repository
            .Setup(repo => repo.CreateOrder(order))
            .ReturnsAsync(order);

        OrderService service = new(_repository.Object, _mapper);

        // Act
        await service.CreateOrder(orderDto);

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

        OrderService service = new(_repository.Object, _mapper);

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

        OrderService service = new(_repository.Object, _mapper);

        // Act
        IEnumerable<OrderDto> orderDtos = await service.GetOrders();

        // Assert
        Assert.NotNull(orderDtos);
        Assert.NotEmpty(orderDtos);
    }
}