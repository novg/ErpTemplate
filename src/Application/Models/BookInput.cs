using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Application.Models;

[XmlType(TypeName = "book")]
public class BookInput
{
    [Required]
    [Range(1, Int32.MaxValue)]
    [DefaultValue(1)]
    [XmlElement("id")]
    public int Id { get; set; }

    [Required]
    [Range(1, 1000)]
    [DefaultValue(1)]
    [XmlElement("count")]
    public int Count { get; set; }
}