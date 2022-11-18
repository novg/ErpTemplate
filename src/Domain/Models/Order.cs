using Domain.Enums;

namespace Domain.Models;

public class Order
{
    public int Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public ClientType ClientType { get; set; }

    public IList<Book> Books { get; set; } = new List<Book>();
    public IList<BookOrder> BookOrders { get; set; } = new List<BookOrder>();
}
