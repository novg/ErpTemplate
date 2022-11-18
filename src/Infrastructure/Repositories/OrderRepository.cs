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

        SetBooksCount(order);

        return order;
    }

    public async Task<Order> GetOrderById(int orderId)
    {
        Order? order = await _context.Orders
            .AsNoTracking()
            .Include(order => order.Books)
            .Include(order => order.BookOrders)
            .Where(order => order.Id == orderId)
            .SingleOrDefaultAsync();

        if (order == null)
        {
            throw new EntityNotFoundException();
        }

        SetBooksCount(order);

        return order;
    }

    public async Task<IEnumerable<Order>> GetOrders()
    {
        IEnumerable<Order> orders = await _context.Orders
            .AsNoTracking()
            .Include(order => order.Books)
            .Include(order => order.BookOrders)
            .ToListAsync();

        foreach (var order in orders)
        {
            SetBooksCount(order);
        }

        return orders;
    }

    private static void SetBooksCount(Order order)
    {
        foreach (var book in order.Books)
        {
            int count = order.BookOrders
                .Where(p => p.BookId == book.Id && p.OrderId == order.Id)
                .Select(b => b.BookCount)
                .First();

            book.Count = count;
        }
    }
}