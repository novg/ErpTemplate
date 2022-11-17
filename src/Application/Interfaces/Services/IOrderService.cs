using Application.Models;

namespace Application.Interfaces.Services;

public interface IOrderService
{
    Task<OrderDto> CreateOrder(OrderDto input);
    Task<OrderDto> GetOrderById(int orderId);
    Task<IEnumerable<OrderDto>> GetOrders();
}