using Domain.Enums;

namespace Domain.Models;

public class Order
{
    public int Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public ClientType ClientType { get; set; }

    public List<Book> Books { get; set; } = new();
    public List<BookOrder> BookOrders { get; set; } = new();
}
