namespace Domain.Models;

public class BookOrder
{
    public int BookId { get; set; }
    public int OrderId { get; set; }
    public int BookCount { get; set; }

    public Order? Order { get; set; }
    public Book? Book { get; set; }
}