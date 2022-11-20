using Application.Interfaces;
using Application.Validators;
using Domain.Models;

namespace Application.Services;

public class CsvReader : IFileReader
{
    public async Task<Order> Read(Stream stream)
    {
        Order order = new Order();
        string[] row = new string[2];

        using (StreamReader reader = new StreamReader(stream))
        {
            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                row = line.Split(',');

                if (!int.TryParse(row[0], out int bookId)
                 || !int.TryParse(row[1], out int bookCount))
                {
                    continue;
                }

                order.Books.Add(new Book
                {
                    Id = bookId,
                    Count = bookCount
                });
            }
        }

        OrderValidator.Validate(order);

        return order;
    }
}