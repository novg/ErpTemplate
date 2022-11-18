using Application.Interfaces.Repositories;
using Domain.Exceptions;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Order> CreateOrder(Order order)
    {
        if (order == null)
        {
            throw new ArgumentException(nameof(order));
        }


        foreach (Book book in order.Books)
        {
            order.BookOrders.Add(new BookOrder
            {
                BookId = book.Id,
                BookCount = book.Count,
            });
        }

        order.Books.Clear();
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return order;
    }

    public async Task<Order> GetOrderById(int orderId)
    {
        Order? order = await _context.Orders
            .AsNoTracking()
            .Include(order => order.Books)
            .Where(order => order.Id == orderId)
            .SingleOrDefaultAsync();

        if (order == null)
        {
            throw new EntityNotFoundException();
        }

        return order;
    }

    public async Task<IEnumerable<Order>> GetOrders()
    {
        return await _context.Orders
            .AsNoTracking()
            .Include(order => order.Books)
            .ToListAsync();
    }
}