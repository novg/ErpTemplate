using Application.Interfaces;
using Domain.Models;

namespace Infrastructure.Services;

public class XlsxReader : IFileReader
{
    public Task<Order> Read(Stream stream)
    {
        throw new NotImplementedException();
    }
}