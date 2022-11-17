namespace Domain.Models;

public class Book
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public ICollection<BookOrder> OrdersLink { get; set; } = new List<BookOrder>();
}