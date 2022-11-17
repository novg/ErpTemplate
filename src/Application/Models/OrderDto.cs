using Domain.Enums;

namespace Application.Models;

public class OrderDto
{
    public int Id { get; set; }

    public string Number { get; set; } = string.Empty;

    public DateTime Created { get; set; } = DateTime.UtcNow;

    public ClientType ClientType { get; set; }

    public IList<BookDto> Books { get; set; } = new List<BookDto>();
}