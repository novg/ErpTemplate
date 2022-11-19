using System.Xml.Serialization;

namespace Application.Models;

[XmlType(TypeName = "Book")]
public class BookDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int Count { get; set; }
}