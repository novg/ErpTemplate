using Application.Models;

namespace Application.Interfaces.Services;

public interface IOrderService
{
    Task<OrderDto> CreateOrder(OrderInput input);
    Task<OrderDto> GetOrderById(int orderId);
    Task<IEnumerable<OrderDto>> GetOrders();
}