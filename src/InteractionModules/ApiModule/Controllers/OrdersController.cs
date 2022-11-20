using Application.Exceptions;
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
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
    {
        try
        {
            IEnumerable<OrderDto> orders = await _service.GetOrders();
            return Ok(orders);
        }
        catch (System.Exception ex)
        {
            _logger.LogError("Error get orders: {ErrorMessage}", ex.Message);
            return Problem(detail: ex.Message);
        }
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
    public async Task<ActionResult<OrderDto>> GetOrderById(int orderId)
    {
        try
        {
            OrderDto order = await _service.GetOrderById(orderId);
            return Ok(order);
        }
        catch (EntityNotFoundException)
        {
            _logger.LogError("Error get: order {OrderId} not found", orderId);
            return NotFound();
        }
        catch (System.Exception ex)
        {
            _logger.LogError("Error get order {OrderId}: {ErrorMessage}", orderId, ex.Message);
            return Problem(detail: ex.Message);
        }
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
    public async Task<ActionResult<OrderDto>> CreateOrder(OrderInput input, ClientType? clientType)
    {
        try
        {
            OrderDto order = await _service.CreateOrder(input, clientType);
            _logger.LogInformation("Created order: {OrderId}, books count: {BookCount}", order.Id, order.Books.Count());
            return Ok(order);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError("Error create order: {ErrorMessage}", ex.Message);
            return BadRequest(ex.Message);
        }
        catch (System.Exception ex)
        {
            _logger.LogError("Error create order: {ErrorMessage}", ex.Message);
            return Problem(detail: ex.Message);
        }
    }

    [HttpPost("{clientType:ClientType}/file")]
    public async Task<ActionResult<OrderDto>> CreateOrderFromFile(IFormFile file, ClientType? clientType)
    {
        try
        {
            using (Stream stream = file.OpenReadStream())
            {
                OrderDto order = await _service.CreateOrderFromFile(file.FileName, stream, clientType);
                _logger.LogInformation("Created order: {OrderId}, books count: {BookCount}", order.Id, order.Books.Count());
                return Ok(order);
            }
        }
        catch (ArgumentException ex)
        {
            _logger.LogError("Error create order: {ErrorMessage}", ex.Message);
            return BadRequest(ex.Message);
        }
        catch (System.Exception ex)
        {
            _logger.LogError("Error create order: {ErrorMessage}", ex.Message);
            return Problem(detail: ex.Message);
        }
    }
}