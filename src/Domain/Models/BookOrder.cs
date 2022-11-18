namespace Domain.Models;

public class BookOrder
{
    public int BookCount { get; set; }

    public int BookId { get; set; }
    public Book? Book { get; set; }

    public int OrderId { get; set; }
    public Order? Order { get; set; }
}