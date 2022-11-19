using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Application.Models;

[XmlType(TypeName = "Book")]
public class BookInput
{
    [Required]
    [XmlElement("id")]
    public int Id { get; set; }

    [Required]
    [Range(1, 1000)]
    [XmlElement("count")]
    public int Count { get; set; }
}