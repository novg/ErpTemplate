using Domain.Models;

namespace Application.Interfaces.Repositories;


public interface IOrderRepository
{
    Task<Order> CreateOrder(Order order);
    Task<Order> GetOrderById(int orderId);
    Task<IEnumerable<Order>> GetOrders();
}