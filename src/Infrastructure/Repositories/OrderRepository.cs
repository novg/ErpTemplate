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

        await _context.Entry(order)
            .Collection(o => o.Books)
            .LoadAsync();

        await _context.Entry(order)
            .Collection(o => o.BookOrders)
            .LoadAsync();

        foreach (var book in order.Books)
        {
            int count = order.BookOrders
                .Where(p => p.BookId == book.Id && p.OrderId == order.Id)
                .Select(b => b.BookCount)
                .First();

            book.Count = count;
        }

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