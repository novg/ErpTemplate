using Application.Interfaces.Services;
using Application.Models;
using Domain.Exceptions;
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
    /// <returns>A newly created Order</returns>
    /// <response code="201">Returns the newly created order</response>
    /// <response code="400">If the input is null</response>
    /// <response code="500">If internal server error occured.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<OrderDto>> CreateOrder(OrderDto input)
    {
        try
        {
            OrderDto order = await _service.CreateOrder(input);
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
}