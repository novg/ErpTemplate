using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Application.Models;

public class OrderInput
{
    [Required]
    [MinLength(1)]
    [XmlElement("books")]
    public List<BookInput> Books { get; set; } = new();
}