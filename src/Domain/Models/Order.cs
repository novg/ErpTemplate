using Domain.Enums;

namespace Domain.Models;

public class Order
{
    public int Id { get; set; }

    public string Number { get; set; } = string.Empty;

    public DateTime Created { get; set; }

    public ClientType ClientType { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();
}
