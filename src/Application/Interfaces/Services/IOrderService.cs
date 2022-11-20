using Application.Models;
using Domain.Enums;

namespace Application.Interfaces.Services;

public interface IOrderService
{
    Task<OrderDto> CreateOrder(OrderInput input, ClientType? clientType);
    Task<OrderDto> CreateOrderFromFile(string fileName, Stream stream, ClientType? clientType);
    Task<OrderDto> GetOrderById(int orderId);
    Task<IEnumerable<OrderDto>> GetOrders();
}