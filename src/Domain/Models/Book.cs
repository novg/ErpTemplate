namespace Domain.Models;

public class Book
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Count { get; set; }

    public IList<Order> Orders { get; set; } = new List<Order>();
    public IList<BookOrder> BookOrders { get; set; } = new List<BookOrder>();
}