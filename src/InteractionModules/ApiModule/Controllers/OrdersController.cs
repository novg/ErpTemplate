using Application.Interfaces.Services;
using Application.Models;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ApiModule.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _service;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(IOrderService service, ILogger<OrdersController> logger)
    {
        _service = service;
        _logger = logger;
    }

    /// <summary>
    /// Get a Order's list.
    /// </summary>
    /// <returns>A Order's list.</returns>
    /// <response code="200">Returns exists orders</response>
    /// <response code="500">If internal server error occured.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IEnumerable<OrderDto>> GetOrders()
    {
        IEnumerable<OrderDto> orders = await _service.GetOrders();
        return orders;
    }

    /// <summary>
    /// Get a Order by id.
    /// </summary>
    /// <param name="orderId">Order id for getting</param>
    /// <returns>A existing Order.</returns>
    /// <response code="200">Returns existsing order</response>
    /// <response code="404">If order not exists.</response>
    /// <response code="500">If internal server error occured.</response>
    [HttpGet("{orderId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<OrderDto> GetOrderById(int orderId)
    {
        return await _service.GetOrderById(orderId);
    }

    /// <summary>
    /// Creates a Order.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="clientType">Type of client module that sent the order</param>
    /// <returns>A newly created Order</returns>
    /// <response code="201">Returns the newly created order</response>
    /// <response code="400">If the input is null</response>
    /// <response code="500">If internal server error occured.</response>
    [HttpPost("{clientType:ClientType?}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<OrderDto> CreateOrder(OrderInput input, ClientType? clientType)
    {
        OrderDto order = await _service.CreateOrder(input, clientType);
        _logger.LogInformation("Created order: {OrderId}, books count: {BookCount}", order.Id, order.Books.Count());
        return order;
    }

    [HttpPost("{clientType:ClientType}/file")]
    public async Task<OrderDto> CreateOrderFromFile(IFormFile file, ClientType? clientType)
    {
        using (Stream stream = file.OpenReadStream())
        {
            OrderDto order = await _service.CreateOrderFromFile(file.FileName, stream, clientType);
            _logger.LogInformation("Created order: {OrderId}, books count: {BookCount}", order.Id, order.Books.Count());
            return order;
        }
    }
}