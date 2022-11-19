using System.ComponentModel.DataAnnotations;

namespace Application.Models;

public class BookDto
{
    [Required]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    [Required]
    public int Count { get; set; }
}